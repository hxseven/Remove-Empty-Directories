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
        private WorkflowSteps currentProcessStep = WorkflowSteps.Init;
        private RuntimeData data = null;

        // Workers
        private CalculateDirectoryCountWorker calcDirCountWorker = null;
        private FindEmptyDirectoryWorker searchEmptyFoldersWorker = null;
        private DeletionWorker deletionWorker = null;

        // Events
        public event EventHandler<REDCoreWorkflowStepChangedEventArgs> OnWorkflowStepChanged;
        public event EventHandler<REDCoreErrorEventArgs> OnError;
        public event EventHandler<REDCoreCalcDirWorkerFinishedEventArgs> OnCalcDirWorkerFinished;
        public event EventHandler OnCancelled;
        public event EventHandler OnAborted;
        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<REDCoreFoundDirEventArgs> OnFoundEmptyDir;
        public event EventHandler<FinishedScanForEmptyDirsEventArgs> OnFinishedScanForEmptyDirs;
        public event EventHandler<DeleteProcessUpdateEventArgs> OnDeleteProcessChanged;
        public event EventHandler<DeletionErrorEventArgs> OnDeleteError;
        public event EventHandler<REDCoreDeleteProcessFinishedEventArgs> OnDeleteProcessFinished;

        public REDCore(MainWindow mainWindow, RuntimeData data)
        {
            this.redMainWindow = mainWindow;
            this.data = data;
        }

        public void init()
        {
            this.set_step(WorkflowSteps.Init);
        }

        public void CalculateDirectoryCount()
        {
            this.set_step(WorkflowSteps.StartingCalcDirCount);
            this.data.EmptyFolderList = new List<DirectoryInfo>();

            // Create new blank worker
            this.calcDirCountWorker = new CalculateDirectoryCountWorker();
            this.calcDirCountWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FSWorker_RunWorkerCompleted);
            this.calcDirCountWorker.MaxDepth = this.data.MaxDepth;
            this.calcDirCountWorker.RunWorkerAsync(this.data.StartFolder);
        }

        private void set_step(WorkflowSteps step)
        {
            this.currentProcessStep = step;

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
            this.set_step(WorkflowSteps.StartSearchingForEmptyDirs);

            searchEmptyFoldersWorker = new FindEmptyDirectoryWorker();
            searchEmptyFoldersWorker.Data = this.data;

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
            searchEmptyFoldersWorker.RunWorkerAsync(this.data.StartFolder);

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

                this.data.EmptyFolderList.Add(directory);

                if (this.OnFoundEmptyDir != null)
                    this.OnFoundEmptyDir(this, new REDCoreFoundDirEventArgs(directory));
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
                    this.OnFinishedScanForEmptyDirs(this, new FinishedScanForEmptyDirsEventArgs(this.data.EmptyFolderList.Count, FolderCount));
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
                if (this.deletionWorker == null) return;

                if ((this.deletionWorker.IsBusy == true) || (deletionWorker.CancellationPending == false))
                    deletionWorker.CancelAsync();
            }
        }

        public void DoDelete()
        {
            this.set_step(WorkflowSteps.DeleteProcessRunning);

            this.deletionWorker = new DeletionWorker();
            this.deletionWorker.Data = this.data;

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
                    this.OnDeleteProcessFinished(this, new REDCoreDeleteProcessFinishedEventArgs(deletedCount, failedCount));
            }
        }

        internal void AddProtectedFolder(string path)
        {
            if (!this.data.ProtectedFolderList.ContainsKey(path))
                this.data.ProtectedFolderList.Add(path, true);
        }

        internal void RemoveProtected(string FolderFullName)
        {
            if (this.data.ProtectedFolderList.ContainsKey(FolderFullName))
                this.data.ProtectedFolderList.Remove(FolderFullName);
        }

        public string GetLog() { return this.data.LogMessages.ToString(); }

        private void showErrorMsg(string errorMessage)
        {
            if (this.OnError != null)
                this.OnError(this, new REDCoreErrorEventArgs(errorMessage));
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
