using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using Alphaleonis.Win32.Filesystem;

namespace RED2
{
    public partial class MainWindow : Form
    {
        private REDCore core = null;
        private TreeManager tree = null;

        private RuntimeData Data = new RuntimeData();
        private Stopwatch runtimeWatch = new Stopwatch();

        #region Init methods

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

            Properties.Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
            Properties.Settings.Default.SettingChanging += new System.Configuration.SettingChangingEventHandler(Default_SettingChanging);

            this.init();
        }

        private void init()
        {
            this.tree = new TreeManager(this.tvFolders);
            this.tree.OnProtectionStatusChanged += new EventHandler<ProtectionStatusChangedEventArgs>(tree_OnProtectionStatusChanged);
            this.tree.OnDeleteRequest += new EventHandler<DeleteRequestFromTreeEventArgs>(tree_OnDeleteRequest);

            this.lbAppTitle.Text += string.Format("{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            #region Bind config settings to controls

            // Read folder from the config file
            this.tbFolder.DataBindings.Add("Text", Properties.Settings.Default, "last_used_directory");
			this.cbUpdateTree.DataBindings.Add("Checked", Properties.Settings.Default, "update_tree");

            this.cbIgnoreHiddenFolders.DataBindings.Add("Checked", Properties.Settings.Default, "dont_scan_hidden_folders");
            this.cbIgnore0kbFiles.DataBindings.Add("Checked", Properties.Settings.Default, "ignore_0kb_files");
            this.cbKeepSystemFolders.DataBindings.Add("Checked", Properties.Settings.Default, "keep_system_folders");
            this.cbClipboardDetection.DataBindings.Add("Checked", Properties.Settings.Default, "clipboard_detection");
            this.cbHideScanErrors.DataBindings.Add("Checked", Properties.Settings.Default, "hide_scan_errors");

            this.tbIgnoreFiles.DataBindings.Add("Text", Properties.Settings.Default, "ignore_files");
            this.tbIgnoreFolders.DataBindings.Add("Text", Properties.Settings.Default, "ignore_directories");

            this.nuMaxDepth.DataBindings.Add("Value", Properties.Settings.Default, "max_depth");
            this.nuInfiniteLoopDetectionCount.DataBindings.Add("Value", Properties.Settings.Default, "infinite_loop_detection_count");
            this.nuPause.DataBindings.Add("Value", Properties.Settings.Default, "pause_between");
            this.cbIgnoreErrors.DataBindings.Add("Checked", Properties.Settings.Default, "ignore_deletion_errors");

            // Special field
            this.lblRedStats.Text = String.Format(RED2.Properties.Resources.red_deleted, Properties.Settings.Default.delete_stats);

            // Delete mode
            foreach (var d in DeleteModeItem.GetList())
                this.cbDeleteMode.Items.Add(new DeleteModeItem(d));

            this.cbDeleteMode.DataBindings.Add("SelectedIndex", Properties.Settings.Default, "delete_mode");

            #region Check if the user started RED as admin

            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                var isIntegrated = SystemFunctions.IsRegKeyIntegratedIntoWindowsExplorer();

                this.btnExplorerIntegrate.Enabled = !isIntegrated;
                this.btnExplorerRemove.Enabled = isIntegrated;

                this.Text += " (Admin mode)";
            }
            else
            {
                this.lblReqAdmin.ForeColor = Color.Red;
                this.btnExplorerIntegrate.Enabled = false;
                this.btnExplorerRemove.Enabled = false;
            }

            #endregion

            var credits = Path.Combine(Application.StartupPath, "credits.txt");

            if (File.Exists(credits))
                this.tbCredits.AppendText(File.ReadAllText(credits));
            else
                this.tbCredits.AppendText("Error: Could not find the credits text file:" + Environment.NewLine + credits);

            this.nuFolderAge.DataBindings.Add("Value", Properties.Settings.Default, "min_folder_age_hours");

            #endregion

            this.lbStatus.Text = "";
			UpdateContextMenu(cmStrip, false);

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

                Properties.Settings.Default.last_used_directory = path;
            }

