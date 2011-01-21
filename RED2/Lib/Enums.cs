using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RED2
{
    public enum REDWorkflowSteps
    {
        Init,
        StartSearchingForEmptyDirs,
        StartingCalcDirCount,
        DeleteProcessRunning,
    }

    public enum REDDirStatus
    {
        Deleted,
        Warning,
        Ignored,
        Protected
    }

    public enum REDIcons
    {
        home,
        deleted,
        protected_icon,
        folder_warning
    }
}
