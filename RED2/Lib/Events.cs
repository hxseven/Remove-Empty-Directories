using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RED2
{
    public class REDCoreWorkflowStepChangedEventArgs : EventArgs
    {
        public REDWorkflowSteps NewStep { get; set; }

        public REDCoreWorkflowStepChangedEventArgs(REDWorkflowSteps NewStep)
        {
            this.NewStep = NewStep;
        }
    }

    public class REDCoreErrorEventArgs : EventArgs
    {

        public string Message { get; set; }

        public REDCoreErrorEventArgs(string msg)
        {
            this.Message = msg;
        }
    }

    public class REDCoreCalcDirWorkerFinishedEventArgs : EventArgs
    {
        public int MaxFolderCount { get; set; }

        public REDCoreCalcDirWorkerFinishedEventArgs(int MaxFolderCount)
        {
            this.MaxFolderCount = MaxFolderCount;
        }
    }

    public class REDCoreFoundDirEventArgs : EventArgs
    {
        public DirectoryInfo Directory { get; set; }

        public REDCoreFoundDirEventArgs(DirectoryInfo dir)
        {
            this.Directory = dir;
        }
    }

    public class REDCoreFinishedScanForEmptyDirsEventArgs : EventArgs
    {
        public int EmptyFolderCount { get; set; }
        public int FolderCount { get; set; }

        public REDCoreFinishedScanForEmptyDirsEventArgs(int EmptyFolderCount, int FolderCount)
        {
            this.EmptyFolderCount = EmptyFolderCount;
            this.FolderCount = FolderCount;
        }
    }

    public class REDCoreDeleteProcessUpdateEventArgs : EventArgs
    {
        public int ProgressStatus { get; set; }
        public DirectoryInfo Folder { get; set; }
        public REDDirStatus Status { get; set; }
        public int FolderCount { get; set; }

        public REDCoreDeleteProcessUpdateEventArgs(int ProgressStatus, DirectoryInfo Folder, REDDirStatus Status, int FolderCount)
        {
            this.ProgressStatus = ProgressStatus;
            this.Folder = Folder;
            this.Status = Status;
            this.FolderCount = FolderCount;
        }
    }

    public class REDCoreDeleteProcessFinishedEventArgs : EventArgs
    {
        public int DeletedFolderCount { get; set; } 
        public int FailedFolderCount { get; set; }

        public REDCoreDeleteProcessFinishedEventArgs(int DeletedFolderCount, int FailedFolderCount)
        {
            this.DeletedFolderCount = DeletedFolderCount;
            this.FailedFolderCount = FailedFolderCount;
        }
    }

}
