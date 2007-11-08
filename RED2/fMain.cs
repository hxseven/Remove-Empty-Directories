using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Reflection;

namespace RED2
{
	public partial class fMain : Form
	{
		private FolderScanWorker FSearchWorker = null;
		private FolderFindWorker FFindWorker = null;

		private int MaxFolders = 0;
		private DirectoryInfo StartFolder = null;
		private TreeNode RootNode = null;

        private String[] ignoreFiles = { };
        
        private Dictionary<String, TreeNode> Dir2Tree = null;
        private Dictionary<String, String> ProtectedFolders = new Dictionary<string, string>();

        // Empty folder list
        private List<DirectoryInfo> EmptyFolders = null;

        // Registry keys
		private string MenuName = "Folder\\shell\\{0}";
        private string Command = "Folder\\shell\\{0}\\command";
        
		private bool StopDeleteProcess = false;

        // Process states
		private enum Processes
		{
			Scan, Search, Delete, Idle 
		}

		private Processes CurrentProcess = Processes.Idle;

        private cSettings settings = null;

        private PictureBox picIcon = new PictureBox();
        private Label picLabel = new Label();

        private int DeleteStats = 0;

		/// <summary>
		/// Constructor
		/// </summary>
		public fMain()
		{
			InitializeComponent();
		}

		/// <summary>
		/// On load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fMain_Load(object sender, EventArgs e)
		{        
            Assembly REDAssembly =	Assembly.GetExecutingAssembly();
            AssemblyName AssemblyName =	REDAssembly.GetName();

            this.lbAppTitle.Text += string.Format("{0}",AssemblyName.Version.ToString());

            this.MenuName = String.Format(MenuName, RED2.Properties.Resources.registry_name);
            this.Command = String.Format(Command, RED2.Properties.Resources.registry_name);

			this.pbStatus.Maximum = 100;
			this.pbStatus.Minimum = 0;
			this.pbStatus.Step = 5;
            
            #region Check for the registry key

            RegistryKey RegistryShellKey = Registry.ClassesRoot.OpenSubKey(MenuName);
            if (RegistryShellKey == null)
                this.cbIntegrateIntoWindowsExplorer.Checked = false;
            else
                this.cbIntegrateIntoWindowsExplorer.Checked = true; 

            #endregion

            #region Set and display folder status icons

            Dictionary<string, string> Icons = new Dictionary<string, string>();

            Icons.Add("home", RED2.Properties.Resources.icon_root);
            Icons.Add("folder", RED2.Properties.Resources.icon_default);
            Icons.Add("folder_trash_files", RED2.Properties.Resources.icon_contains_trash);
            Icons.Add("folder_hidden", RED2.Properties.Resources.icon_hidden_folder);
            Icons.Add("folder_lock", RED2.Properties.Resources.icon_locked_folder);
            Icons.Add("folder_warning", RED2.Properties.Resources.icon_warning);
            Icons.Add("protected", RED2.Properties.Resources.icon_protected_folder);
            Icons.Add("deleted", RED2.Properties.Resources.icon_deleted_folder);

            int xpos = 6;
            int ypos = 30;

            foreach (string key in Icons.Keys)
            {
                Image Icon = (Image)this.ilFolderIcons.Images[key];

                PictureBox picIcon = new PictureBox();
                picIcon.Image = Icon;
                picIcon.Location = new System.Drawing.Point(xpos, ypos);
                picIcon.Name = "picIcon";
                picIcon.Size = new System.Drawing.Size(Icon.Width, Icon.Height);

                Label picLabel = new Label();
                picLabel.Text = Icons[key];
                picLabel.Location = new System.Drawing.Point(xpos + Icon.Width + 2, ypos + 2);
                picLabel.Name = "picLabel";

                this.pnlIcons.Controls.Add(picIcon);
                this.pnlIcons.Controls.Add(picLabel);

                ypos += Icon.Height + 6;
            } 
            #endregion

            #region Read config settings

            // settings path:
            string configPath = Path.Combine(Application.StartupPath, RED2.Properties.Resources.config_file);

            // Settings object:
            settings = new cSettings(configPath);

            // Read folder from the config file
            this.tbFolder.Text = this.settings.Read("folder", RED2.Properties.Resources.folder);

            bool KeepSystemFolders = this.settings.Read("keep_system_folders", Boolean.Parse(RED2.Properties.Resources.keep_system_folders));

            if (!KeepSystemFolders)
                this.cbKeepSystemFolders.Tag = 1; // this disables the warning message

            this.cbKeepSystemFolders.Checked = KeepSystemFolders;

            this.cbIgnoreHiddenFolders.Checked = this.settings.Read("dont_scan_hidden_folders", Boolean.Parse(RED2.Properties.Resources.dont_scan_hidden_folders));

            this.cbIgnore0kbFiles.Checked = this.settings.Read("ignore_0kb_files", Boolean.Parse(RED2.Properties.Resources.ignore_0kb_files));

            this.nuMaxDepth.Value = (int)this.settings.Read("max_depth", Int32.Parse(RED2.Properties.Resources.max_depth));
            this.nuPause.Value = (int)this.settings.Read("pause_between", Int32.Parse(RED2.Properties.Resources.pause_between));

            this.tbIgnoreFiles.Text = this.settings.Read("ignore_files", FixText(RED2.Properties.Resources.ignore_files));

            this.tbIgnoreFolders.Text = this.settings.Read("ignore_folders", FixText(RED2.Properties.Resources.ignore_folders));

            DeleteStats = this.settings.Read("delete_stats", 0);
            UpdateDeleteStats();

            #endregion           

            #region Read and apply command line arguments

            string[] Arguments = Environment.GetCommandLineArgs();

            if (Arguments.Length > 1)
                this.tbFolder.Text = Arguments[1].ToString(); 

            #endregion

  		}

