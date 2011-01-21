using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Reflection;

namespace RED2
{
    public partial class fMain : Form
    {
        private REDCore core = null;
        private TreeManager treeMan = null;

        private DirectoryInfo StartFolder = null;

        // Registry keys
        private string MenuName = "Folder\\shell\\{0}";
        private string Command = "Folder\\shell\\{0}\\command";

        private cSettings settings = null;

        private PictureBox picIcon = new PictureBox();
        private Label picLabel = new Label();

        private int deletedFolderCount = 0;

        #region Generic stuff

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
            this.core = new REDCore();

            // Attach events
            this.core.OnWorkflowStepChanged += new EventHandler<REDCoreWorkflowStepChangedEventArgs>(core_OnWorkflowStepChanged);
            this.core.OnError += new EventHandler<REDCoreErrorEventArgs>(core_OnError);
            this.core.OnCancelled += new EventHandler(core_OnCancelled);

            this.core.OnCalcDirWorkerFinished += new EventHandler<REDCoreCalcDirWorkerFinishedEventArgs>(core_OnCalcDirWorkerFinished);
            this.core.OnProgressChanged += new EventHandler<ProgressChangedEventArgs>(core_OnProgressChanged);
            this.core.OnFoundEmptyDir += new EventHandler<REDCoreFoundDirEventArgs>(core_OnFoundEmptyDir);
            this.core.OnFinishedScanForEmptyDirs += new EventHandler<REDCoreFinishedScanForEmptyDirsEventArgs>(core_OnFoundFinishedScanForEmptyDirs);
            this.core.OnDeleteProcessChanged += new EventHandler<REDCoreDeleteProcessUpdateEventArgs>(core_OnDeleteProcessChanged);
            this.core.OnDeleteProcessFinished += new EventHandler<REDCoreDeleteProcessFinishedEventArgs>(core_OnDeleteProcessFinished);

            this.core.init();
        }

        private void UI_Init()
        {
            treeMan = new TreeManager(this.tvFolders);

            Assembly REDAssembly = Assembly.GetExecutingAssembly();
            AssemblyName AssemblyName = REDAssembly.GetName();

            this.lbAppTitle.Text += string.Format("{0}", AssemblyName.Version.ToString());

            this.MenuName = String.Format(MenuName, RED2.Properties.Resources.registry_name);
            this.Command = String.Format(Command, RED2.Properties.Resources.registry_name);

            this.pbProgressStatus.Maximum = 100;
            this.pbProgressStatus.Minimum = 0;
            this.pbProgressStatus.Step = 5;

            #region Check for the registry key

            RegistryKey RegistryShellKey = Registry.ClassesRoot.OpenSubKey(MenuName);
            if (RegistryShellKey == null)
                this.cbIntegrateIntoWindowsExplorer.Checked = false;
            else
                this.cbIntegrateIntoWindowsExplorer.Checked = true;

            #endregion

            DrawDirectoryIcons();

            #region Read config settings

            // settings path:
            string configPath = Path.Combine(Application.StartupPath, RED2.Properties.Resources.config_file);

            // Settings object:
            settings = new cSettings(configPath);

            // Read folder from the config file
            this.tbFolder.Text = this.settings.Read("folder", RED2.Properties.Resources.folder);

            bool KeepSystemFolders = this.settings.Read("keep_system_folders", Boolean.Parse(RED2.Properties.Resources.keep_system_folders));
            this.cbKeepSystemFolders.Checked = KeepSystemFolders;

            this.cbIgnoreHiddenFolders.Checked = this.settings.Read("dont_scan_hidden_folders", Boolean.Parse(RED2.Properties.Resources.dont_scan_hidden_folders));

            this.cbIgnore0kbFiles.Checked = this.settings.Read("ignore_0kb_files", Boolean.Parse(RED2.Properties.Resources.ignore_0kb_files));

            this.nuMaxDepth.Value = (int)this.settings.Read("max_depth", Int32.Parse(RED2.Properties.Resources.max_depth));
            this.nuPause.Value = (int)this.settings.Read("pause_between", Int32.Parse(RED2.Properties.Resources.pause_between));

            this.tbIgnoreFiles.Text = this.settings.Read("ignore_files", Helpers_FixText(RED2.Properties.Resources.ignore_files));

            this.tbIgnoreFolders.Text = this.settings.Read("ignore_folders", Helpers_FixText(RED2.Properties.Resources.ignore_folders));

            deletedFolderCount = this.settings.Read("delete_stats", 0);

            UpdateDeletionStats(this.deletedFolderCount);

            #endregion

            #region Read and apply command line arguments

            string[] Arguments = Environment.GetCommandLineArgs();

            if (Arguments.Length > 1)
                this.tbFolder.Text = Arguments[1].ToString();

            #endregion

            this.attachOptions();
        }

