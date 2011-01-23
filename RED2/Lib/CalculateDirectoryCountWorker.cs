using System.ComponentModel;
using System.IO;

namespace RED2
{
    /// <summary>
    /// Calculates the directory count
    /// </summary>
	public class CalculateDirectoryCountWorker : BackgroundWorker
	{
		#region Private members
		private int folderCount = 0;
		#endregion

		#region Properties
		public int FolderCount
		{
			get { return folderCount; }
			set { folderCount = value; }
		}

		private int maxDepth = -1;

		public int MaxDepth
		{
			get { return maxDepth; }
			set { maxDepth = value; }
		}
	

		#endregion

		#region Constructor
		public CalculateDirectoryCountWorker(){
			WorkerReportsProgress = true;
			WorkerSupportsCancellation = true;
		}
		#endregion

		protected override void OnDoWork(DoWorkEventArgs e)
		{
			//Here we receive the necessary data 
			DirectoryInfo _StartFolder = (DirectoryInfo)e.Argument;

			// This method will run on a thread other than the UI thread.
			// Be sure not to manipulate any Windows Forms controls created
			// on the UI thread from this method.

			this.calculateMaxFolders(_StartFolder, 1);

			if (CancellationPending)
			{
				e.Cancel = true;
				return;
			}

			//Here we pass the final result of the Job
			e.Result = this.folderCount; 
		}

        /// <summary>
        /// Loop recursive trough all folders
        /// </summary>
        /// <param name="_StartFolder"></param>
        /// <param name="_depth"></param>
		protected void calculateMaxFolders(DirectoryInfo _StartFolder, int _depth)
		{
			if (this.maxDepth != -1 && _depth > this.maxDepth)
				return;

			if (CancellationPending)
				return;

            DirectoryInfo[] subFoldersList = null;

            try
            {
                subFoldersList = _StartFolder.GetDirectories();
            }
            catch {
                // Could not scan sub folder
                return;
            }

			if (subFoldersList.Length == 0)
				return;

			foreach (DirectoryInfo Folder in subFoldersList)
			{
				this.folderCount++;

				this.calculateMaxFolders(Folder, _depth+1);
			}
		}

	}
}
