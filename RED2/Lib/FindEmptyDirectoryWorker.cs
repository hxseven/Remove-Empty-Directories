using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

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

        public FindEmptyDirectoryWorker()
        {
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            //Here we receive the necessary data 
            DirectoryInfo startFolder = (DirectoryInfo)e.Argument;

            // Clean dir list
            this.Data.EmptyFolderList = new List<DirectoryInfo>();

            var _file_pattern = this.Data.IgnoreFolders.Replace("\r\n", "\n").Replace("\r", "\n").Trim();

            if (_file_pattern != "")
                this.ignoreFolderList = _file_pattern.Split('\n');


            _file_pattern = _file_pattern.Replace("\r\n", "\n").Replace("\r", "\n");
            this.ignoreFileList = _file_pattern.Split('\n');


            //Here we tell the manager that we start the job..
            this.ReportProgress(0, "Starting scan process...");

            bool isEmpty = this.checkIfDirectoryEmpty(startFolder, 1);

            if (isEmpty)
                this.Data.EmptyFolderList.Add(startFolder);

            if (CancellationPending)
            {
                e.Cancel = true;
                e.Result = 0;
                return;
            }

            ReportProgress(this.folderCount, "");

            //Here we pass the final result of the Job
            e.Result = 1;
        }

        private bool checkIfDirectoryEmpty(DirectoryInfo startDir, int depth)
        {
            if (this.Data.MaxDepth != -1 && depth > this.Data.MaxDepth) return false;

            // Cancel process if the user hits stop
            if (CancellationPending) return false;

            // increase folder count
            this.folderCount++;

            // update status progress bar after 100 steps:
            if (this.folderCount % 100 == 0)
                ReportProgress(folderCount, "Scanning folder: " + startDir.Name);


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
            }
            else if (fileList.Length == 0)
            {
                containsFiles = false;
            }
            else
            {
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
                    if (!matchIgnorePattern(file, filesize))
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
                return false;
            }

            // The folder is empty, break here:
            if (!containsFiles && subFolderList.Length == 0)
                return true;

            bool allSubFolderEmpty = true;

            foreach (DirectoryInfo curDir in subFolderList)
            {
                // Hidden folder?
                bool ignoreFolder = (this.Data.IgnoreHiddenFolders && ((curDir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden));
                ignoreFolder = (ignoreFolder || (this.Data.KeepSystemFolders && ((curDir.Attributes & FileAttributes.System) == FileAttributes.System)));
                ignoreFolder = (ignoreFolder || checkIfDirectoryIsOnIgnoreList(curDir));

                // Scan sub folder:
                bool isCurrentSubFolderEmpty = false;

                if (!ignoreFolder)
                    isCurrentSubFolderEmpty = this.checkIfDirectoryEmpty(curDir, depth + 1);

                // is empty?
                if (isCurrentSubFolderEmpty && !ignoreFolder)
                {
                    // Folder is empty, report that to the gui:
                    this.ReportProgress(-1, curDir);
                }

                // this folder is not empty:
                if (!isCurrentSubFolderEmpty || ignoreFolder)
                    allSubFolderEmpty = false;
            }

            // All subdirectories are empty
            return (allSubFolderEmpty && !containsFiles);
        }

        private bool matchIgnorePattern(FileInfo file, int filesize)
        {
            bool matches_a_pattern = false;

            for (int p = 0; (p < this.ignoreFileList.Length && !matches_a_pattern); p++)
            {
                string pattern = this.ignoreFileList[p];

                if (this.Data.Ignore0kbFiles && filesize == 0)
                {
                    matches_a_pattern = true;
                }
                else if (pattern.ToLower() == file.Name.ToLower())
                {
                    matches_a_pattern = true;
                }
                else if (pattern.Contains("*"))
                {
                    pattern = Regex.Escape(pattern);
                    pattern = pattern.Replace("\\*", ".*");

                    Regex RgxPattern = new Regex("^" + pattern + "$");

                    if (RgxPattern.IsMatch(file.Name))
                        matches_a_pattern = true;
                }
                else if (pattern.StartsWith("/") && pattern.EndsWith("/"))
                {
                    Regex RgxPattern = new Regex(pattern.Substring(1, pattern.Length - 2));

                    if (RgxPattern.IsMatch(file.Name))
                        matches_a_pattern = true;
                }

            }
            return matches_a_pattern;
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
}