        public static string FixText(string _str) {
            return _str.Replace(@"\r\n", "\r\n").Replace(@"\n", "\n"); 
        }

        private void UpdateDeleteStats()
        {
            this.lblRedStats.Text = String.Format(RED2.Properties.Resources.red_deleted, DeleteStats); 
        }

	
		/// <summary>
		/// Exit
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}


		/// <summary>
		/// Starts the Scan-Progress
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnScan_Click(object sender, EventArgs e)
		{
			this.CurrentProcess = Processes.Search;

            ProtectedFolders = new Dictionary<string, string>();
            EmptyFolders = new List<DirectoryInfo>();
            Dir2Tree = new Dictionary<string, TreeNode>();

			// Check given folder:
			DirectoryInfo Folder = new DirectoryInfo(this.tbFolder.Text);

			if (!Folder.Exists)
			{
                MessageBox.Show(RED2.Properties.Resources.error_dir_does_not_exist);
				return;
			}

            this.settings.Write("folder", Folder.FullName);

			// Update button states
			this.btnCancel.Enabled = true;
			this.btnScan.Enabled = false;
			this.btnDelete.Enabled = false;

			// Reset TreeView
			this.tvFolders.Nodes.Clear();

			this.StartFolder = Folder;

			// Set marquee mode
			this.pbStatus.Style = ProgressBarStyle.Marquee;

            this.lbStatus.Text = RED2.Properties.Resources.scanning_folders;

			// Create new blank worker
			FSearchWorker = new FolderScanWorker();
			FSearchWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FSWorker_RunWorkerCompleted);

			// set settings:
			FSearchWorker.MaxDepth = (int)this.nuMaxDepth.Value;

