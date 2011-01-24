using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Threading;

namespace RED2
{
    /// <summary>
    /// Scans for empty directories
    /// </summary>
    public class FindEmptyDirectoryWorker : BackgroundWorker
    {
        private int folderCount = 0;
        public int FolderCount
        {
            get { return folderCount; }
        }

        public RuntimeData Data { get; set; }

        private string[] ignoreFolderList = null;
        private string[] ignoreFileList = null;

        public DeletionErrorEventArgs ErrorInfo { get; set; }

        public int PossibleEndlessLoop { get; set; }

        public FindEmptyDirectoryWorker()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            //Here we receive the necessary data 
            DirectoryInfo startFolder = (DirectoryInfo)e.Argument;

            this.PossibleEndlessLoop = 0;

            // Clean dir list
            this.Data.EmptyFolderList = new List<DirectoryInfo>();
            this.Data.LogMessages = new System.Text.StringBuilder();

            this.ignoreFileList = this.Data.GetIgnoreFileList();
            this.ignoreFolderList = this.Data.GetIgnoreDirectories();

            try
            {
                var rootStatusType = this.checkIfDirectoryEmpty(startFolder, 1);

                this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startFolder, rootStatusType));

                if (this.PossibleEndlessLoop > this.Data.InfiniteLoopDetectionCount)
                {
                    this.Data.AddLogMessage("Detected possible infinite-loop somewhere in the target path \"" + startFolder + "\" (symbolic links can cause this)");
                    throw new Exception("Possible infinite-loop detected (symbolic links can cause this)");
                }
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.Data.AddLogMessage("A error occured during the scan process: " + ex.Message);
                this.ErrorInfo = new DeletionErrorEventArgs(startFolder.FullName, ex.Message);
                return;
            }

            if (CancellationPending)
            {
                this.Data.AddLogMessage("Scan process was cancelled");
                e.Cancel = true;
                e.Result = 0;
                return;
            }
            
            //Here we pass the final result of the Job
            e.Result = 1;
        }

        private DirectorySearchStatusTypes checkIfDirectoryEmpty(DirectoryInfo startDir, int depth)
        {
            if (this.PossibleEndlessLoop > this.Data.InfiniteLoopDetectionCount)
            {
                this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.Error, "Aborted - possible infinite-loop detected"));
                return DirectorySearchStatusTypes.Error;
            }

            try
            {
                //Thread.Sleep(500);

                if (this.Data.MaxDepth != -1 && depth > this.Data.MaxDepth)
                    return DirectorySearchStatusTypes.NotEmpty;

                // Cancel process if the user hits stop
                if (CancellationPending)
                    return DirectorySearchStatusTypes.NotEmpty;

                // increase folder count
                this.folderCount++;

                // update status progress bar after 100 steps:
                if (this.folderCount % 100 == 0)
                {
                    this.ReportProgress(folderCount, "Checking directory: " + startDir.Name);
                }

                bool containsFiles = false;

                // Get file list
                FileInfo[] fileList = null;

                // some directories could trigger a exception:
                try
                {
                    fileList = startDir.GetFiles();
                }
                catch
                {
                    fileList = null;
                }

                if (fileList == null)
                {
                    // CF = true = folder does not get deleted:
                    containsFiles = true; // secure way
                    this.Data.AddLogMessage("Failed to access files in \"" + startDir.FullName + "\"");
                    this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.Error, "Failed to access files"));
                }
                else if (fileList.Length == 0)
                {
                    containsFiles = false;
                }
                else
                {
                    string delPattern = "";

                    // loop trough files and cancel if containsFiles == true
                    for (int f = 0; (f < fileList.Length && !containsFiles); f++)
                    {
                        FileInfo file = null;
                        int filesize = 0;

                        try
                        {
                            file = fileList[f];
                            filesize = (int)file.Length;
                        }
                        catch
                        {
                            // keep folder if there is a strange file that
                            // triggers a exception:
                            containsFiles = true;
                            break;
                        }

                        // If only one file is good, then stop.
                        if (!SystemFunctions.MatchesIgnorePattern(file, filesize, this.Data.Ignore0kbFiles, this.ignoreFileList, out delPattern))
                            containsFiles = true;
                    }
                }

                // If the folder does not contain any files -> get subfolders:
                DirectoryInfo[] subFolderList = null;

                try
                {
                    subFolderList = startDir.GetDirectories();
                }
                catch
                {
                    // If we can not read the folder -> don't delete it:
                    this.Data.AddLogMessage("Failed to access subdirectories in \"" + startDir.FullName + "\"");
                    this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.Error, "Failed to access subdirectories"));
                    return DirectorySearchStatusTypes.Error;
                }

                // The folder is empty, break here:
                if (!containsFiles && subFolderList.Length == 0)
                {
                    return DirectorySearchStatusTypes.Empty;
                }

                bool allSubDirectoriesEmpty = true;

                foreach (var curDir in subFolderList)
                {
                    // Hidden folder?
                    bool ignoreSubDirectory = (this.Data.IgnoreHiddenFolders && ((curDir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden));
                    ignoreSubDirectory = (ignoreSubDirectory || (this.Data.KeepSystemFolders && ((curDir.Attributes & FileAttributes.System) == FileAttributes.System)));
                    ignoreSubDirectory = (ignoreSubDirectory || checkIfDirectoryIsOnIgnoreList(curDir));

                    // Scan sub folder:
                    var subFolderStatus = DirectorySearchStatusTypes.NotEmpty;

                    if (!ignoreSubDirectory)
                    {
                        subFolderStatus = this.checkIfDirectoryEmpty(curDir, depth + 1);

                        // Report status to the GUI
                        if (subFolderStatus == DirectorySearchStatusTypes.Empty)
                            this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(curDir, subFolderStatus));
                    }

                    // this folder is not empty:
                    if (subFolderStatus != DirectorySearchStatusTypes.Empty || ignoreSubDirectory)
                        allSubDirectoriesEmpty = false;
                }

                // All subdirectories are empty
                return (allSubDirectoriesEmpty && !containsFiles) ? DirectorySearchStatusTypes.Empty : DirectorySearchStatusTypes.NotEmpty;

            }
            catch (Exception ex)
            {
                if (ex is System.IO.PathTooLongException)
                    this.PossibleEndlessLoop++;

                this.Data.AddLogMessage("A unknown error occured while trying to scan this directory: \"" + startDir.FullName + "\" - Error message: " + ex.Message);
                this.ReportProgress(0, new FoundEmptyDirInfoEventArgs(startDir, DirectorySearchStatusTypes.Error, ex.Message));

                return DirectorySearchStatusTypes.Error;
            }
        }

        private bool checkIfDirectoryIsOnIgnoreList(DirectoryInfo Folder)
        {
            bool ignoreFolder = false;

            // Ignore folder if the user wishes that
            if (this.ignoreFolderList.Length > 0)
            {
                foreach (string f in this.ignoreFolderList)
                {
                    if (Folder.FullName.ToLower().Contains(f.ToLower()))
                        ignoreFolder = true;
                }
            }
            return ignoreFolder;
        }
    }

    public class FoundEmptyDirInfoEventArgs : EventArgs
    {
        public DirectoryInfo Directory { get; set; }
        public DirectorySearchStatusTypes Type { get; set; }
        public string ErrorMessage { get; set; }

        public FoundEmptyDirInfoEventArgs(DirectoryInfo Directory, DirectorySearchStatusTypes type)
        {
            this.Directory = Directory;
            this.Type = type;
        }

        public FoundEmptyDirInfoEventArgs(DirectoryInfo Directory, DirectorySearchStatusTypes type, string ErrorMessage)
        {
            this.Directory = Directory;
            this.Type = type;
            this.ErrorMessage = ErrorMessage;
        }
    }

}
