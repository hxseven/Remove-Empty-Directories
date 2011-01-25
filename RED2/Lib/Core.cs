using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RED2
{
    /// <summary>
    /// RED core class, handles all the "hard work" and communicates with the GUI by using various events.
    /// </summary>
    public class REDCore
    {
        private MainWindow redMainWindow = null;
        public WorkflowSteps CurrentProcessStep = WorkflowSteps.Idle;
        private RuntimeData Data = null;

        // Workers
        private FindEmptyDirectoryWorker searchEmptyFoldersWorker = null;
        private DeletionWorker deletionWorker = null;

        // Events
        public event EventHandler<ErrorEventArgs> OnError;
        public event EventHandler OnCancelled;
        public event EventHandler OnAborted;
        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<FoundEmptyDirInfoEventArgs> OnFoundEmptyDirectory;
        public event EventHandler<FinishedScanForEmptyDirsEventArgs> OnFinishedScanForEmptyDirs;
        public event EventHandler<DeleteProcessUpdateEventArgs> OnDeleteProcessChanged;
        public event EventHandler<DeletionErrorEventArgs> OnDeleteError;
        public event EventHandler<DeleteProcessFinishedEventArgs> OnDeleteProcessFinished;

        public REDCore(MainWindow mainWindow, RuntimeData data)
        {
            this.redMainWindow = mainWindow;
            this.Data = data;
        }

        /// <summary>
        /// Start searching empty folders
        /// </summary>
        public void SearchingForEmptyDirectories()
        {
            this.CurrentProcessStep = WorkflowSteps.StartSearchingForEmptyDirs;

            // Rest folder list
            this.Data.ProtectedFolderList = new Dictionary<string, bool>();

            searchEmptyFoldersWorker = new FindEmptyDirectoryWorker();
            searchEmptyFoldersWorker.Data = this.Data;

            searchEmptyFoldersWorker.ProgressChanged += new ProgressChangedEventHandler(FFWorker_ProgressChanged);
            searchEmptyFoldersWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FFWorker_RunWorkerCompleted);

            // Start worker
            searchEmptyFoldersWorker.RunWorkerAsync(this.Data.StartFolder);
        }

        /// <summary>
        /// This function gets called on a status update of the 
        /// find worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FFWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is FoundEmptyDirInfoEventArgs)
            {
                var info = (FoundEmptyDirInfoEventArgs)e.UserState;

                if (info.Type == DirectorySearchStatusTypes.Empty)
                    this.Data.EmptyFolderList.Add(info.Directory);

                if (this.OnFoundEmptyDirectory != null)
                    this.OnFoundEmptyDirectory(this, info);
            }
            else if (e.UserState is string)
            {
                if (this.OnProgressChanged != null)
                    this.OnProgressChanged(this, new ProgressChangedEventArgs(0, (string)e.UserState));
            }
            else
            {
                // TODO: Handle unknown types
            }
        }

        void FFWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.CurrentProcessStep = WorkflowSteps.Idle;

            if (e.Error != null)
            {
                this.searchEmptyFoldersWorker.Dispose(); this.searchEmptyFoldersWorker = null;

                this.showErrorMsg(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                if (this.searchEmptyFoldersWorker.ErrorInfo != null)
                {
                    // A error occured, process was stopped
                    this.showErrorMsg(this.searchEmptyFoldersWorker.ErrorInfo.ErrorMessage);

                    this.searchEmptyFoldersWorker.Dispose(); this.searchEmptyFoldersWorker = null;

                    if (this.OnAborted != null)
                        this.OnAborted(this, new EventArgs());
                }
                else
                {
                    this.searchEmptyFoldersWorker.Dispose(); this.searchEmptyFoldersWorker = null;

                    if (this.OnCancelled != null)
                        this.OnCancelled(this, new EventArgs());
                }
            }
            else
            {
                int FolderCount = this.searchEmptyFoldersWorker.FolderCount;

                this.searchEmptyFoldersWorker.Dispose(); this.searchEmptyFoldersWorker = null;

                if (this.OnFinishedScanForEmptyDirs != null)
                    this.OnFinishedScanForEmptyDirs(this, new FinishedScanForEmptyDirsEventArgs(this.Data.EmptyFolderList.Count, FolderCount));
            }
        }

        internal void CancelCurrentProcess()
        {
            if (this.CurrentProcessStep == WorkflowSteps.StartSearchingForEmptyDirs)
            {
                if (this.searchEmptyFoldersWorker == null) return;

                if ((this.searchEmptyFoldersWorker.IsBusy == true) || (searchEmptyFoldersWorker.CancellationPending == false))
                    searchEmptyFoldersWorker.CancelAsync();
            }
            else if (this.CurrentProcessStep == WorkflowSteps.DeleteProcessRunning)
            {
                if (this.deletionWorker == null) return;

                if ((this.deletionWorker.IsBusy == true) || (deletionWorker.CancellationPending == false))
                    deletionWorker.CancelAsync();
            }
        }

        public void StartDeleteProcess()
        {
            this.CurrentProcessStep = WorkflowSteps.DeleteProcessRunning;

            this.deletionWorker = new DeletionWorker();
            this.deletionWorker.Data = this.Data;

            this.deletionWorker.ProgressChanged += new ProgressChangedEventHandler(deletionWorker_ProgressChanged);
            this.deletionWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(deletionWorker_RunWorkerCompleted);

            this.deletionWorker.RunWorkerAsync();
        }

        void deletionWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var state = e.UserState as DeleteProcessUpdateEventArgs;

            if (this.OnDeleteProcessChanged != null)
                this.OnDeleteProcessChanged(this, state);
        }

        void deletionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.CurrentProcessStep = WorkflowSteps.Idle;

            if (e.Error != null)
            {
                this.showErrorMsg(e.Error.Message);

                this.deletionWorker.Dispose(); this.deletionWorker = null;
            }
            else if (e.Cancelled)
            {
                if (this.deletionWorker.ErrorInfo != null)
                {
                    // A error occured, process was stopped
                    //
                    // -> Ask user to continue

                    if (OnDeleteError != null)
                        OnDeleteError(this, this.deletionWorker.ErrorInfo);
                    else
                        throw new Exception("Internal error: event handler is missing.");
                }
                else
                {
                    // The user cancelled the process
                    if (this.OnCancelled != null)
                        this.OnCancelled(this, new EventArgs());
                }
            }
            else
            {
                // TODO: Use separate class here?
                int deletedCount = this.deletionWorker.DeletedCount;
                int failedCount = this.deletionWorker.FailedCount;

                this.deletionWorker.Dispose(); this.deletionWorker = null;

                if (this.OnDeleteProcessFinished != null)
                    this.OnDeleteProcessFinished(this, new DeleteProcessFinishedEventArgs(deletedCount, failedCount));
            }
        }

        internal void AddProtectedFolder(string path)
        {
            if (!this.Data.ProtectedFolderList.ContainsKey(path))
                this.Data.ProtectedFolderList.Add(path, true);
        }

        internal void RemoveProtected(string FolderFullName)
        {
            if (this.Data.ProtectedFolderList.ContainsKey(FolderFullName))
                this.Data.ProtectedFolderList.Remove(FolderFullName);
        }

        public string GetLog()
        {
            return this.Data.LogMessages.ToString();
        }

        private void showErrorMsg(string errorMessage)
        {
            if (this.OnError != null)
                this.OnError(this, new ErrorEventArgs(errorMessage));
        }

        internal void AbortDeletion()
        {
            this.CurrentProcessStep = WorkflowSteps.Idle;

            this.deletionWorker.Dispose(); this.deletionWorker = null;

            if (this.OnAborted != null)
                this.OnAborted(this, new EventArgs());
        }

        internal void ContinueDeleteProcess()
        {
            this.CurrentProcessStep = WorkflowSteps.DeleteProcessRunning;
            this.deletionWorker.RunWorkerAsync();
        }
    }
}
