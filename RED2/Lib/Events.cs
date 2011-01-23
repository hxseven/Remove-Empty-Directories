using System;
using System.IO;

namespace RED2
{
    public class REDCoreWorkflowStepChangedEventArgs : EventArgs
    {
        public WorkflowSteps NewStep { get; set; }

        public REDCoreWorkflowStepChangedEventArgs(WorkflowSteps NewStep)
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

    public class FinishedScanForEmptyDirsEventArgs : EventArgs
    {
        public int EmptyFolderCount { get; set; }
        public int FolderCount { get; set; }

        public FinishedScanForEmptyDirsEventArgs(int EmptyFolderCount, int FolderCount)
        {
            this.EmptyFolderCount = EmptyFolderCount;
            this.FolderCount = FolderCount;
        }
    }

    public class DeleteProcessUpdateEventArgs : EventArgs
    {
        public int ProgressStatus { get; set; }
        public DirectoryInfo Folder { get; set; }
        public DirectoryStatusTypes Status { get; set; }
        public int FolderCount { get; set; }

        public DeleteProcessUpdateEventArgs(int ProgressStatus, DirectoryInfo Folder, DirectoryStatusTypes Status, int FolderCount)
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

    public class ProtectionStatusChangedEventArgs : EventArgs
    {
        public string Path { get; set; }
        public bool Protected { get; set; }

        public ProtectionStatusChangedEventArgs(string Path, bool Protected)
        {
            this.Path = Path;
            this.Protected = Protected;
        }
    }

    public class DeletionErrorEventArgs : EventArgs
    {
        public string Path { get; set; }
        public string ErrorMessage { get; set; }

        public DeletionErrorEventArgs(string Path, string ErrorMessage)
        {
            this.Path = Path;
            this.ErrorMessage = ErrorMessage;
        }
    }
}
