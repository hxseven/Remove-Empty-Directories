using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RED2
{
    public enum WorkflowSteps
    {
        Init,
        StartSearchingForEmptyDirs,
        StartingCalcDirCount,
        DeleteProcessRunning,
    }

    public enum DirectoryStatusTypes
    {
        Deleted,
        Warning,
        Ignored,
        Protected
    }

    public enum DirectoryIcons
    {
        home,
        deleted,
        protected_icon,
        folder_warning
    }

    public enum DeleteModes
    {
        Recycle_bin,
        Recycle_bin_with_question,
        Directly,
        Simulate
    }

    public class DeleteModeItem
    {
        public DeleteModes DeleteMode { get; set; }

        public DeleteModeItem(DeleteModes Mode)
        {
            this.DeleteMode = Mode;
        }

        public static DeleteModes[] GetList()
        {
            return new DeleteModes[] { 
                DeleteModes.Recycle_bin, 
                DeleteModes.Recycle_bin_with_question,
                DeleteModes.Directly,
                DeleteModes.Simulate
            };
        }

        public override string ToString()
        {
            switch (this.DeleteMode)
            {
                case DeleteModes.Recycle_bin:
                    return "Delete to recycle bin (Default)";

                case DeleteModes.Recycle_bin_with_question:
                    return "Delete to recycle bin and ask before deletion of any directories";

                case DeleteModes.Directly:
                    return "Delete directly and don't ask no questions";

                case DeleteModes.Simulate:
                    return "Simulate deletion (Don't delete anything)";

                default:
                    throw new Exception("Unknown delete mode");
            }

            return "";
        }
    }

}
