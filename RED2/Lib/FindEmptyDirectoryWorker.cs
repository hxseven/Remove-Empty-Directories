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
        #region Class variables

        private int folderCount = 0;
        public int FolderCount
        {
            get { return folderCount; }
        }

        public List<DirectoryInfo> emptyDirectories = null;

        private string[] ignoreFiles = null;
        private string[] ignoreFolders = null;

        private bool ignore0kbFiles = false;
        public bool Ignore0kbFiles
        {
            get { return ignore0kbFiles; }
            set { ignore0kbFiles = value; }
        }

        private bool ignoreHiddenFolders = false;
        public bool IgnoreHiddenFolders
        {
            get { return ignoreHiddenFolders; }
            set { ignoreHiddenFolders = value; }
        }

        private bool ignoreSystemFolders = true;
        public bool IgnoreSystemFolders
        {
            get { return ignoreSystemFolders; }
            set { ignoreSystemFolders = value; }
        }

        private int maxDepth = -1;
        public int MaxDepth
        {
            get { return maxDepth; }
            set { maxDepth = value; }
        }

        private int emptyFolderCount = 0;
        public int EmptyFolderCount
        {
            get { return emptyFolderCount; }
        } 

        #endregion
	
		public FindEmptyDirectoryWorker(){			
			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
		}

		public bool SetIgnoreFiles(string _file_pattern) {
			try
			{
				_file_pattern = _file_pattern.Replace("\r\n", "\n");
				_file_pattern = _file_pattern.Replace("\r", "\n");
				this.ignoreFiles = _file_pattern.Split('\n');
				return true;
			}
			catch {
				return false;
			}
		}

        public bool SetIgnoreFolders(string _file_pattern)
        {
            try
            {
                _file_pattern = _file_pattern.Replace("\r\n", "\n");
                _file_pattern = _file_pattern.Replace("\r", "\n");
                _file_pattern = _file_pattern.Trim();

                if (_file_pattern != "")
                    this.ignoreFolders = _file_pattern.Split('\n');

                return true;
            }
            catch
            {
                return false;
            }
        }

		protected override void OnDoWork(DoWorkEventArgs e)
		{
			//Here we receive the necessary data 
			DirectoryInfo _StartFolder = (DirectoryInfo)e.Argument;

			// Clean dir list
			this.emptyDirectories = new List<DirectoryInfo>();

			//Here we tell the manager that we start the job..
			this.ReportProgress(0, "Starting scan process..."); 
			
			bool isEmpty = this.ScanFolders(_StartFolder, 1);

			if (isEmpty)
				this.emptyDirectories.Add(_StartFolder);

			if (CancellationPending)
			{
				e.Cancel = true;
				e.Result = 0;
				return;
			}

			ReportProgress(this.folderCount, "Finished!");

			//Here we pass the final result of the Job
			e.Result = 1;
		}

	
		private bool ScanFolders(DirectoryInfo _StartFolder, int _depth)
		{
			if (this.maxDepth != -1 && _depth > this.maxDepth)
				return false;
	
			// Cancel process if the user hits stop
			if (CancellationPending)
				return false;

			// increase folder count
			this.folderCount++;

			// update status progress bar after 100 steps:
			if (this.folderCount % 100 == 0)
				ReportProgress(folderCount, "Scanning folder: " + _StartFolder.Name);

			// Is the folder really empty?
			bool ContainsFiles = false;

            // Read files:
            FileInfo[] Files = null;

            // some folders trigger a exception:
            try
            {
                Files = _StartFolder.GetFiles();
            }
            catch {
                Files = null;
            }

            if (Files == null)
            {
                // CF = true = folder does not get deleted:
                ContainsFiles = true; // secure way
            }
            else if (Files.Length == 0)
            {
                ContainsFiles = false;
            }
            else
            {
                // loop trough files and cancel if containsFiles == true
                for (int f = 0; (f < Files.Length && !ContainsFiles); f++)
                {
                    FileInfo file = Files[f];

                    int filesize = 0;
                    try
                    {
                        filesize = (int)file.Length;
                    }
                    catch {
                        // keep folder if there is a strange file that
                        // triggers a exception:
                        ContainsFiles = true;
                        continue;
                    }

                    bool matches_a_pattern = false;

                    for (int p = 0; (p < this.ignoreFiles.Length && !matches_a_pattern); p++)
                    {
                        string pattern = this.ignoreFiles[p];

                        if (this.ignore0kbFiles && filesize == 0)
                            matches_a_pattern = true;
                        else if (pattern.ToLower() == file.Name.ToLower())
                            matches_a_pattern = true;
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

                    // If only one file is good, then stop.
                    if (!matches_a_pattern)
                        ContainsFiles = true;
                }
            }

			// If the folder does not contain any files -> get subfolders:
            DirectoryInfo[] SubFolders = null;

            try
            {
                SubFolders = _StartFolder.GetDirectories();
            }
            catch {
                // If we can not read the folder 
                // don't delete it:
                return false;
            }
			
			// The folder is empty, break here:
			if (!ContainsFiles && SubFolders.Length == 0)
				return true;

			bool AreTheSubFoldersEmpty = true;

			foreach (DirectoryInfo Folder in SubFolders)
			{
				// Hidden folder?
				bool ignoreFolder = (this.ignoreHiddenFolders && ((Folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden));
				
				// System folder:
				ignoreFolder = (this.ignoreSystemFolders && ((Folder.Attributes & FileAttributes.System) == FileAttributes.System)) ? true : ignoreFolder;

                // Ignore folder if the user wishes that
                if (this.ignoreFolders != null)
                {
                    foreach (string f in this.ignoreFolders)
                    {
                        if (Folder.FullName.ToLower().Contains(f.ToLower()))
                            ignoreFolder = true;
                    }
                }

				// Scan sub folder:
				bool isSubFolderEmpty = false;

				if (!ignoreFolder)
					isSubFolderEmpty = this.ScanFolders(Folder, _depth+1);

				// is empty?
				if (isSubFolderEmpty && !ignoreFolder)
				{
					this.emptyFolderCount++;
                    
					// Folder is empty, report that to the gui:
					this.ReportProgress(-1, Folder);
				}

				// this folder is not empty:
				if (!isSubFolderEmpty || ignoreFolder)
					AreTheSubFoldersEmpty = false;
			}

			// All subdirectories are empty
			return (AreTheSubFoldersEmpty && !ContainsFiles);
		}
    }
}
