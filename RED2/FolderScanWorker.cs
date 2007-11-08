using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.IO;

namespace RED2
{
	public class FolderScanWorker : BackgroundWorker
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
		public FolderScanWorker(){
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

			this.CalculateMaxFolders(_StartFolder, 1);

			if (CancellationPending)
			{
				e.Cancel = true;
				return;
			}

			//Here we pass the final result of the Job
			e.Result = this.folderCount; 
		}

		protected void CalculateMaxFolders(DirectoryInfo _StartFolder, int _depth)
		{
			if (this.maxDepth != -1 && _depth > this.maxDepth)
				return;

			if (CancellationPending)
				return;

            DirectoryInfo[] SubFolders = null;

            try
            {
                SubFolders = _StartFolder.GetDirectories();
            }
            catch {
                // Could not scan sub folder
                return;
            }

			if (SubFolders.Length == 0)
				return;

			foreach (DirectoryInfo Folder in SubFolders)
			{
				this.folderCount++;
				this.CalculateMaxFolders(Folder, _depth+1);
			}
		}

	}
}
