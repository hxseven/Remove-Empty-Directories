using System;
using System.Collections.Generic;
using Alphaleonis.Win32.Filesystem;
using System.Text;

namespace RED2
{
    /// <summary>
    /// Container for all runtime related data
    /// </summary>
    public class RuntimeData
    {
        // Configuration

        public DirectoryInfo StartFolder { get; set; }

        public bool IgnoreAllErrors { get; set; }
        public bool DisableLogging { get; set; }
        public DeleteModes DeleteMode { get; set; }

        public string IgnoreFiles { get; set; }
        public string IgnoreDirectoriesList { get; set; }

        public bool IgnoreEmptyFiles { get; set; }
        public bool IgnoreHiddenFolders { get; set; }
        public bool KeepSystemFolders { get; set; }
        public bool HideScanErrors { get; set; }
        public double PauseTime { get; set; }
        public uint MinFolderAgeHours { get; set; }

        public int MaxDepth { get; set; }
        public int InfiniteLoopDetectionCount { get; set; }

        public StringBuilder LogMessages = null;
        public Dictionary<string, bool> ProtectedFolderList = new Dictionary<string, bool>();
        public List<string> EmptyFolderList { get; set; }

        public RuntimeData()
        {
            this.LogMessages = new StringBuilder();
            this.ProtectedFolderList = new Dictionary<string, bool>();
            this.EmptyFolderList = new List<string>();
        }

        public string[] GetIgnoreFileList()
        {
            return this.IgnoreFiles.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');
        }

        public string[] GetIgnoreDirectories()
        {
            return this.IgnoreDirectoriesList.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');
        }

        public void AddLogMessage(string msg)
        {
            this.LogMessages.AppendLine(DateTime.Now.ToString("r") + "\t" + msg);
        }

        internal void AddLogSpacer()
        {
            if (this.LogMessages.Length > 0)
                this.LogMessages.Append(Environment.NewLine);
        }
    }
}
