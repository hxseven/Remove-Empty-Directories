using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Text;

namespace RED2
{
    public class REDCore
    {
        private WorkflowSteps currentProcessStep = WorkflowSteps.Init;

        private List<DirectoryInfo> emptyFolderList = null;
        private bool stopDeleteProcessTrigger = false;
        private Dictionary<String, String> protectedFolderList = new Dictionary<string, string>();

        private CalculateDirectoryCountWorker calcDirCountWorker = null;
        private FindEmptyDirectoryWorker searchEmptyFoldersWorker = null;
        private StringBuilder log = new StringBuilder();
        private bool ignoreAllErrors = false;

        // Events
        public event EventHandler<REDCoreWorkflowStepChangedEventArgs> OnWorkflowStepChanged;
        public event EventHandler<REDCoreErrorEventArgs> OnError;
        public event EventHandler<REDCoreCalcDirWorkerFinishedEventArgs> OnCalcDirWorkerFinished;
        public event EventHandler OnCancelled;
        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<REDCoreFoundDirEventArgs> OnFoundEmptyDir;
        public event EventHandler<REDCoreFinishedScanForEmptyDirsEventArgs> OnFinishedScanForEmptyDirs;
        public event EventHandler<REDCoreDeleteProcessUpdateEventArgs> OnDeleteProcessChanged;
        public event EventHandler<REDCoreDeleteProcessFinishedEventArgs> OnDeleteProcessFinished;

        // Public settings
        public DeleteModes DeleteMode { get; set; }

        public REDCore()
        {
        }

        public void init()
        {
            this.log = new StringBuilder();
            this.protectedFolderList = new Dictionary<string, string>();
            this.set_step(WorkflowSteps.Init);
        }

        public void CalculateDirectoryCount(DirectoryInfo Folder, int MaxDepth)
        {
            emptyFolderList = new List<DirectoryInfo>();

            this.set_step(WorkflowSteps.StartingCalcDirCount);

            // Create new blank worker
            this.calcDirCountWorker = new CalculateDirectoryCountWorker();
            this.calcDirCountWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FSWorker_RunWorkerCompleted);
            this.calcDirCountWorker.MaxDepth = MaxDepth;
            this.calcDirCountWorker.RunWorkerAsync(Folder);
        }

        private void set_step(WorkflowSteps step)
        {
            if (this.OnWorkflowStepChanged != null)
                this.OnWorkflowStepChanged(this, new REDCoreWorkflowStepChangedEventArgs(step));
        }

        /// <summary>
        /// Scan process finished
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FSWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CalculateDirectoryCountWorker LWorker = sender as CalculateDirectoryCountWorker;

