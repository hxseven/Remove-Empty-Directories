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
        private MainWindow redMainWindow = null;
        public WorkflowSteps CurrentProcessStep = WorkflowSteps.Init;
        private RuntimeData Data = null;

        // Workers
        private FindEmptyDirectoryWorker searchEmptyFoldersWorker = null;
        private DeletionWorker deletionWorker = null;

        // Events
        public event EventHandler<WorkflowStepChangedEventArgs> OnWorkflowStepChanged;
        public event EventHandler<ErrorEventArgs> OnError;
        public event EventHandler OnCancelled;
        public event EventHandler OnAborted;
        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<FoundDirEventArgs> OnFoundEmptyDir;
        public event EventHandler<FinishedScanForEmptyDirsEventArgs> OnFinishedScanForEmptyDirs;
        public event EventHandler<DeleteProcessUpdateEventArgs> OnDeleteProcessChanged;
        public event EventHandler<DeletionErrorEventArgs> OnDeleteError;
        public event EventHandler<DeleteProcessFinishedEventArgs> OnDeleteProcessFinished;

        public REDCore(MainWindow mainWindow, RuntimeData data)
        {
            this.redMainWindow = mainWindow;
            this.Data = data;
        }

        public void init()
        {
            this.set_step(WorkflowSteps.Init);
        }

        private void set_step(WorkflowSteps step)
        {
            this.CurrentProcessStep = step;

            if (this.OnWorkflowStepChanged != null)
                this.OnWorkflowStepChanged(this, new WorkflowStepChangedEventArgs(step));
        }

        /// <summary>
        /// Start searching empty folders
        /// </summary>
        public void SearchingForEmptyDirectories()
        {
            this.set_step(WorkflowSteps.StartSearchingForEmptyDirs);

            // Rest folder list
            this.Data.ProtectedFolderList = new Dictionary<string, bool>();

            searchEmptyFoldersWorker = new FindEmptyDirectoryWorker();
            searchEmptyFoldersWorker.Data = this.Data;

            searchEmptyFoldersWorker.ProgressChanged += new ProgressChangedEventHandler(FFWorker_ProgressChanged);
            searchEmptyFoldersWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FFWorker_RunWorkerCompleted);

            //TODO

            //if (!searchEmptyFoldersWorker.SetIgnoreFiles(this.data.IgnoreFiles))
            //{
            //    showErrorMsg(RED2.Properties.Resources.error_ignore_settings);
            //    return;
            //}

            //if (!searchEmptyFoldersWorker.SetIgnoreFolders(this.data.IgnoreFolders))
            //{
            //    showErrorMsg(RED2.Properties.Resources.error_ignore_settings);
            //    return;
            //}

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
            if ((int)e.ProgressPercentage != -1)
            {
                if (this.OnProgressChanged != null)
                    this.OnProgressChanged(this, e);
            }
            else
            {
                var directory = (DirectoryInfo)e.UserState;

                this.Data.EmptyFolderList.Add(directory);

                if (this.OnFoundEmptyDir != null)
                    this.OnFoundEmptyDir(this, new FoundDirEventArgs(directory));
            }
        }

        void FFWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FindEmptyDirectoryWorker FindWorker = sender as FindEmptyDirectoryWorker;

            if (e.Error != null)
            {
                searchEmptyFoldersWorker.Dispose();
                this.showErrorMsg(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                if (this.OnCancelled != null)
                    this.OnCancelled(this, new EventArgs());
            }
            else
            {
                int FolderCount = FindWorker.FolderCount;

                searchEmptyFoldersWorker.Dispose();
                searchEmptyFoldersWorker = null;

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

        public void DoDelete()
        {
            this.set_step(WorkflowSteps.DeleteProcessRunning);

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
                // Todo: Use separate class here?
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

        public string GetLog() { return this.Data.LogMessages.ToString(); }

        private void showErrorMsg(string errorMessage)
        {
            if (this.OnError != null)
                this.OnError(this, new ErrorEventArgs(errorMessage));
        }

        internal void AbortDeletion()
        {
            this.deletionWorker.Dispose(); this.deletionWorker = null;

            if (this.OnAborted != null)
                this.OnAborted(this, new EventArgs());
        }

        internal void ContinueDeletion()
        {
            // Continue
            this.deletionWorker.RunWorkerAsync();
        }
    }
}
