using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;

namespace RED2
{
    public partial class MainWindow : Form
    {
        private REDCore core = null;
        private TreeManager tree = null;
        private ConfigurationManger config = null;

        private RuntimeData data = null;

        #region Generic stuff

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
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
            this.config = new ConfigurationManger(Path.Combine(Application.StartupPath, RED2.Properties.Resources.config_file));
            this.config.OnSettingsSaved += new EventHandler(config_OnSettingsSaved);

            this.data = new RuntimeData();

            this.core = new REDCore(this, this.data);

            // Attach events
            this.core.OnWorkflowStepChanged += new EventHandler<REDCoreWorkflowStepChangedEventArgs>(core_OnWorkflowStepChanged);
            this.core.OnError += new EventHandler<REDCoreErrorEventArgs>(core_OnError);
            this.core.OnCancelled += new EventHandler(core_OnCancelled);
            this.core.OnAborted += new EventHandler(core_OnAborted);

            this.core.OnCalcDirWorkerFinished += new EventHandler<REDCoreCalcDirWorkerFinishedEventArgs>(core_OnCalcDirWorkerFinished);
            this.core.OnProgressChanged += new EventHandler<ProgressChangedEventArgs>(core_OnProgressChanged);
            this.core.OnFoundEmptyDir += new EventHandler<REDCoreFoundDirEventArgs>(core_OnFoundEmptyDir);
            this.core.OnFinishedScanForEmptyDirs += new EventHandler<REDCoreFinishedScanForEmptyDirsEventArgs>(core_OnFoundFinishedScanForEmptyDirs);
            this.core.OnDeleteProcessChanged += new EventHandler<REDCoreDeleteProcessUpdateEventArgs>(core_OnDeleteProcessChanged);
            this.core.OnDeleteProcessFinished += new EventHandler<REDCoreDeleteProcessFinishedEventArgs>(core_OnDeleteProcessFinished);
            this.core.OnDeleteError += new EventHandler<DeletionErrorEventArgs>(core_OnDeleteError);

            this.core.init();
        }

        void core_OnDeleteError(object sender, DeletionErrorEventArgs e)
        {
            var errorDialog = new DeletionError();

            errorDialog.SetPath(e.Path);
            errorDialog.SetErrorMessage(e.ErrorMessage);

            var dialogResult = errorDialog.ShowDialog();

            errorDialog.Dispose();

            if (dialogResult == DialogResult.Abort)
            {
                this.core.AbortDeletion();
            }
            else
            {
                // Hack: retry means -> ignore all errors
                if (dialogResult == DialogResult.Retry)
                    this.data.IgnoreAllErrors = true;

                this.core.ContinueDeletion();
            }
        }

        void config_OnSettingsSaved(object sender, EventArgs e)
        {
            this.lblRedStats.Text = String.Format(RED2.Properties.Resources.red_deleted, this.config.DeletedFolderCount);
        }

        private void Init()
        {
            this.tree = new TreeManager(this.tvFolders);
            this.tree.OnProtectionStatusChanged += new EventHandler<ProtectionStatusChangedEventArgs>(tree_OnProtectionStatusChanged);

            Assembly REDAssembly = Assembly.GetExecutingAssembly();
            AssemblyName AssemblyName = REDAssembly.GetName();

            this.lbAppTitle.Text += string.Format("{0}", AssemblyName.Version.ToString());

            #region Read config settings

            // Read folder from the config file
            this.config.AddControl("folder", this.tbFolder, RED2.Properties.Resources.folder);

            this.config.AddControl("dont_scan_hidden_folders", this.cbIgnoreHiddenFolders, RED2.Properties.Resources.dont_scan_hidden_folders);
            this.config.AddControl("ignore_0kb_files", this.cbIgnore0kbFiles, RED2.Properties.Resources.ignore_0kb_files);
            this.config.AddControl("keep_system_folders", this.cbKeepSystemFolders, RED2.Properties.Resources.keep_system_folders);

            this.config.AddControl("ignore_files", this.tbIgnoreFiles, RED2.Properties.Resources.ignore_files);
            this.config.AddControl("ignore_folders", this.tbIgnoreFolders, RED2.Properties.Resources.ignore_folders);

            this.config.AddControl("max_depth", this.nuMaxDepth, RED2.Properties.Resources.max_depth);
            this.config.AddControl("pause_between", this.nuPause, RED2.Properties.Resources.pause_between);
            this.config.AddControl("ignore_errors", this.cbIgnoreErrors, RED2.Properties.Resources.default_ignore_errors);

            // Special fields
            this.config.AddControl("delete_stats", this.lblRedStats, "0");
            this.config.AddControl("registry_explorer_integration", this.cbIntegrateIntoWindowsExplorer, "");
            this.config.AddControl("delete_mode", this.cbDeleteMode, RED2.Properties.Resources.default_delete_mode);

            foreach (var d in DeleteModeItem.GetList())
                this.cbDeleteMode.Items.Add(new DeleteModeItem(d));

            // settings path:
            this.config.LoadOptions();

            #endregion

            drawDirectoryIcons();

            #region Read and apply command line arguments

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
                this.tbFolder.Text = args[1].ToString();

            #endregion
        }

