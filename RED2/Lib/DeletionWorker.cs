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

                var folder = this.Data.EmptyFolderList[this.ListPos];
                var status = DirectoryDeletionStatusTypes.Ignored;

                // Do not delete protected folders:
                if (!this.Data.ProtectedFolderList.ContainsKey(folder.FullName))
                {
                    try
                    {
                        // Try to delete the directory
                        this.secureDelete(folder);

                        this.Data.AddLogMessage(String.Format("Successfully deleted dir \"{0}\"", folder.FullName));

                        status = DirectoryDeletionStatusTypes.Deleted;
                        this.DeletedCount++;
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        stopNow = (!this.Data.IgnoreAllErrors);

                        this.Data.AddLogMessage(String.Format("Failed to delete dir \"{0}\" - Error message: \"{1}\"", folder.FullName, errorMessage));

                        status = DirectoryDeletionStatusTypes.Warning;
                        this.FailedCount++;
                    }

                    if (!stopNow)
                        Thread.Sleep(TimeSpan.FromMilliseconds(this.Data.PauseTime));
                }
                else
                    status = DirectoryDeletionStatusTypes.Protected;

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

        private void secureDelete(DirectoryInfo emptyDirectory)
        {
            if (!Directory.Exists(emptyDirectory.FullName))
                throw new Exception("Could not delete the directory \""+emptyDirectory.FullName+"\" because it does not exist anymore.");

            // Cleanup folder

            String[] ignoreFileList = this.Data.GetIgnoreFileList();

            FileInfo[] Files = emptyDirectory.GetFiles();

            if (Files != null && Files.Length != 0)
            {
                // loop trough files and cancel if containsFiles == true
                for (int f = 0; f < Files.Length; f++)
                {
                    FileInfo file = Files[f];

                    string delPattern = "";
                    bool deleteTrashFile = SystemFunctions.MatchesIgnorePattern(file, (int)file.Length, this.Data.Ignore0kbFiles, ignoreFileList, out delPattern);

                    // If only one file is good, then stop.
                    if (deleteTrashFile)
                    {
                        try
                        {
                            SystemFunctions.SecureDeleteFile(file, this.Data.DeleteMode);

                            this.Data.AddLogMessage(String.Format("-> Successfully deleted file \"{0}\" because it matched the ignore pattern \"{1}\"", file.FullName, delPattern));
                        }
                        catch (Exception ex) {
                            this.Data.AddLogMessage(String.Format("Failed to delete file \"{0}\" - Error message: \"{1}\"", file.FullName, ex.Message));

                            throw new Exception("Could not delete a empty (trash) file - error message: " + ex.Message);
                        }
                    }
                }
            }

            // End cleanup

            SystemFunctions.SecureDeleteDirectory(emptyDirectory, this.Data.DeleteMode);

        }

    }
}