            #endregion
        }

        void Default_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            if (e.SettingName == "keep_system_folders" && !(bool)e.NewValue)
            {
                if (MessageBox.Show(this, SystemFunctions.ConvertLineBreaks(RED2.Properties.Resources.warning_really_delete), RED2.Properties.Resources.warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Save all settings
            Properties.Settings.Default.Save();
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
                    MessageBox.Show(this, RED2.Properties.Resources.error_dir_does_not_exist);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "The given directory caused a problem:" + Environment.NewLine + ex.Message, "RED error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.btnShowLog.Enabled = false;

            // TODO: necessary?
            // Properties.Settings.Default.Save();

            this.Data.StartFolder = selectedDirectory;
            updateDataObject();

            this.pbProgressStatus.Style = ProgressBarStyle.Marquee;

            this.tree.CreateRootNode(this.Data.StartFolder, DirectoryIcons.home);

            this.btnCancel.Enabled = true;
			UpdateContextMenu(cmStrip, false);

            this.Data.AddLogSpacer();
            setStatusAndLogMessage(RED2.Properties.Resources.searching_empty_folders);

            runtimeWatch.Reset();
            runtimeWatch.Start();

            this.core.SearchingForEmptyDirectories();
        }

        void core_OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.lbStatus.Text = (string)e.UserState;
        }

        void core_OnFoundEmptyDir(object sender, FoundEmptyDirInfoEventArgs e)
        {
            this.tree.AddOrUpdateDirectoryNode(e.Directory, e.Type, e.ErrorMessage);
        }

        void core_OnFoundFinishedScanForEmptyDirs(object sender, FinishedScanForEmptyDirsEventArgs e)
        {
            // Finished scan

            runtimeWatch.Stop();

            setStatusAndLogMessage(String.Format(RED2.Properties.Resources.found_x_empty_folders, e.EmptyFolderCount, e.FolderCount, runtimeWatch.Elapsed.Minutes, runtimeWatch.Elapsed.Seconds));

            this.btnDelete.Enabled = (e.EmptyFolderCount > 0);
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;
            this.pbProgressStatus.Maximum = e.EmptyFolderCount;
            this.pbProgressStatus.Minimum = 0;
            this.pbProgressStatus.Value = this.pbProgressStatus.Maximum;
            this.pbProgressStatus.Step = 5;

            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;
            this.btnShowLog.Enabled = true;
			UpdateContextMenu(cmStrip, true);

			this.tvFolders.ResumeLayout();
			this.tree.AddRootNode();
            this.tree.EnsureRootNodeIsVisible();

            this.btnScan.Text = RED2.Properties.Resources.btn_scan_again;
        }

        #endregion

        #region Step 2: Delete empty directories

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Data.AddLogSpacer();
            setStatusAndLogMessage(RED2.Properties.Resources.started_deletion_process);

            this.btnScan.Enabled = false;
            this.btnCancel.Enabled = true;
			UpdateContextMenu(cmStrip, false);
            this.btnDelete.Enabled = false;
            this.btnShowLog.Enabled = false;

            updateDataObject();

            runtimeWatch.Reset();
            runtimeWatch.Start();

            this.core.StartDeleteProcess();
        }

        private void updateDataObject()
        {
            this.Data.IgnoreAllErrors = Properties.Settings.Default.ignore_deletion_errors;
            this.Data.IgnoreFiles = Properties.Settings.Default.ignore_files;
            this.Data.IgnoreDirectoriesList = Properties.Settings.Default.ignore_directories;
            this.Data.IgnoreEmptyFiles = Properties.Settings.Default.ignore_0kb_files;
            this.Data.IgnoreHiddenFolders = Properties.Settings.Default.dont_scan_hidden_folders;
            this.Data.KeepSystemFolders = Properties.Settings.Default.keep_system_folders;
            this.Data.HideScanErrors = Properties.Settings.Default.hide_scan_errors;
            this.Data.MinFolderAgeHours = Properties.Settings.Default.min_folder_age_hours;
            this.Data.MaxDepth = (int)Properties.Settings.Default.max_depth;
            this.Data.InfiniteLoopDetectionCount = (int)Properties.Settings.Default.infinite_loop_detection_count;
            this.Data.DeleteMode = (DeleteModes)Properties.Settings.Default.delete_mode;
            this.Data.PauseTime = (int)Properties.Settings.Default.pause_between;
        }

        private void core_OnDeleteProcessChanged(object sender, DeleteProcessUpdateEventArgs e)
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

        private void core_OnDeleteError(object sender, DeletionErrorEventArgs e)
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

        private void core_OnDeleteProcessFinished(object sender, DeleteProcessFinishedEventArgs e)
        {
            runtimeWatch.Stop();

            setStatusAndLogMessage(String.Format(RED2.Properties.Resources.delete_process_finished, e.DeletedFolderCount, e.FailedFolderCount, e.ProtectedCount, runtimeWatch.Elapsed.Minutes, runtimeWatch.Elapsed.Seconds));

            this.pbProgressStatus.Value = this.pbProgressStatus.Maximum;

            this.btnDelete.Enabled = false;
            this.btnScan.Enabled = true;
            this.btnCancel.Enabled = false;
            this.btnShowLog.Enabled = true;

            Properties.Settings.Default.delete_stats += e.DeletedFolderCount;
            this.lblRedStats.Text = String.Format(RED2.Properties.Resources.red_deleted, Properties.Settings.Default.delete_stats);
        }

        #endregion

        #region Generic process events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.core.CancelCurrentProcess();
        }

        private void core_OnCancelled(object sender, EventArgs e)
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

        private void core_OnAborted(object sender, EventArgs e)
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

        private void core_OnError(object sender, ErrorEventArgs e)
        {
            this.pbProgressStatus.Style = ProgressBarStyle.Blocks;

            MessageBox.Show(this, "Error: " + e.Message, "RED error message");
        }

        #endregion

        #region Tree related methods

        /// <summary>
        /// User clicks twice on a folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFolders_DoubleClick(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(this.tree.GetSelectedFolderPath());
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(this.tree.GetSelectedFolderPath());
        }

        private void scanOnlyThisDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.last_used_directory = this.tree.GetSelectedFolderPath();
            this.btnScan.PerformClick();
        }

        private void protectFolderFromBeingDeletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tree.ProtectSelected();
        }

        private void unprotectFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tree.UnprotectSelected();
        }

        private void tree_OnProtectionStatusChanged(object sender, ProtectionStatusChangedEventArgs e)
        {
            if (e.Protected)
                this.core.AddProtectedFolder(e.Path);
            else
                this.core.RemoveProtected(e.Path);
        }

        private void proToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tvFolders.SelectedNode == null) return;
            this.tcMain.SelectedIndex = 1;

            // TODO: Find a better way...
            this.btnDelete.Enabled = false;
            this.tbIgnoreFolders.AppendText("\r\n" + ((DirectoryInfo)this.tvFolders.SelectedNode.Tag).FullName);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tree.DeleteSelectedDirectory();
        }

        private void tree_OnDeleteRequest(object sender, DeleteRequestFromTreeEventArgs e)
        {
            try
            {
                var deletePath = e.Directory;

                // To simplify the code here there is only the RecycleBinWithQuestion or simulate possible here
                // (all others will be ignored)
                SystemFunctions.ManuallyDeleteDirectory(deletePath, (DeleteModes)Properties.Settings.Default.delete_mode);

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

        #endregion

        #region General UI related functions

        private void setStatusAndLogMessage(string msg)
        {
            this.lbStatus.Text = msg;
            this.Data.AddLogMessage(msg);
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
                Properties.Settings.Default.last_used_directory = s[0].Trim();
            else
                MessageBox.Show(this, RED2.Properties.Resources.error_only_one_folder);
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

                    Properties.Settings.Default.last_used_directory = clipValue;
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
            Properties.Settings.Default.last_used_directory = SystemFunctions.ChooseDirectoryDialog(Properties.Settings.Default.last_used_directory);
        }

        private void btnShowConfig_Click(object sender, EventArgs e)
        {
            SystemFunctions.OpenDirectoryWithExplorer(Application.StartupPath);
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            var logWindow = new LogWindow();
            logWindow.SetLog(this.core.GetLogMessages());
            logWindow.ShowDialog();
            logWindow.Dispose();
        }

        #endregion

        #region Config and about dialog related

        private void btnResetConfig_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you really want to reset your current settings to the default values?\nYou will loose your current settings.", "Restore default settings", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.Reset();
            }
        }

        private void btnExplorerIntegrate_Click(object sender, EventArgs e)
        {
            SystemFunctions.AddOrRemoveRegKey(true);
            this.btnExplorerRemove.Enabled = true;
            this.btnExplorerIntegrate.Enabled = false;
        }

        private void btnExplorerRemove_Click(object sender, EventArgs e)
        {
            SystemFunctions.AddOrRemoveRegKey(false);
            this.btnExplorerRemove.Enabled = false;
            this.btnExplorerIntegrate.Enabled = true;
        }

        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.jonasjohn.de/lab/red.htm");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(string.Format("http://www.jonasjohn.de/lab/check_update.php?p=red&version={0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.jonasjohn.de/lab/red_feedback.htm");

        }

        private void btnCopyDebugInfo_Click(object sender, EventArgs e)
        {
            var info = new System.Text.StringBuilder();

            info.AppendLine("System info");
            info.Append("- RED Version: ");
            try
            {
                info.AppendLine(string.Format("{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            }
            catch (Exception ex)
            {
                info.AppendLine("Failed (" + ex.Message + ")");
            }

            info.Append("- Operating System: ");
            try
            {
                info.AppendLine(System.Environment.OSVersion.ToString());
            }
            catch (Exception ex)
            {
                info.AppendLine("Failed (" + ex.Message + ")");
            }

            info.Append("- Processor architecture: ");
            try
            {
                info.AppendLine(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
            }
            catch (Exception ex)
            {
                info.AppendLine("Failed (" + ex.Message + ")");
            }

            info.Append("- Is Administrator: ");
            try
            {
                var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                info.AppendLine((principal.IsInRole(WindowsBuiltInRole.Administrator) ? "Yes" : "No"));
            }
            catch (Exception ex)
            {
                info.AppendLine("Failed (" + ex.Message + ")");
            }

            info.AppendLine("");
            info.AppendLine("RED Config settings: ");
            try
            {
                foreach (System.Configuration.SettingsProperty setting in Properties.Settings.Default.Properties)
                {
                    var value = Properties.Settings.Default.PropertyValues[setting.Name].PropertyValue.ToString();

                    if (setting.Name == "ignore_files" || setting.Name == "ignore_directories")
                        value = value.Replace("\r", "").Replace("\n", "\\n");

                    info.AppendLine("- " + setting.Name + ": " + value);

                }
            }
            catch (Exception ex)
            {
                info.AppendLine("Failed (" + ex.Message + ")");
            }

            try
            {
                Clipboard.SetText(info.ToString(), TextDataFormat.Text);

                MessageBox.Show("Copied this text to your clipboard:" + Environment.NewLine + Environment.NewLine + info.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, could not copy the debug info into your clipboard because of this error: " + Environment.NewLine + ex.Message);
            }
        }

        #endregion

		private void cbUpdateTree_CheckedChanged(object sender, EventArgs e)
		{
			if (cbUpdateTree.Checked)
			{
				tree.UpdateUi = true;
				tvFolders.ResumeLayout();
				this.tree.AddRootNode();
			}
			else
			{
				tree.UpdateUi = false;
				tvFolders.SuspendLayout();
			}
		}

		private void cmStrip_Opening(object sender, CancelEventArgs e)
		{
			openFolderToolStripMenuItem.Enabled = tvFolders.SelectedNode != null;
		}

		/// <summary>
		/// Enables/disables all items in the context menu
		/// </summary>
		/// <param name="contextMenuStrip"></param>
		/// <param name="enable"></param>
		private void UpdateContextMenu(ContextMenuStrip contextMenuStrip, bool enable)
		{
			foreach (ToolStripItem item in contextMenuStrip.Items)
				item.Enabled = enable;
		}
    }
}