			// Start worker
			FSearchWorker.RunWorkerAsync(Folder);
		}


		/// <summary>
		/// Scan process finished
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FSWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			FolderScanWorker LWorker = sender as FolderScanWorker;

			this.pbStatus.Style = ProgressBarStyle.Blocks;

			// First, handle the case where an exception was thrown.
			if (e.Error != null)			
				MessageBox.Show(e.Error.Message);			
			else if (e.Cancelled)
                this.lbStatus.Text = RED2.Properties.Resources.process_cancelled;			
			else
			{
				MaxFolders = (int)e.Result;

				// Set max value:
				this.pbStatus.Maximum = MaxFolders + 1;

				this.StartFindProcess();
			}

			FSearchWorker.Dispose();
			FSearchWorker = null;
		}


		/// <summary>
		/// Start searching empty folders:
		/// </summary>
		void StartFindProcess()
		{
			this.CurrentProcess = Processes.Scan;

			EmptyFolders = new List<DirectoryInfo>();

			Dir2Tree = new Dictionary<String, TreeNode>();
            this.lbStatus.Text = RED2.Properties.Resources.searching_empty_folders;

            #region Create root node
            
            RootNode = new TreeNode(StartFolder.Name);
            RootNode.Tag = StartFolder;
            RootNode.ImageKey = "home";
            RootNode.SelectedImageKey = "home";
            this.tvFolders.Nodes.Add(RootNode); 

            #endregion

			Dir2Tree.Add(StartFolder.FullName, RootNode);

            #region Initialize the FolderFindWorker-Object

            FFindWorker = new FolderFindWorker();
            FFindWorker.ProgressChanged += new ProgressChangedEventHandler(FFWorker_ProgressChanged);
            FFindWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FFWorker_RunWorkerCompleted);

            // set options:
            if (!FFindWorker.SetIgnoreFiles(this.tbIgnoreFiles.Text))
                MessageBox.Show(RED2.Properties.Resources.error_ignore_settings);

            if (!FFindWorker.SetIgnoreFolders(this.tbIgnoreFolders.Text))
                MessageBox.Show(RED2.Properties.Resources.error_ignore_settings);

            FFindWorker.Ignore0kbFiles = this.cbIgnore0kbFiles.Checked;
            FFindWorker.IgnoreHiddenFolders = this.cbIgnoreHiddenFolders.Checked;
            FFindWorker.IgnoreSystemFolders = this.cbKeepSystemFolders.Checked;

            FFindWorker.MaxDepth = (int)this.nuMaxDepth.Value;

            // Start worker
            FFindWorker.RunWorkerAsync(StartFolder); 

            #endregion

		}


        /// <summary>
        /// This function gets called on a status update of the 
        /// find worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void FFWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if ((int)e.ProgressPercentage != -1)
			{
				// Just update the progress bar
				this.lbStatus.Text = (string)e.UserState;
				this.pbStatus.Value = (int)e.ProgressPercentage;
			}
			else
			{
				// Found a empty folder:
				DirectoryInfo Folder = (DirectoryInfo)e.UserState;
				EmptyFolders.Add(Folder);
				this.AddFolderToTreeView(Folder, true);
			}
		}


        /// <summary>
        /// Adds a folder to the treeview
        /// </summary>
        /// <param name="Folder"></param>
        /// <param name="_isEmpty"></param>
        /// <returns></returns>
		private TreeNode AddFolderToTreeView(DirectoryInfo Folder, bool _isEmpty)
		{
			// exists already:
			if (Dir2Tree.ContainsKey(Folder.FullName))
			{
				//Dir2Tree[Folder.FullName].ImageKey = "folder_delete";
				Dir2Tree[Folder.FullName].ForeColor = Color.Red;
				return Dir2Tree[Folder.FullName];
			}

			TreeNode NewNode = new TreeNode(Folder.Name);
			if (_isEmpty)
				//NewNode.ImageKey = "folder_delete";
				NewNode.ForeColor = Color.Red;
			else
				NewNode.ForeColor = Color.Gray;

            bool ContainsTrash = (!(Folder.GetFiles().Length == 0) && _isEmpty);

            NewNode.ImageKey = ContainsTrash ? "folder_trash_files" : "folder";
        
			if ((Folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                NewNode.ImageKey = ContainsTrash ? "folder_hidden_trash_files" : "folder_hidden";

			if ((Folder.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted)
                NewNode.ImageKey = ContainsTrash ? "folder_lock_trash_files" : "folder_lock";

			if ((Folder.Attributes & FileAttributes.System) == FileAttributes.System)
                NewNode.ImageKey = ContainsTrash ? "folder_lock_trash_files" : "folder_lock";
            
            NewNode.SelectedImageKey = NewNode.ImageKey;

			NewNode.Tag = Folder;

			if (Folder.Parent.FullName.Trim('\\') == this.StartFolder.FullName.Trim('\\'))
				this.RootNode.Nodes.Add(NewNode);
			else 
            {
				TreeNode ParentNode = this.FindTreeNodeByFolder(Folder.Parent);
				ParentNode.Nodes.Add(NewNode);
			}

			Dir2Tree.Add(Folder.FullName, NewNode);

			NewNode.EnsureVisible();

			return NewNode;
		}


        /// <summary>
        /// Returns a TreeNode Object for a given Folder
        /// </summary>
        /// <param name="Folder"></param>
        /// <returns></returns>
		private TreeNode FindTreeNodeByFolder(DirectoryInfo Folder)
		{
			// Folder exists already:
			if (Dir2Tree.ContainsKey(Folder.FullName))
				return Dir2Tree[Folder.FullName];
			else 
				return AddFolderToTreeView(Folder, false);
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void FFWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			FolderFindWorker FindWorker = sender as FolderFindWorker;

			// First, handle the case where an exception was thrown.
			if (e.Error != null)
                MessageBox.Show(RED2.Properties.Resources.error + "\n\n" + e.Error.Message);
			else if (e.Cancelled)
                this.lbStatus.Text = RED2.Properties.Resources.process_cancelled;	
			else
			{
                this.lbStatus.Text = String.Format(RED2.Properties.Resources.found_x_empty_folders, FindWorker.EmptyFolderCount, FindWorker.FolderCount);
				e = null;
			}

            this.pbStatus.Value = this.pbStatus.Maximum;

			this.btnScan.Enabled = true;
			this.btnCancel.Enabled = false;

			FFindWorker.Dispose();
			FFindWorker = null;

			this.RootNode.EnsureVisible();

            if (FindWorker.EmptyFolderCount != 0)
			    this.btnDelete.Enabled = true;

			this.btnScan.Text = RED2.Properties.Resources.btn_scan_again;

			this.CurrentProcess = Processes.Idle;

		}	


		/// <summary>
		/// Let's the user select a folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnChooseFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlgFolder = new FolderBrowserDialog();

			dlgFolder.Description = RED2.Properties.Resources.please_select;
			dlgFolder.ShowNewFolderButton = false;

            if (this.tbFolder.Text != "")
            {
                DirectoryInfo dir = new DirectoryInfo(this.tbFolder.Text);

                if (dir.Exists)
                    dlgFolder.SelectedPath = this.tbFolder.Text;
            }

			if (dlgFolder.ShowDialog() == DialogResult.OK)
				this.tbFolder.Text = dlgFolder.SelectedPath;	

			dlgFolder.Dispose();
			dlgFolder = null;
		}


		/// <summary>
		/// User hit's cancel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (this.CurrentProcess == Processes.Search && this.FSearchWorker != null)
			{
				if ((this.FSearchWorker.IsBusy == true) || (FSearchWorker.CancellationPending == false))
					FSearchWorker.CancelAsync();

				this.btnScan.Enabled = true;
			
			}
			else if (this.CurrentProcess == Processes.Scan && this.FFindWorker != null)
			{
				if ((this.FFindWorker.IsBusy == true) || (FFindWorker.CancellationPending == false))
					FFindWorker.CancelAsync();

				this.btnScan.Enabled = true;
				
			}
			else if (this.CurrentProcess == Processes.Delete)
			{
				this.StopDeleteProcess = true;
				this.btnScan.Enabled = true;
				this.btnDelete.Enabled = false;
			}

			this.btnCancel.Enabled = false;

			this.CurrentProcess = Processes.Idle;
		}


        /// <summary>
        /// User clicks twice on a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void tvFolders_DoubleClick(object sender, EventArgs e)
		{
            this.OpenFolder();
		}

        /// <summary>
        /// Opens a folder
        /// </summary>
        private void OpenFolder()
        {
            string path = this.GetSelectedFolderPath();

            if (path == "")
                return;

            string windows_folder = Environment.GetEnvironmentVariable("SystemRoot");

            Process.Start(windows_folder + "\\explorer.exe", "/e,\"" + path + "\"");
        }


        /// <summary>
        /// Returns the selected folder path
        /// </summary>
        /// <returns></returns>
		private string GetSelectedFolderPath()
		{
			if (this.tvFolders.SelectedNode != null && this.tvFolders.SelectedNode.Tag != null)
			{
				DirectoryInfo folder = (DirectoryInfo)this.tvFolders.SelectedNode.Tag;
				return folder.FullName;
			}
			return "";
		}


		/// <summary>
		/// Integrates RED into the Windows explorer
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbIntegrateIntoWindowsExplorer_CheckedChanged(object sender, EventArgs e)
		{
			RegistryKey regmenu = null;
			RegistryKey regcmd = null;

			CheckBox cb = (CheckBox)sender;

			if (cb.Checked)
			{
				try
				{
					regmenu = Registry.ClassesRoot.CreateSubKey(MenuName);
					if (regmenu != null)
						regmenu.SetValue("", RED2.Properties.Resources.registry_name);

					regcmd = Registry.ClassesRoot.CreateSubKey(Command);
					if (regcmd != null)
						regcmd.SetValue("", Application.ExecutablePath + " \"%1\"");
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.ToString());
				}
				finally
				{
					if (regmenu != null)
						regmenu.Close();
					if (regcmd != null)
						regcmd.Close();
				}
			}
			else {
				try
				{
					RegistryKey reg = Registry.ClassesRoot.OpenSubKey(Command);
					if (reg != null)
					{
						reg.Close();
						Registry.ClassesRoot.DeleteSubKey(Command);
					}
					reg = Registry.ClassesRoot.OpenSubKey(MenuName);
					if (reg != null)
					{
						reg.Close();
						Registry.ClassesRoot.DeleteSubKey(MenuName);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(RED2.Properties.Resources.error + "\n\n" + this, ex.ToString());
				}
			}

		}


        /// <summary>
        /// Deletes all empty folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void btnDelete_Click(object sender, EventArgs e)
		{
            this.lbStatus.Text = RED2.Properties.Resources.rem_empty_folders;

			this.CurrentProcess = Processes.Delete;
			this.StopDeleteProcess = false;

			this.btnScan.Enabled = false;
			this.btnCancel.Enabled = true;

			int DeletedFolderCount = 0;
			int FailedFolderCount = 0;

            this.pbStatus.Maximum = EmptyFolders.Count;
            this.pbStatus.Minimum = 0;
            this.pbStatus.Step = 5;

            this.ignoreFiles = FormatAndSplit(this.tbIgnoreFiles.Text);

            int FolderCount = EmptyFolders.Count;

			for (int i = 0; (i < EmptyFolders.Count && !this.StopDeleteProcess); i++)
			{
				DirectoryInfo Folder = EmptyFolders[i];

                // Do not delete protected folders:
                if (!this.ProtectedFolders.ContainsKey(Folder.FullName))
                {
                    if (this.SecureDelete(Folder))
                    {
                        this.lbStatus.Text = String.Format(RED2.Properties.Resources.removing_empty_folders, (i + 1), FolderCount);
                        this.MarkAsDeleted(Folder);
                        DeletedFolderCount++;
                    }
                    else
                    {
                        this.MarkAsWarning(Folder);
                        FailedFolderCount++;
                    }

                    Application.DoEvents();
                    Thread.Sleep(TimeSpan.FromMilliseconds((double)this.nuPause.Value));

                }
				
                this.pbStatus.Value = i;

			}

            this.pbStatus.Value = this.pbStatus.Maximum;

            this.lbStatus.Text = String.Format(RED2.Properties.Resources.found_x_empty_folders, DeletedFolderCount, FailedFolderCount);

            #region Update delete stats
            this.DeleteStats += DeletedFolderCount;
            this.settings.Write("delete_stats", this.DeleteStats);
            UpdateDeleteStats(); 
            #endregion

			this.btnDelete.Enabled = false;
			this.btnScan.Enabled = true;
			this.btnCancel.Enabled = false;

			this.CurrentProcess = Processes.Idle;
		}


        /// <summary>
        /// Marks a folder with the "warning" icon
        /// </summary>
        /// <param name="Folder"></param>
		private void MarkAsWarning(DirectoryInfo Folder)
		{
			TreeNode FNode = this.FindTreeNodeByFolder(Folder);
            FNode.ImageKey = "folder_warning";
            FNode.SelectedImageKey = "folder_warning";
            FNode.EnsureVisible();
		}


        /// <summary>
        /// Marks a folder with the "deleted" icon
        /// </summary>
        /// <param name="Folder"></param>
		private void MarkAsDeleted(DirectoryInfo Folder)
		{
			TreeNode FNode = this.FindTreeNodeByFolder(Folder);
			FNode.ImageKey = "deleted";
            FNode.SelectedImageKey = "deleted";
            FNode.EnsureVisible();
		}


        /// <summary>
        /// Finally delete a folder (with security checks before)
        /// </summary>
        /// <param name="Folder"></param>
        /// <returns></returns>
		private bool SecureDelete(DirectoryInfo Folder)
		{
            if (!Directory.Exists(Folder.FullName))
                return false;

            this.CleanupFolder(Folder);

            // last security check (for files):
			if (Folder.GetFiles().Length == 0 && Folder.GetDirectories().Length == 0)
			{
                try
                {
                    Folder.Delete();
                    return true;
                }
                catch
                {
                    return false;
                }
			}
			return false;
		}

        
        /// <summary>
        /// This function deletes file trash (0 kb files or user given files)
        /// </summary>
        /// <param name="_StartFolder"></param>
        private void CleanupFolder(DirectoryInfo _StartFolder)
        {
            // Read files:
            FileInfo[] Files = _StartFolder.GetFiles();

            if (Files != null && Files.Length != 0)
            {
                // loop trough files and cancel if containsFiles == true
                for (int f = 0; f < Files.Length; f++)
                {
                    FileInfo file = Files[f];

                    bool matches_a_pattern = false;

                    for (int p = 0; (p < ignoreFiles.Length && !matches_a_pattern); p++)
                    {
                        string pattern = ignoreFiles[p];

                        if (this.cbIgnore0kbFiles.Checked && file.Length == 0)
                            matches_a_pattern = true;
                        else if (pattern.ToLower() == file.Name.ToLower())
                            matches_a_pattern = true;
                        else if (pattern.Contains("*"))
                        {
                            pattern = Regex.Escape(pattern);
                            pattern = pattern.Replace("\\*", ".*");

                            Regex RgxPattern = new Regex("^" + pattern + "$");

                            if (RgxPattern.IsMatch(file.Name))
                                matches_a_pattern = true;
                        }
                        else if (pattern.StartsWith("/") && pattern.EndsWith("/"))
                        {
                            Regex RgxPattern = new Regex(pattern.Substring(1, pattern.Length - 2));

                            if (RgxPattern.IsMatch(file.Name))
                                matches_a_pattern = true;
                        }

                    }

                    // If only one file is good, then stop.
                    if (matches_a_pattern)
                        file.Delete();

                }
            }
        }
 
        private String[] FormatAndSplit(string _pattern)
        {
            _pattern = _pattern.Replace("\r\n", "\n");
            _pattern = _pattern.Replace("\r", "\n");            
            return _pattern.Split('\n');
        }


        /// <summary>
        /// Part of the drag & drop functions 
        /// (you can drag a folder into RED)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void fMain_DragDrop(object sender, DragEventArgs e)
		{
			string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			if (s.Length == 1)
				this.tbFolder.Text = s[0];
			else
                MessageBox.Show(RED2.Properties.Resources.error_only_one_folder);
		}


        /// <summary>
        /// Part of the drag & drop functions 
        /// (you can drag a folder into RED)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void fMain_DragEnter(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.None;
				return;
			}
			else 
				e.Effect = DragDropEffects.Copy;			
		}

        #region Save setting functions

        private void cbKeepSystemFolders_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbKeepSystemFolders.Tag == null)
            {
                if (!this.cbKeepSystemFolders.Checked)
                {
                    if (MessageBox.Show(this, FixText(RED2.Properties.Resources.warning_really_delete), RED2.Properties.Resources.warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                        this.cbKeepSystemFolders.Checked = true;
                }

                this.settings.Write("keep_system_folders", this.cbKeepSystemFolders.Checked);
            }
            else
            {
                this.cbKeepSystemFolders.Tag = null;
            }
        }

        private void cbIgnoreHiddenFolders_CheckedChanged(object sender, EventArgs e)
        {
            this.settings.Write("dont_scan_hidden_folders", this.cbIgnoreHiddenFolders.Checked);
        }

        private void cbIgnore0kbFiles_CheckedChanged(object sender, EventArgs e)
        {
            this.settings.Write("ignore_0kb_files", this.cbIgnore0kbFiles.Checked);
        }

        private void nuMaxDepth_ValueChanged(object sender, EventArgs e)
        {
            this.settings.Write("max_depth", (int)this.nuMaxDepth.Value);
        }

        private void nuPause_ValueChanged(object sender, EventArgs e)
        {
            this.settings.Write("pause_between", (int)this.nuPause.Value);
        }

        private void tbIgnoreFiles_TextChanged(object sender, EventArgs e)
        {
            this.settings.Write("ignore_files", tbIgnoreFiles.Text);
        }

        private void tbIgnoreFolders_TextChanged(object sender, EventArgs e)
        {
            this.settings.Write("ignore_folders", tbIgnoreFolders.Text);
        }	 
        #endregion	

        #region Weblinks

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.sxc.hu/photo/593924");
        }

        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.jonasjohn.de/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.famfamfam.com/");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nuovext.pwsp.net/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://icon-king.com/?p=15");
        } 

        #endregion

        private void btnCheckForUpdates_Click(object sender, EventArgs e)
        {
            Assembly REDAssembly = Assembly.GetExecutingAssembly();
            AssemblyName AssemblyName = REDAssembly.GetName();

            string UpdateUrl = string.Format("http://www.jonasjohn.de/lab/check_update.php?p=red&version={0}", AssemblyName.Version.ToString());

            Process.Start(UpdateUrl);
        }

  
        private void tvFolders_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode TvNode = this.tvFolders.GetNodeAt(e.X, e.Y);
            this.tvFolders.SelectedNode = TvNode;        
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFolder();
        }

        private void protectFolderFromBeingDeletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode Node = this.tvFolders.SelectedNode;

            if (Node != null)
                ProtectNode(Node);
        }

        private void ProtectNode(TreeNode Node)
        {
            DirectoryInfo Folder = (DirectoryInfo)Node.Tag;

            if (!this.ProtectedFolders.ContainsKey(Folder.FullName))
                this.ProtectedFolders.Add(Folder.FullName, Node.ImageKey + "|" + Node.ForeColor.ToArgb().ToString());

            Node.ImageKey = "protected";
            Node.SelectedImageKey = "protected";
            Node.ForeColor = Color.Blue;

            if (Node.Parent != this.RootNode)            
                ProtectNode(Node.Parent);            

        }

        private void unprotectFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode Node = this.tvFolders.SelectedNode;

            if (Node != null)
                UnProtectNode(Node);
        }

        private void UnProtectNode(TreeNode Node)
        {          
            DirectoryInfo Folder = (DirectoryInfo)Node.Tag;

            if (this.ProtectedFolders.ContainsKey(Folder.FullName))
            {
                string[] BackupValues = ((string)this.ProtectedFolders[Folder.FullName]).Split('|');

                string ImageKey = BackupValues[0];
                Color NodeColor = Color.FromArgb(Int32.Parse(BackupValues[1]));

                Node.ImageKey = ImageKey;
                Node.SelectedImageKey = ImageKey;
                Node.ForeColor = NodeColor;

                this.ProtectedFolders.Remove(Folder.FullName);

                foreach (TreeNode SubNode in Node.Nodes) {
                    this.UnProtectNode(SubNode);                
                }

            }
        }

        private void proToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tcMain.SelectedIndex = 1;

            TreeNode Node = this.tvFolders.SelectedNode;
            DirectoryInfo Folder = (DirectoryInfo)Node.Tag;

            this.tbIgnoreFolders.Text += "\r\n" + Folder.FullName;
        }

        private void btnDonateMoney_Click(object sender, EventArgs e)
        {
            Process.Start("http://sourceforge.net/project/project_donations.php?group_id=209622");
        }


	}

}