        private void attachOptions()
        {
            foreach (Control cb in this.gpOptions.Controls)
            {
                if (cb is CheckBox)
                    ((CheckBox)cb).CheckedChanged += new EventHandler(Options_CheckedChanged);
            }
        }

        void Options_CheckedChanged(object sender, EventArgs e)
        {
            var cb = (CheckBox)sender;

            switch ((string)cb.Tag)
            {
                case "explorer_integration":
                    SystemFunctions.AddOrRemoveRegKey(this.cbIntegrateIntoWindowsExplorer.Checked, MenuName, Command);
                    break;

                case "ignore_0kb_files":
                    this.settings.Write("ignore_0kb_files", this.cbIgnore0kbFiles.Checked);
                    break;

                case "clipboard_detection":
                    break;

                case "keep_system_dirs":

                    if (!this.cbKeepSystemFolders.Checked)
                    {
                        if (MessageBox.Show(this, Helpers_FixText(RED2.Properties.Resources.warning_really_delete), RED2.Properties.Resources.warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                            this.cbKeepSystemFolders.Checked = true;
                    }

                    this.settings.Write("keep_system_folders", this.cbKeepSystemFolders.Checked);

                    break;

                case "ignore_hidden":
                    this.settings.Write("dont_scan_hidden_folders", this.cbIgnoreHiddenFolders.Checked);
                    break;

                case "recycle_bin":
                    break;

                case "simulate_deletion":
                    break;

                default:
                    break;
            }

        }

        #region Save setting functions

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

        private void DrawDirectoryIcons()
        {
            #region Set and display folder status icons

            Dictionary<string, string> Icons = new Dictionary<string, string>();

            Icons.Add("home", RED2.Properties.Resources.icon_root);
            Icons.Add("folder", RED2.Properties.Resources.icon_default);
            Icons.Add("folder_trash_files", RED2.Properties.Resources.icon_contains_trash);
            Icons.Add("folder_hidden", RED2.Properties.Resources.icon_hidden_folder);
            Icons.Add("folder_lock", RED2.Properties.Resources.icon_locked_folder);
            Icons.Add("folder_warning", RED2.Properties.Resources.icon_warning);
            Icons.Add("protected_icon", RED2.Properties.Resources.icon_protected_folder);
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
        }

        void core_OnWorkflowStepChanged(object sender, REDCoreWorkflowStepChangedEventArgs e)
        {
            switch (e.NewStep)
            {
                case REDWorkflowSteps.Init:
                    this.UI_Init();
                    break;

                case REDWorkflowSteps.StartingCalcDirCount:

                    // Update button states
                    this.btnCancel.Enabled = true;
                    this.btnScan.Enabled = false;
                    this.btnDelete.Enabled = false;

                    // Reset TreeView
                    this.tvFolders.Nodes.Clear();

                    // Activate progressbar
                    this.pbProgressStatus.Style = ProgressBarStyle.Marquee;

                    this.lbStatus.Text = RED2.Properties.Resources.scanning_folders;

                    break;

                case REDWorkflowSteps.StartSearchingForEmptyDirs:
                    this.lbStatus.Text = RED2.Properties.Resources.searching_empty_folders;
                    break;

                case REDWorkflowSteps.DeleteProcessRunning:

                    this.lbStatus.Text = RED2.Properties.Resources.rem_empty_folders;

                    this.btnScan.Enabled = false;
                    this.btnCancel.Enabled = true;

                    break;

                default:
                    break;
            }
        }

        void core_OnError(object sender, REDCoreErrorEventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;

            MessageBox.Show(RED2.Properties.Resources.error + "\n\n" + e.Message);
        }

        void core_OnCancelled(object sender, EventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;
            this.lbStatus.Text = RED2.Properties.Resources.process_cancelled;
            this.btnScan.Enabled = true;
            this.btnDelete.Enabled = false;
        }

        #endregion

        #region Step 1: Scan for empty directories

        /// <summary>
        /// Starts the Scan-Progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScan_Click(object sender, EventArgs e)
        {
            this.treeMan.Init();

            // Check given folder:
            DirectoryInfo Folder = new DirectoryInfo(this.tbFolder.Text);

            if (!Folder.Exists)
            {
                MessageBox.Show(RED2.Properties.Resources.error_dir_does_not_exist);
                return;
            }

            this.settings.Write("folder", Folder.FullName);

            this.StartFolder = Folder;

            this.core.CalculateDirectoryCount(Folder, (int)this.nuMaxDepth.Value);
        }

        void core_OnCalcDirWorkerFinished(object sender, REDCoreCalcDirWorkerFinishedEventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks; // = Stop

            // Set max value:
            this.pbProgressStatus.Maximum = e.MaxFolderCount + 1;

            this.treeMan.CreateRootNode(StartFolder, REDIcons.home);

            this.core.SearchingForEmptyDirectories(
                this.StartFolder,
                this.tbIgnoreFiles.Text, this.tbIgnoreFolders.Text, this.cbIgnore0kbFiles.Checked, this.cbIgnoreHiddenFolders.Checked, this.cbKeepSystemFolders.Checked,
                (int)this.nuMaxDepth.Value
            );
        }

        #endregion

        #region Step 2: Scan for empty directories

        void core_OnFoundFinishedScanForEmptyDirs(object sender, REDCoreFinishedScanForEmptyDirsEventArgs e)
        {
            // Finished scan

            this.lbStatus.Text = String.Format(RED2.Properties.Resources.found_x_empty_folders, e.EmptyFolderCount, e.FolderCount);

            if (e.EmptyFolderCount != 0)
                this.btnDelete.Enabled = true;

            this.pbProgressStatus.Maximum = e.EmptyFolderCount;
            this.pbProgressStatus.Minimum = 0;
            this.pbProgressStatus.Value = this.pbProgressStatus.Maximum;
            this.pbProgressStatus.Step = 5;

            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;

            this.treeMan.EnsureRootVis();

            this.btnScan.Text = RED2.Properties.Resources.btn_scan_again;
        }

        #endregion

        #region Step 3: Deletion process

        void core_OnDeleteProcessChanged(object sender, REDCoreDeleteProcessUpdateEventArgs e)
        {
            switch (e.Status)
            {
                case REDDirStatus.Deleted:
                    this.lbStatus.Text = String.Format(RED2.Properties.Resources.removing_empty_folders, (e.ProgressStatus + 1), e.FolderCount);
                    this.treeMan.UpdateItemIcon(e.Folder, REDIcons.deleted);
                    break;

                case REDDirStatus.Protected:
                    this.treeMan.UpdateItemIcon(e.Folder, REDIcons.protected_icon);
                    break;

                default:
                    this.treeMan.UpdateItemIcon(e.Folder, REDIcons.folder_warning);
                    break;
            }

            this.pbProgressStatus.Value = e.ProgressStatus;
        }

        void core_OnDeleteProcessFinished(object sender, REDCoreDeleteProcessFinishedEventArgs e)
        {
            this.pbProgressStatus.Value = this.pbProgressStatus.Maximum;

            this.btnDelete.Enabled = false;
            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;

            this.lbStatus.Text = String.Format(RED2.Properties.Resources.found_x_empty_folders, e.DeletedFolderCount, e.FailedFolderCount);

            #region Update delete stats

            this.deletedFolderCount += e.DeletedFolderCount;
            this.settings.Write("delete_stats", this.deletedFolderCount);
            UpdateDeletionStats(this.deletedFolderCount);

            #endregion
        }

        #endregion

        #region UI Stuff

        /// <summary>
        /// User hit's cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnScan.Enabled = false;
            this.btnCancel.Enabled = false;

            this.core.CancelCurrentProcess();
        }

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

        /// <summary>
        /// Deletes all empty folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.core.SimulateDeletion = this.cbSimulateDeletion.Checked;
            this.core.StartDelete(Helpers_FormatAndSplit(this.tbIgnoreFiles.Text), this.cbIgnore0kbFiles.Checked, (double)this.nuPause.Value, this.cbDeleteToRecycleBin.Checked);
        }

