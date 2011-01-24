using System.ComponentModel;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;

namespace RED2
{
    /// <summary>
    /// Deletes the empty directories RED found
    /// </summary>
    public class DeletionWorker : BackgroundWorker
    {
        public RuntimeData Data { get; set; }

        public int DeletedCount { get; set; }
        public int FailedCount { get; set; }

        public int ListPos { get; set; }

        public DeletionErrorEventArgs ErrorInfo { get; set; }
        public bool DeletionError { get; set; }

        public DeletionWorker()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

            this.ListPos = 0;
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            // This method will run on a thread other than the UI thread.
            // Be sure not to manipulate any Windows Forms controls created
            // on the UI thread from this method.

            if (CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            bool stopNow = false;
            string errorMessage = "";
            this.ErrorInfo = null;

            int count = this.Data.EmptyFolderList.Count;

            while (this.ListPos < this.Data.EmptyFolderList.Count)
            {
                if (CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                DirectoryInfo folder = this.Data.EmptyFolderList[this.ListPos];
                DirectoryStatusTypes status = DirectoryStatusTypes.Ignored;

                // Do not delete protected folders:
                if (!this.Data.ProtectedFolderList.ContainsKey(folder.FullName))
                {
                    try
                    {
                        // Try to delete the directory
                        this.secureDelete(folder);

                        status = DirectoryStatusTypes.Deleted;
                        this.DeletedCount++;
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        stopNow = (!this.Data.IgnoreAllErrors);

                        status = DirectoryStatusTypes.Warning;
                        this.FailedCount++;
                    }

                    if (!stopNow)
                        Thread.Sleep(TimeSpan.FromMilliseconds(this.Data.PauseTime));
                }
                else
                    status = DirectoryStatusTypes.Protected;

                this.ReportProgress(1, new DeleteProcessUpdateEventArgs(this.ListPos, folder, status, count));

                this.ListPos++;

                if (stopNow)
                {
                    // stop here for now
                    if (errorMessage == "") errorMessage = "Unknown error";

                    e.Cancel = true;
                    this.ErrorInfo = new DeletionErrorEventArgs(folder.FullName, errorMessage);
                    return;
                }
            }

            e.Result = count;
        }

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

            String[] ignoreFileList = this.Data.IgnoreFiles.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

            FileInfo[] Files = emptyDirectory.GetFiles();

            if (Files != null && Files.Length != 0)
            {
                this.Data.LogMessages.AppendLine(String.Format("Cleaning directory: \"{0}\"", emptyDirectory.FullName));

                // loop trough files and cancel if containsFiles == true
                for (int f = 0; f < Files.Length; f++)
                {
                    FileInfo file = Files[f];

                    string delPattern = "";
                    bool deleteTrashFile = SystemFunctions.MatchesIgnorePattern(file, (int)file.Length, this.Data.Ignore0kbFiles, ignoreFileList, out delPattern);

                    // If only one file is good, then stop.
                    if (deleteTrashFile)
                    {
                        this.Data.LogMessages.AppendLine(String.Format(" -> Deleted file \"{0}\" because it matched the pattern \"{1}\"", file.FullName, delPattern));

                        SystemFunctions.SecureDeleteFile(file, this.Data.DeleteMode);
                    }
                }
            }

            // End cleanup

            this.Data.LogMessages.AppendLine(String.Format("Deleted dir \"{0}\"", emptyDirectory.FullName));

            SystemFunctions.SecureDeleteDirectory(emptyDirectory, this.Data.DeleteMode);
        }

    }
}
