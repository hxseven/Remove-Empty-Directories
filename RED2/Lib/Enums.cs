namespace RED2
{
    /// <summary>
    /// RED workflow steps
    /// </summary>
    public enum WorkflowSteps
    {
        Idle,
        StartSearchingForEmptyDirs,
        DeleteProcessRunning,
    }

    /// <summary>
    /// Result status types of the scan
    /// </summary>
    public enum DirectorySearchStatusTypes
    {
        Empty,
        Error,
        NotEmpty,
        Ignore
    }

    /// <summary>
    /// Result types of the deletion process
    /// </summary>
    public enum DirectoryDeletionStatusTypes
    {
        Deleted,
        Warning,
        Ignored,
        Protected
    }
}