        void core_OnFoundEmptyDir(object sender, REDCoreFoundDirEventArgs e)
        {
            this.treeMan.UI_AddEmptyFolderToTreeView(e.Directory, true);
        }

        void core_OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Just update the progress bar
            this.lbStatus.Text = (string)e.UserState;
            this.pbProgressStatus.Value = e.ProgressPercentage;
        }

        private void proToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tcMain.SelectedIndex = 1;

            TreeNode Node = this.tvFolders.SelectedNode;
            DirectoryInfo Folder = (DirectoryInfo)Node.Tag;

            this.tbIgnoreFolders.Text += "\r\n" + Folder.FullName;
        }

        /// <summary>
        /// User clicks twice on a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFolders_DoubleClick(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(this.treeMan.GetSelectedFolderPath());
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

        private void fMain_Activated(object sender, EventArgs e)
        {
            // Detect paths in the clipboard

            if (this.cbClipboardDetection.Checked && Clipboard.ContainsText(TextDataFormat.Text))
            {
                var clipValue = Clipboard.GetText(TextDataFormat.Text);

                if (clipValue.Contains(":\\"))
                {
                    this.tbFolder.Text = clipValue;
                }
            }
        }

        private void tbFolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.tbFolder.SelectAll();
        }

        private void UpdateDeletionStats(int count)
        {
            this.lblRedStats.Text = String.Format(RED2.Properties.Resources.red_deleted, count);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Let the user select a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            this.tbFolder.Text = SystemFunctions.ChooseDirectoryDialog(this.tbFolder.Text);
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(this.treeMan.GetSelectedFolderPath());
        }

        #endregion



        #region Folder protection

        private void protectFolderFromBeingDeletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode Node = this.tvFolders.SelectedNode;

            if (Node != null)
                ProtectNode(Node);
        }

        private void ProtectNode(TreeNode Node)
        {
            DirectoryInfo Folder = (DirectoryInfo)Node.Tag;

            this.core.AddProtectedFolder(Folder.FullName, Node.ImageKey + "|" + Node.ForeColor.ToArgb().ToString());

            Node.ImageKey = "protected_icon";
            Node.SelectedImageKey = "protected_icon";
            Node.ForeColor = Color.Blue;

            if (!this.treeMan.IsRootNode(Node.Parent))
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
            DirectoryInfo selectedFolder = (DirectoryInfo)Node.Tag;

            if (this.core.ProtectedContainsKey(selectedFolder.FullName))
            {
                string[] BackupValues = this.core.GetProtectedNode(selectedFolder.FullName);

                string ImageKey = BackupValues[0];
                Color NodeColor = Color.FromArgb(Int32.Parse(BackupValues[1]));

                Node.ImageKey = ImageKey;
                Node.SelectedImageKey = ImageKey;
                Node.ForeColor = NodeColor;

                this.core.RemoveProtected(selectedFolder.FullName);

                foreach (TreeNode SubNode in Node.Nodes)
                {
                    this.UnProtectNode(SubNode);
                }

            }
        }

        #endregion

        #region Various Helpers

        private String[] Helpers_FormatAndSplit(string _pattern)
        {
            _pattern = _pattern.Replace("\r\n", "\n");
            _pattern = _pattern.Replace("\r", "\n");
            return _pattern.Split('\n');
        }

        public static string Helpers_FixText(string _str)
        {
            return _str.Replace(@"\r\n", "\r\n").Replace(@"\n", "\n");
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
    }

}