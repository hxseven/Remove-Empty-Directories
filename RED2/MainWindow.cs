using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace RED2
{
    public partial class MainWindow : Form
    {
        private REDCore core = null;
        private TreeManager tree = null;
        private ConfigurationManger config = null;

        private RuntimeData Data = null;

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

            this.Data = new RuntimeData();

            this.core = new REDCore(this, this.Data);

            // Attach events
            this.core.OnError += new EventHandler<ErrorEventArgs>(core_OnError);
            this.core.OnCancelled += new EventHandler(core_OnCancelled);
            this.core.OnAborted += new EventHandler(core_OnAborted);

            this.core.OnProgressChanged += new EventHandler<ProgressChangedEventArgs>(core_OnProgressChanged);
            this.core.OnFoundEmptyDirectory += new EventHandler<FoundEmptyDirInfoEventArgs>(core_OnFoundEmptyDir);
            this.core.OnFinishedScanForEmptyDirs += new EventHandler<FinishedScanForEmptyDirsEventArgs>(core_OnFoundFinishedScanForEmptyDirs);
            this.core.OnDeleteProcessChanged += new EventHandler<DeleteProcessUpdateEventArgs>(core_OnDeleteProcessChanged);
            this.core.OnDeleteProcessFinished += new EventHandler<DeleteProcessFinishedEventArgs>(core_OnDeleteProcessFinished);
            this.core.OnDeleteError += new EventHandler<DeletionErrorEventArgs>(core_OnDeleteError);

            this.init();
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
                    this.Data.IgnoreAllErrors = true;

                this.core.ContinueDeleteProcess();
            }
        }

        void config_OnSettingsSaved(object sender, EventArgs e)
        {
            // Update deletion stats
            this.lblRedStats.Text = String.Format(RED2.Properties.Resources.red_deleted, this.config.DeletedFolderCount);
        }

        private void init()
        {
            this.tree = new TreeManager(this.tvFolders);
            this.tree.OnProtectionStatusChanged += new EventHandler<ProtectionStatusChangedEventArgs>(tree_OnProtectionStatusChanged);
            this.tree.OnDeleteRequest += new EventHandler<DeleteRequestFromTreeEventArgs>(tree_OnDeleteRequest);

            Assembly REDAssembly = Assembly.GetExecutingAssembly();
            AssemblyName AssemblyName = REDAssembly.GetName();

            this.lbAppTitle.Text += string.Format("{0}", AssemblyName.Version.ToString());

            #region Read config settings

            // Read folder from the config file
            this.config.AddControl("folder", this.tbFolder, RED2.Properties.Resources.folder);

            this.config.AddControl("dont_scan_hidden_folders", this.cbIgnoreHiddenFolders, RED2.Properties.Resources.dont_scan_hidden_folders);
            this.config.AddControl("ignore_0kb_files", this.cbIgnore0kbFiles, RED2.Properties.Resources.default_ignore_0kb_files);
            this.config.AddControl("keep_system_folders", this.cbKeepSystemFolders, RED2.Properties.Resources.default_keep_system_folders);
            this.config.AddControl("clipboard_detection", this.cbClipboardDetection, RED2.Properties.Resources.default_clipboard_detection);

            this.config.AddControl("ignore_files", this.tbIgnoreFiles, RED2.Properties.Resources.ignore_files);
            this.config.AddControl("ignore_folders", this.tbIgnoreFolders, RED2.Properties.Resources.ignore_folders);

            this.config.AddControl("max_depth", this.nuMaxDepth, RED2.Properties.Resources.default_max_depth);
            this.config.AddControl("infinite_loop_detection_count", this.nuInfiniteLoopDetectionCount, RED2.Properties.Resources.default_infinite_loop_detection_count);
            this.config.AddControl("pause_between", this.nuPause, RED2.Properties.Resources.default_pause_between);
            this.config.AddControl("ignore_errors", this.cbIgnoreErrors, RED2.Properties.Resources.default_ignore_errors);

            // Special fields
            this.config.AddControl("delete_stats", this.lblRedStats, "0");
            this.config.AddControl("registry_explorer_integration", this.cbIntegrateIntoWindowsExplorer, "");
            this.config.AddControl("delete_mode", this.cbDeleteMode, RED2.Properties.Resources.default_delete_mode);

            foreach (var d in DeleteModeItem.GetList())
                this.cbDeleteMode.Items.Add(new DeleteModeItem(d));

            // settings path:
            this.config.LoadOptions();

            this.cbKeepSystemFolders.CheckedChanged += new System.EventHandler(this.cbKeepSystemFolders_CheckedChanged);

            #endregion

            this.lbStatus.Text = "";
            this.cmStrip.Enabled = false;

            this.pbProgressStatus.Maximum = 100;
            this.pbProgressStatus.Minimum = 0;
            this.pbProgressStatus.Step = 5;

            this.btnShowLog.Enabled = false;

            drawDirectoryIcons();

            #region Read and apply command line arguments

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                args[0] = "";
                var path = String.Join("", args).Replace("\"", "").Trim();

                // add ending backslash
                if (!path.EndsWith("\\")) path += "\\";

                this.tbFolder.Text = path;
            }

            #endregion
        }

        void tree_OnDeleteRequest(object sender, DeleteRequestFromTreeEventArgs e)
        {
            try
            {
                var deletePath = e.Directory;

                // To simplify the code here there is only the RecycleBinWithQuestion or simulate possible here
                // (all others will be ignored)
                SystemFunctions.ManuallyDeleteDirectory(deletePath, ((DeleteModeItem)this.cbDeleteMode.SelectedItem).DeleteMode);

                // Remove root node
                this.tree.RemoveNode(deletePath);

                this.Data.AddLogMessage("Manually deleted: \"" + deletePath + "\" including all subdirectories");

                // Disable the delete button because the user has to re-scan after he manually deleted a directory
                this.btnDelete.Enabled = false;

            }
            catch (System.OperationCanceledException)
            {
                // The user canceled the deletion 
            }
            catch (Exception ex)
            {
                this.Data.AddLogMessage("Could not manually delete \"" + e.Directory + "\" because of the following error: " + ex.Message);

                MessageBox.Show(this, "The directory was not deleted, because of the following error:" + Environment.NewLine + ex.Message);
            }
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

        void core_OnError(object sender, ErrorEventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;

            MessageBox.Show(this, "Error: " + e.Message, "RED error");
        }

        void core_OnCancelled(object sender, EventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;

            if (this.core.CurrentProcessStep == WorkflowSteps.DeleteProcessRunning)
                setStatusAndLogMessage(RED2.Properties.Resources.deletion_aborted);
            else
                setStatusAndLogMessage(RED2.Properties.Resources.process_cancelled);

            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnShowLog.Enabled = true;
        }

        void core_OnAborted(object sender, EventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;

            if (this.core.CurrentProcessStep == WorkflowSteps.DeleteProcessRunning)
                setStatusAndLogMessage(RED2.Properties.Resources.deletion_aborted);
            else
                setStatusAndLogMessage(RED2.Properties.Resources.process_aborted);

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
            this.tree.ClearTree();

            // Check given folder:
            DirectoryInfo selectedDirectory = null;
            try
            {
                selectedDirectory = new DirectoryInfo(this.tbFolder.Text);

                if (!selectedDirectory.Exists)
                {
                    MessageBox.Show(RED2.Properties.Resources.error_dir_does_not_exist);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "The given directory caused a problem:" + Environment.NewLine + ex.Message, "RED error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.btnShowLog.Enabled = false;

            this.config.Save();

            this.Data.StartFolder = selectedDirectory;
            this.Data.MaxDepth = (int)this.nuMaxDepth.Value;
            this.Data.IgnoreAllErrors = this.cbIgnoreErrors.Checked;
            this.Data.IgnoreFiles = this.tbIgnoreFiles.Text;
            this.Data.IgnoreDirectoriesList = this.tbIgnoreFolders.Text;
            this.Data.IgnoreEmptyFiles = this.cbIgnore0kbFiles.Checked;
            this.Data.IgnoreHiddenFolders = this.cbIgnoreHiddenFolders.Checked;
            this.Data.KeepSystemFolders = this.cbKeepSystemFolders.Checked;
            this.Data.MaxDepth = (int)this.nuMaxDepth.Value;
            this.Data.InfiniteLoopDetectionCount = (int)this.nuInfiniteLoopDetectionCount.Value;

            //this.pbProgressStatus.Style = ProgressBarStyle.Blocks; // = Stop
            this.pbProgressStatus.Style = ProgressBarStyle.Marquee;

            this.tree.AddRootNode(this.Data.StartFolder, DirectoryIcons.home);

            this.btnCancel.Enabled = true;

            this.Data.AddLogSpacer();
            setStatusAndLogMessage(RED2.Properties.Resources.searching_empty_folders);

            this.core.SearchingForEmptyDirectories();
        }

        private void setStatusAndLogMessage(string msg)
        {
            this.lbStatus.Text = msg;
            this.Data.AddLogMessage(msg);
        }

        #endregion

        #region Step 2: Scan for empty directories

        void core_OnFoundFinishedScanForEmptyDirs(object sender, FinishedScanForEmptyDirsEventArgs e)
        {
            // Finished scan

            setStatusAndLogMessage(String.Format(RED2.Properties.Resources.found_x_empty_folders, e.EmptyFolderCount, e.FolderCount));

            this.btnDelete.Enabled = (e.EmptyFolderCount > 0);
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;
            this.pbProgressStatus.Maximum = e.EmptyFolderCount;
            this.pbProgressStatus.Minimum = 0;
            this.pbProgressStatus.Value = this.pbProgressStatus.Maximum;
            this.pbProgressStatus.Step = 5;

            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;
            this.btnShowLog.Enabled = true;
            this.cmStrip.Enabled = true;

            this.tree.EnsureRootNodeIsVisible();

            this.btnScan.Text = RED2.Properties.Resources.btn_scan_again;
        }

        #endregion

        #region Step 3: Deletion process

        void core_OnDeleteProcessChanged(object sender, DeleteProcessUpdateEventArgs e)
        {
            switch (e.Status)
            {
                case DirectoryDeletionStatusTypes.Deleted:
                    this.lbStatus.Text = String.Format(RED2.Properties.Resources.removing_empty_folders, (e.ProgressStatus + 1), e.FolderCount);
                    this.tree.UpdateItemIcon(e.Path, DirectoryIcons.deleted);
                    break;

                case DirectoryDeletionStatusTypes.Protected:
                    this.tree.UpdateItemIcon(e.Path, DirectoryIcons.protected_icon);
                    break;

                default:
                    this.tree.UpdateItemIcon(e.Path, DirectoryIcons.folder_warning);
                    break;
            }

            this.pbProgressStatus.Value = e.ProgressStatus;
        }

        void core_OnDeleteProcessFinished(object sender, DeleteProcessFinishedEventArgs e)
        {
            this.pbProgressStatus.Value = this.pbProgressStatus.Maximum;

            this.btnDelete.Enabled = false;
            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;
            this.btnShowLog.Enabled = true;

            setStatusAndLogMessage(String.Format(RED2.Properties.Resources.delete_process_finished, e.DeletedFolderCount, e.FailedFolderCount));

            this.config.DeletedFolderCount += e.DeletedFolderCount;
            this.config.Save();
        }

        #endregion

        #region UI Stuff

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.core.CancelCurrentProcess();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Data.AddLogSpacer();
            setStatusAndLogMessage(RED2.Properties.Resources.started_deletion_process);

            this.btnScan.Enabled = false;
            this.btnCancel.Enabled = true;
            this.cmStrip.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnShowLog.Enabled = false;

            this.Data.IgnoreFiles = this.tbIgnoreFiles.Text;
            this.Data.IgnoreEmptyFiles = this.cbIgnore0kbFiles.Checked;
            this.Data.PauseTime = (double)this.nuPause.Value;
            this.Data.DeleteMode = ((DeleteModeItem)this.cbDeleteMode.SelectedItem).DeleteMode;
            this.Data.IgnoreAllErrors = this.cbIgnoreErrors.Checked;

            this.core.StartDeleteProcess();
        }

        void core_OnFoundEmptyDir(object sender, FoundEmptyDirInfoEventArgs e)
        {
            this.tree.AddOrUpdateDirectoryNode(e.Directory, e.Type, e.ErrorMessage);
        }

        void core_OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lbStatus.Text = (string)e.UserState;
        }

        private void proToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tvFolders.SelectedNode == null) return;
            this.tcMain.SelectedIndex = 1;

            // TODO: Find a better way...
            this.btnDelete.Enabled = false;
            this.tbIgnoreFolders.AppendText("\r\n" + ((DirectoryInfo)this.tvFolders.SelectedNode.Tag).FullName);
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

                if (clipValue.Contains(":\\") && !clipValue.Contains("\n"))
                {
                    // add ending backslash
                    if (!clipValue.EndsWith("\\")) clipValue += "\\";

                    this.tbFolder.Text = clipValue;
                }
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tree.DeleteSelectedDirectory();
        }

        #endregion

        #region Various functions

        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.jonasjohn.de/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Format("http://www.jonasjohn.de/lab/check_update.php?p=red&version={0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
        }

        #endregion

        private void cbKeepSystemFolders_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.cbKeepSystemFolders.Checked)
            {
                if (MessageBox.Show(this, SystemFunctions.FixLineBreaks(RED2.Properties.Resources.warning_really_delete), RED2.Properties.Resources.warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                    this.cbKeepSystemFolders.Checked = true;
            }
        }

        private void btnResetConfig_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you really want to reset your current settings to the default values?\nYour settings can't be restored.", "Restore default settings", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK) {
                File.Delete(Path.Combine(Application.StartupPath, RED2.Properties.Resources.config_file));

                // Todo: Find better way
                MessageBox.Show("Your current config file has been deleted.\nPlease restart RED now to load the default values.");
            }
        }
    }
}