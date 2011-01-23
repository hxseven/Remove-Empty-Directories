using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RED2
{
    public class RuntimeData
    {
        // Configuration

        public DirectoryInfo StartFolder { get; set; }

        public bool IgnoreAllErrors { get; set; }
        public bool DisableLogging { get; set; }
        public DeleteModes DeleteMode { get; set; }

        public string IgnoreFiles { get; set; }
        public string IgnoreFolders { get; set; }

        public bool Ignore0kbFiles { get; set; }
        public bool IgnoreHiddenFolders { get; set; }
        public bool KeepSystemFolders { get; set; }
        public double PauseTime { get; set; }
        public int MaxDepth { get; set; }

        public StringBuilder LogMessages = new StringBuilder();
        public Dictionary<String, bool> ProtectedFolderList = new Dictionary<string, bool>();
        public List<DirectoryInfo> EmptyFolderList { get; set; }

        public RuntimeData()
        {
            this.LogMessages = new StringBuilder();
            this.ProtectedFolderList = new Dictionary<string, bool>();
            this.EmptyFolderList = new List<DirectoryInfo>();
        }
    }
}