        void tree_OnProtectionStatusChanged(object sender, ProtectionStatusChangedEventArgs e)
        {
            if (e.Protected)
                this.core.AddProtectedFolder(e.Path);
            else
                this.core.RemoveProtected(e.Path);
        }
        
        private void drawDirectoryIcons()
        {
            #region Set and display folder status icons

            Dictionary<string, string> icons = new Dictionary<string, string>();

            icons.Add("home", RED2.Properties.Resources.icon_root);
            icons.Add("folder", RED2.Properties.Resources.icon_default);
            icons.Add("folder_trash_files", RED2.Properties.Resources.icon_contains_trash);
            icons.Add("folder_hidden", RED2.Properties.Resources.icon_hidden_folder);
            icons.Add("folder_lock", RED2.Properties.Resources.icon_locked_folder);
            icons.Add("folder_warning", RED2.Properties.Resources.icon_warning);
            icons.Add("protected_icon", RED2.Properties.Resources.icon_protected_folder);
            icons.Add("deleted", RED2.Properties.Resources.icon_deleted_folder);

            int xpos = 6;
            int ypos = 30;

            foreach (string key in icons.Keys)
            {
                Image Icon = (Image)this.ilFolderIcons.Images[key];

                PictureBox picIcon = new PictureBox();
                picIcon.Image = Icon;
                picIcon.Location = new System.Drawing.Point(xpos, ypos);
                picIcon.Name = "picIcon";
                picIcon.Size = new System.Drawing.Size(Icon.Width, Icon.Height);

                Label picLabel = new Label();
                picLabel.Text = icons[key];
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
                case WorkflowSteps.Init:

                    this.Init();

                    this.lbStatus.Text = "";

                    this.pbProgressStatus.Maximum = 100;
                    this.pbProgressStatus.Minimum = 0;
                    this.pbProgressStatus.Step = 5;

                    this.btnShowLog.Enabled = false;

                    break;

                case WorkflowSteps.StartingCalcDirCount:

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

                case WorkflowSteps.StartSearchingForEmptyDirs:
                    this.lbStatus.Text = RED2.Properties.Resources.searching_empty_folders;
                    break;

                case WorkflowSteps.DeleteProcessRunning:

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
            this.btnCancel.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnShowLog.Enabled = true;
        }

        void core_OnAborted(object sender, EventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;
            this.lbStatus.Text = RED2.Properties.Resources.process_aborted;
            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnShowLog.Enabled = true;
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
            this.tree.Clear();

            this.btnShowLog.Enabled = false;

            // Check given folder:
            DirectoryInfo selectedDirectory = new DirectoryInfo(this.tbFolder.Text);

            if (!selectedDirectory.Exists)
            {
                MessageBox.Show(RED2.Properties.Resources.error_dir_does_not_exist);
                return;
            }

            this.config.Save();

            this.data.StartFolder = selectedDirectory;
            this.data.MaxDepth = (int)this.nuMaxDepth.Value;
            this.data.IgnoreAllErrors = this.cbIgnoreErrors.Checked;

            this.core.CalculateDirectoryCount();
        }

        void core_OnCalcDirWorkerFinished(object sender, REDCoreCalcDirWorkerFinishedEventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks; // = Stop

            // Set max value:
            this.pbProgressStatus.Maximum = e.MaxFolderCount + 1;

            this.tree.CreateRootNode(this.data.StartFolder, DirectoryIcons.home);

            this.data.IgnoreFiles = this.tbIgnoreFiles.Text;
            this.data.IgnoreFolders = this.tbIgnoreFolders.Text;
            this.data.Ignore0kbFiles = this.cbIgnore0kbFiles.Checked;
            this.data.IgnoreHiddenFolders = this.cbIgnoreHiddenFolders.Checked;
            this.data.KeepSystemFolders = this.cbKeepSystemFolders.Checked;
            this.data.MaxDepth = (int)this.nuMaxDepth.Value;

            this.core.SearchingForEmptyDirectories();
        }

        #endregion

        #region Step 2: Scan for empty directories

        void core_OnFoundFinishedScanForEmptyDirs(object sender, REDCoreFinishedScanForEmptyDirsEventArgs e)
        {
            // Finished scan

            this.lbStatus.Text = String.Format(RED2.Properties.Resources.found_x_empty_folders, e.EmptyFolderCount, e.FolderCount);

            if (e.EmptyFolderCount != 0)
                this.btnDelete.Enabled = true;

            this.pbProgressStatus.Maximum = e.EmptyFolderCount + 1;
            this.pbProgressStatus.Minimum = 0;
            this.pbProgressStatus.Value = this.pbProgressStatus.Maximum;
            this.pbProgressStatus.Step = 5;

            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;

            this.tree.EnsureRootNodeIsVisible();

            this.btnScan.Text = RED2.Properties.Resources.btn_scan_again;
        }

        #endregion

        #region Step 3: Deletion process

        void core_OnDeleteProcessChanged(object sender, REDCoreDeleteProcessUpdateEventArgs e)
        {
            switch (e.Status)
            {
                case DirectoryStatusTypes.Deleted:
                    this.lbStatus.Text = String.Format(RED2.Properties.Resources.removing_empty_folders, (e.ProgressStatus + 1), e.FolderCount);
                    this.tree.UpdateItemIcon(e.Folder, DirectoryIcons.deleted);
                    break;

                case DirectoryStatusTypes.Protected:
                    this.tree.UpdateItemIcon(e.Folder, DirectoryIcons.protected_icon);
                    break;

                default:
                    this.tree.UpdateItemIcon(e.Folder, DirectoryIcons.folder_warning);
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
            this.btnShowLog.Enabled = true;

            this.lbStatus.Text = String.Format(RED2.Properties.Resources.found_x_empty_folders, e.DeletedFolderCount, e.FailedFolderCount);

            this.config.DeletedFolderCount += e.DeletedFolderCount;
            this.config.Save();
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
            this.core.CancelCurrentProcess();
        }

        /// <summary>
        /// Deletes all empty folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.data.IgnoreFiles = this.tbIgnoreFiles.Text;
            this.data.Ignore0kbFiles = this.cbIgnore0kbFiles.Checked;
            this.data.PauseTime = (double)this.nuPause.Value;
            this.data.DeleteMode = ((DeleteModeItem)this.cbDeleteMode.SelectedItem).DeleteMode;
            this.data.IgnoreAllErrors = this.cbIgnoreErrors.Checked;

            this.core.DoDelete();
        }

        void core_OnFoundEmptyDir(object sender, REDCoreFoundDirEventArgs e)
        {
            this.tree.AddEmptyFolderToTreeView(e.Directory, true);
        }

        void core_OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
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
            SystemFunctions.OpenDirectoryWithExplorer(this.tree.GetSelectedFolderPath());
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
                e.Effect = DragDropEffects.None;
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
                    this.tbFolder.Text = clipValue;
            }
        }

        private void tbFolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.tbFolder.SelectAll();
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
            SystemFunctions.OpenDirectoryWithExplorer(this.tree.GetSelectedFolderPath());
        }

        private void btnShowConfig_Click(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(Application.StartupPath);
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            var logWindow = new LogWindow();
            logWindow.SetLog(this.core.GetLog());
            logWindow.ShowDialog();
            logWindow.Dispose();
        }

        #endregion

        #region Folder protection

        private void protectFolderFromBeingDeletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tree.ProtectSelected();
        }

        private void unprotectFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tree.UnprotectSelected();
        }

        #endregion

        #region Various functions

 
        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.jonasjohn.de/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Assembly REDAssembly = Assembly.GetExecutingAssembly();
            AssemblyName AssemblyName = REDAssembly.GetName();

            Process.Start(string.Format("http://www.jonasjohn.de/lab/check_update.php?p=red&version={0}", AssemblyName.Version.ToString()));
        }

        #endregion


   
    }
}