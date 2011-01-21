using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RED2
{
    public enum REDWorkflowSteps
    {
        StartSearchingForEmptyDirs,
        StartingCalcDirCount,
        // DirectoryCountWorkerFinished, = unused
        DeleteProcessRunning,
        //IdleAfterCancel,
        // FindEmptyDirectoryWorkerFinished = unused
        //IdleAfterCancel2, = unused
        Init
    }

    public enum REDDirStatus
    {
        Deleted,
        Warning,
        Ignored,
        Protected
    }

}
