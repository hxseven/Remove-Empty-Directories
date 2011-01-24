using System;
using System.IO;

namespace RED2
{
    public class WorkflowStepChangedEventArgs : EventArgs
    {
        public WorkflowSteps NewStep { get; set; }

        public WorkflowStepChangedEventArgs(WorkflowSteps NewStep)
        {
            this.NewStep = NewStep;
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; set; }

        public ErrorEventArgs(string msg)
        {
            this.Message = msg;
        }
    }

    //public class FoundDirEventArgs : EventArgs
    //{
    //    public DirectoryInfo Directory { get; set; }

    //    public FoundDirEventArgs(DirectoryInfo dir)
    //    {
    //        this.Directory = dir;
    //    }
    //}

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
        public DirectoryDeletionStatusTypes Status { get; set; }
        public int FolderCount { get; set; }

        public DeleteProcessUpdateEventArgs(int ProgressStatus, DirectoryInfo Folder, DirectoryDeletionStatusTypes Status, int FolderCount)
        {
            this.ProgressStatus = ProgressStatus;
            this.Folder = Folder;
            this.Status = Status;
            this.FolderCount = FolderCount;
        }
    }

    public class DeleteProcessFinishedEventArgs : EventArgs
    {
        public int DeletedFolderCount { get; set; } 
        public int FailedFolderCount { get; set; }

        public DeleteProcessFinishedEventArgs(int DeletedFolderCount, int FailedFolderCount)
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

    public class DeleteRequestFromTreeEventArgs : EventArgs
    {
        public DirectoryInfo Directory { get; set; }

        public DeleteRequestFromTreeEventArgs(DirectoryInfo Directory)
        {
            this.Directory = Directory;
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
