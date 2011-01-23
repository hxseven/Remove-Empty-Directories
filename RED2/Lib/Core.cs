using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace RED2
{
    /// <summary>
    /// RED core class, handles all the "hard work" and communicates with the GUI by using various events.
    /// </summary>
    public class REDCore
    {
        public DirectoryInfo StartFolder { get; set; }
        private WorkflowSteps currentProcessStep = WorkflowSteps.Init;

        private List<DirectoryInfo> emptyFolderList = null;
        private Dictionary<String, bool> protectedFolderList = new Dictionary<string, bool>();

        private CalculateDirectoryCountWorker calcDirCountWorker = null;
        private FindEmptyDirectoryWorker searchEmptyFoldersWorker = null;
        private StringBuilder log = new StringBuilder();

        private bool ignoreAllErrors = false;
        private bool stopDeleteProcessTrigger = false;

        // Events
        public event EventHandler<REDCoreWorkflowStepChangedEventArgs> OnWorkflowStepChanged;
        public event EventHandler<REDCoreErrorEventArgs> OnError;
        public event EventHandler<REDCoreCalcDirWorkerFinishedEventArgs> OnCalcDirWorkerFinished;
        public event EventHandler OnCancelled;
        public event EventHandler OnAborted;
        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<REDCoreFoundDirEventArgs> OnFoundEmptyDir;
        public event EventHandler<REDCoreFinishedScanForEmptyDirsEventArgs> OnFinishedScanForEmptyDirs;
        public event EventHandler<REDCoreDeleteProcessUpdateEventArgs> OnDeleteProcessChanged;
        public event EventHandler<REDCoreDeleteProcessFinishedEventArgs> OnDeleteProcessFinished;

        // Configuration
        public bool IgnoreErrors { get; set; }
        public bool DisableLogging { get; set; }
        public DeleteModes DeleteMode { get; set; }

        public string IgnoreFiles { get; set; }
        public string IgnoreFolders { get; set; }

        public bool Ignore0kbFiles { get; set; }
        public bool IgnoreHiddenFolders { get; set; }
        public bool KeepSystemFolders { get; set; }
        public double PauseTime { get; set; }
        public int MaxDepth { get; set; }

        public REDCore()
        {
        }

        public void init()
        {
            this.log = new StringBuilder();
            this.protectedFolderList = new Dictionary<string, bool>();
            this.set_step(WorkflowSteps.Init);
        }

        public void CalculateDirectoryCount()
        {
            emptyFolderList = new List<DirectoryInfo>();

            this.set_step(WorkflowSteps.StartingCalcDirCount);

            // Create new blank worker
            this.calcDirCountWorker = new CalculateDirectoryCountWorker();
            this.calcDirCountWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FSWorker_RunWorkerCompleted);
            this.calcDirCountWorker.MaxDepth = this.MaxDepth;
            this.calcDirCountWorker.RunWorkerAsync(this.StartFolder);
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

        /// <summary>
        /// Start searching empty folders
        /// </summary>
        public void SearchingForEmptyDirectories()
        {

            this.emptyFolderList = new List<DirectoryInfo>();

            this.set_step(WorkflowSteps.StartSearchingForEmptyDirs);

            searchEmptyFoldersWorker = new FindEmptyDirectoryWorker();
            searchEmptyFoldersWorker.ProgressChanged += new ProgressChangedEventHandler(FFWorker_ProgressChanged);
            searchEmptyFoldersWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FFWorker_RunWorkerCompleted);

            // set options:
            if (!searchEmptyFoldersWorker.SetIgnoreFiles(IgnoreFiles))
            {
                showErrorMsg(RED2.Properties.Resources.error_ignore_settings);
                return;
            }

            if (!searchEmptyFoldersWorker.SetIgnoreFolders(this.IgnoreFolders))
            {
                showErrorMsg(RED2.Properties.Resources.error_ignore_settings);
                return;
            }

            searchEmptyFoldersWorker.Ignore0kbFiles = Ignore0kbFiles;
            searchEmptyFoldersWorker.IgnoreHiddenFolders = IgnoreHiddenFolders;
            searchEmptyFoldersWorker.IgnoreSystemFolders = KeepSystemFolders;

            searchEmptyFoldersWorker.MaxDepth = MaxDepth;

            // Start worker
            searchEmptyFoldersWorker.RunWorkerAsync(StartFolder);

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

        internal void StartDelete()
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
                        this.secureDelete(folder);

                        status = DirectoryStatusTypes.Deleted;
                        DeletedFolderCount++;
                    }
                    catch (Exception ex)
                    {
                        // Todo: Ask user to continue...? -- stop??

                        if (!this.ignoreAllErrors)
                        {
                            var errorDialog = new DeletionError();

                            errorDialog.SetPath(folder.FullName);
                            errorDialog.SetErrorMessage(ex.GetType().ToString() + ": " + ex.Message);

                            var result = errorDialog.ShowDialog();

                            if (result == DialogResult.Abort)
                            {
                                //this.core.CancelCurrentProcess();
                                this.stopDeleteProcessTrigger = true;

                                if (this.OnAborted != null)
                                    this.OnAborted(this, new EventArgs());

                                return;
                            }
                            else if (result == DialogResult.Retry) // retry = ignore all errors
                            {
                                this.ignoreAllErrors = true;
                            }

                            errorDialog.Dispose();
                        }

                        status = DirectoryStatusTypes.Warning;
                        FailedFolderCount++;
                    }

                    // Hack to allow the user to cancel the deletion process
                    Application.DoEvents();
                    Thread.Sleep(TimeSpan.FromMilliseconds(PauseTime));
                }
                else
                    status = DirectoryStatusTypes.Protected;

                if (this.OnDeleteProcessChanged != null)
                    this.OnDeleteProcessChanged(this, new REDCoreDeleteProcessUpdateEventArgs(i, folder, status, FolderCount));
            }

            if (this.OnDeleteProcessFinished != null)
                this.OnDeleteProcessFinished(this, new REDCoreDeleteProcessFinishedEventArgs(DeletedFolderCount, FailedFolderCount));

        }

        internal void AddProtectedFolder(string path)
        {
            if (!this.protectedFolderList.ContainsKey(path))
                this.protectedFolderList.Add(path, true);
        }

        internal void RemoveProtected(string FolderFullName)
        {
            if (this.protectedFolderList.ContainsKey(FolderFullName))
                this.protectedFolderList.Remove(FolderFullName);
        }

        public string GetLog() { return this.log.ToString(); }

        /// <summary>
        /// Finally delete a folder (with security checks before)
        /// </summary>
        /// <param name="emptyDirectory"></param>
        /// <returns></returns>
        private void secureDelete(DirectoryInfo emptyDirectory)
        {
            if (!Directory.Exists(emptyDirectory.FullName))
                throw new Exception("Directory does not exist anymore?");

            // Cleanup folder

            String[] ignoreFileList = this.IgnoreFiles.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

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

                        if (this.Ignore0kbFiles && file.Length == 0)
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

        private void showErrorMsg(string errorMessage)
        {
            if (this.OnError != null)
                this.OnError(this, new REDCoreErrorEventArgs(errorMessage));
        }
    }
}