            calcDirCountWorker.Dispose();
            calcDirCountWorker = null;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                showErrorMsg(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                if (this.OnCancelled != null)
                    this.OnCancelled(this, new EventArgs());
            }
            else
            {
                if (this.OnCalcDirWorkerFinished != null)
                    this.OnCalcDirWorkerFinished(this, new REDCoreCalcDirWorkerFinishedEventArgs((int)e.Result));
            }
        }

        private void showErrorMsg(string errorMessage)
        {
            if (this.OnError != null)
                this.OnError(this, new REDCoreErrorEventArgs(errorMessage));
        }

        /// <summary>
        /// Start searching empty folders
        /// </summary>
        public void SearchingForEmptyDirectories(DirectoryInfo StartFolder, string tbIgnoreFiles, string tbIgnoreFolders, bool cbIgnore0kbFiles, bool cbIgnoreHiddenFolders, bool cbKeepSystemFolders, int nuMaxDepth)
        {
            #region Initialize the FolderFindWorker-Object

            this.emptyFolderList = new List<DirectoryInfo>();

            this.set_step(WorkflowSteps.StartSearchingForEmptyDirs);

            searchEmptyFoldersWorker = new FindEmptyDirectoryWorker();
            searchEmptyFoldersWorker.ProgressChanged += new ProgressChangedEventHandler(FFWorker_ProgressChanged);
            searchEmptyFoldersWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FFWorker_RunWorkerCompleted);

            // set options:
            if (!searchEmptyFoldersWorker.SetIgnoreFiles(tbIgnoreFiles))
            {
                showErrorMsg(RED2.Properties.Resources.error_ignore_settings);
                return;
            }

            if (!searchEmptyFoldersWorker.SetIgnoreFolders(tbIgnoreFolders))
            {
                showErrorMsg(RED2.Properties.Resources.error_ignore_settings);
                return;
            }

            searchEmptyFoldersWorker.Ignore0kbFiles = cbIgnore0kbFiles;
            searchEmptyFoldersWorker.IgnoreHiddenFolders = cbIgnoreHiddenFolders;
            searchEmptyFoldersWorker.IgnoreSystemFolders = cbKeepSystemFolders;

            searchEmptyFoldersWorker.MaxDepth = nuMaxDepth;

            // Start worker
            searchEmptyFoldersWorker.RunWorkerAsync(StartFolder);

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
                if (this.OnProgressChanged != null)
                    this.OnProgressChanged(this, e);
            }
            else
            {
                var directory = (DirectoryInfo)e.UserState;

                emptyFolderList.Add(directory);

                if (this.OnFoundEmptyDir != null)
                    this.OnFoundEmptyDir(this, new REDCoreFoundDirEventArgs(directory));
            }
        }

        void FFWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FindEmptyDirectoryWorker FindWorker = sender as FindEmptyDirectoryWorker;

            if (e.Error != null)
            {
                this.showErrorMsg(e.Error.Message);

                searchEmptyFoldersWorker.Dispose();
                searchEmptyFoldersWorker = null;
                return;
            }
            else if (e.Cancelled)
            {
                if (this.OnCancelled != null)
                    this.OnCancelled(this, new EventArgs());
            }
            else
            {
                int EmptyFolderCount = FindWorker.EmptyFolderCount;
                int FolderCount = FindWorker.FolderCount;

                searchEmptyFoldersWorker.Dispose();
                searchEmptyFoldersWorker = null;

                if (this.OnFinishedScanForEmptyDirs != null)
                    this.OnFinishedScanForEmptyDirs(this, new REDCoreFinishedScanForEmptyDirsEventArgs(EmptyFolderCount, FolderCount));
            }
        }

        /// <summary>
        /// Finally delete a folder (with security checks before)
        /// </summary>
        /// <param name="emptyDirectory"></param>
        /// <returns></returns>
        private void secureDelete(DirectoryInfo emptyDirectory, String[] ignoreFileList, bool ignore_0kb_files)
        {
            if (!Directory.Exists(emptyDirectory.FullName))
                throw new Exception("Directory does not exist anymore?");

            // Cleanup folder

            FileInfo[] Files = emptyDirectory.GetFiles();
            Regex regexPattern = null;

            if (Files != null && Files.Length != 0)
            {
                this.log.AppendLine(String.Format("Cleaning directory: \"{0}\"", emptyDirectory.FullName));

                // loop trough files and cancel if containsFiles == true
                for (int f = 0; f < Files.Length; f++)
                {
                    FileInfo file = Files[f];

                    bool deleteTrashFile = false;
                    string delPattern = "";

                    for (int p = 0; (p < ignoreFileList.Length && !deleteTrashFile); p++)
                    {
                        var pattern = ignoreFileList[p];

                        if (ignore_0kb_files && file.Length == 0)
                        {
                            delPattern = "[empty file (0 KB)]";
                            deleteTrashFile = true;
                        }
                        else if (pattern.ToLower() == file.Name.ToLower())
                        {
                            delPattern = pattern;
                            deleteTrashFile = true;
                        }
                        else if (pattern.Contains("*"))
                        {
                            pattern = Regex.Escape(pattern);
                            pattern = pattern.Replace("\\*", ".*");

                            regexPattern = new Regex("^" + pattern + "$");

                            if (regexPattern.IsMatch(file.Name))
                            {
                                delPattern = pattern;
                                deleteTrashFile = true;
                            }
                        }
                        else if (pattern.StartsWith("/") && pattern.EndsWith("/"))
                        {
                            regexPattern = new Regex(pattern.Substring(1, pattern.Length - 2));

                            if (regexPattern.IsMatch(file.Name))
                            {
                                delPattern = pattern;
                                deleteTrashFile = true;
                            }
                        }
                    }

                    // If only one file is good, then stop.
                    if (deleteTrashFile)
                    {
                        this.log.AppendLine(String.Format(" -> Deleted file \"{0}\" because it matched the pattern \"{1}\"", file.FullName, delPattern));

                        SystemFunctions.SecureDeleteFile(file, this.DeleteMode);
                    }
                }
            }

            // End cleanup

            this.log.AppendLine(String.Format("Deleted dir \"{0}\"", emptyDirectory.FullName));

            SystemFunctions.SecureDeleteDirectory(emptyDirectory, this.DeleteMode);
        }

        internal void CancelCurrentProcess()
        {
            if (this.currentProcessStep == WorkflowSteps.StartingCalcDirCount)
            {
                if (this.calcDirCountWorker == null) return;

                if ((this.calcDirCountWorker.IsBusy == true) || (calcDirCountWorker.CancellationPending == false))
                    calcDirCountWorker.CancelAsync();
            }
            else if (this.currentProcessStep == WorkflowSteps.StartSearchingForEmptyDirs)
            {
                if (this.searchEmptyFoldersWorker == null) return;

                if ((this.searchEmptyFoldersWorker.IsBusy == true) || (searchEmptyFoldersWorker.CancellationPending == false))
                    searchEmptyFoldersWorker.CancelAsync();
            }
            else if (this.currentProcessStep == WorkflowSteps.DeleteProcessRunning)
            {
                this.stopDeleteProcessTrigger = true;
            }
        }


        internal void StartDelete(String[] ignoreFileList, bool cbIgnore0kbFiles, double pauseTime)
        {
            this.set_step(WorkflowSteps.DeleteProcessRunning);

            this.stopDeleteProcessTrigger = false;

            int DeletedFolderCount = 0;
            int FailedFolderCount = 0;

            int FolderCount = emptyFolderList.Count;

            for (int i = 0; (i < emptyFolderList.Count && !this.stopDeleteProcessTrigger); i++)
            {
                DirectoryInfo folder = emptyFolderList[i];
                DirectoryStatusTypes status = DirectoryStatusTypes.Ignored;


                // Do not delete protected folders:
                if (!this.protectedFolderList.ContainsKey(folder.FullName))
                {
                    try
                    {
                        // Try to delete the directory
                        this.secureDelete(folder, ignoreFileList, cbIgnore0kbFiles);

                        status = DirectoryStatusTypes.Deleted;
                        DeletedFolderCount++;
                    }
                    catch (Exception ex)
                    {
                        // Todo: Ask user to continue...? -- stop??

                        if (!this.ignoreAllErrors)
                        {
                            var dlg = new DeletionError();

                            dlg.SetPath(folder.FullName);
                            dlg.SetErrorMessage(ex.GetType().ToString() + ": " + ex.Message);

                            var result = dlg.ShowDialog();

                            if (result == DialogResult.Abort)
                            {
                                //this.core.CancelCurrentProcess();
                                this.stopDeleteProcessTrigger = true;

                                if (this.OnCancelled != null)
                                    this.OnCancelled(this, new EventArgs());

                                return;
                            }
                            else if (result == DialogResult.Retry) // retry = ignore all errors
                            {
                                this.ignoreAllErrors = true;
                            }

                            dlg.Dispose();
                        }

                        status = DirectoryStatusTypes.Warning;
                        FailedFolderCount++;
                    }

                    // Hack to allow the user to cancel the deletion process
                    Application.DoEvents();
                    Thread.Sleep(TimeSpan.FromMilliseconds(pauseTime));
                }
                else
                    status = DirectoryStatusTypes.Protected;

                if (this.OnDeleteProcessChanged != null)
                    this.OnDeleteProcessChanged(this, new REDCoreDeleteProcessUpdateEventArgs(i, folder, status, FolderCount));
            }

            if (this.OnDeleteProcessFinished != null)
                this.OnDeleteProcessFinished(this, new REDCoreDeleteProcessFinishedEventArgs(DeletedFolderCount, FailedFolderCount));

        }

        internal void AddProtectedFolder(string FullName, string Key)
        {
            if (!this.protectedFolderList.ContainsKey(FullName))
                this.protectedFolderList.Add(FullName, Key);

        }

        internal void RemoveProtected(string FolderFullName)
        {
            this.protectedFolderList.Remove(FolderFullName);
        }

        internal bool ProtectedContainsKey(string FolderFullName)
        {
            return this.protectedFolderList.ContainsKey(FolderFullName);
        }

        internal string[] GetProtectedNode(string selectedFolderFullName)
        {
            return ((string)this.protectedFolderList[selectedFolderFullName]).Split('|');
        }

        public string GetLog() { return this.log.ToString(); }
    }
}
