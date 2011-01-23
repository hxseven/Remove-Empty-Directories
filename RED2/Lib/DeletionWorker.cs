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

        public RuntimeData data { get; set; }

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

            int count = this.data.EmptyFolderList.Count;

            while (this.ListPos < this.data.EmptyFolderList.Count)
            {
                if (CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                DirectoryInfo folder = this.data.EmptyFolderList[this.ListPos];
                DirectoryStatusTypes status = DirectoryStatusTypes.Ignored;

                // Do not delete protected folders:
                if (!this.data.protectedFolderList.ContainsKey(folder.FullName))
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
                        stopNow = (!this.data.IgnoreAllErrors);

                        status = DirectoryStatusTypes.Warning;
                        this.FailedCount++;
                    }

                    if (!stopNow)
                        Thread.Sleep(TimeSpan.FromMilliseconds(this.data.PauseTime));
                }
                else
                    status = DirectoryStatusTypes.Protected;

                this.ReportProgress(this.ListPos, new REDCoreDeleteProcessUpdateEventArgs(this.ListPos, folder, status, count));

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

            String[] ignoreFileList = this.data.IgnoreFiles.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

            FileInfo[] Files = emptyDirectory.GetFiles();
            Regex regexPattern = null;

            if (Files != null && Files.Length != 0)
            {
                this.data.log.AppendLine(String.Format("Cleaning directory: \"{0}\"", emptyDirectory.FullName));

                // loop trough files and cancel if containsFiles == true
                for (int f = 0; f < Files.Length; f++)
                {
                    FileInfo file = Files[f];

                    bool deleteTrashFile = false;
                    string delPattern = "";

                    for (int p = 0; (p < ignoreFileList.Length && !deleteTrashFile); p++)
                    {
                        var pattern = ignoreFileList[p];

                        if (this.data.Ignore0kbFiles && file.Length == 0)
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
                        this.data.log.AppendLine(String.Format(" -> Deleted file \"{0}\" because it matched the pattern \"{1}\"", file.FullName, delPattern));

                        SystemFunctions.SecureDeleteFile(file, this.data.DeleteMode);
                    }
                }
            }

            // End cleanup

            this.data.log.AppendLine(String.Format("Deleted dir \"{0}\"", emptyDirectory.FullName));

            SystemFunctions.SecureDeleteDirectory(emptyDirectory, this.data.DeleteMode);
        }

    }
}
