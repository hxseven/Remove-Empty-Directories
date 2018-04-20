using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Alphaleonis.Win32.Filesystem;
using FileAttributes = System.IO.FileAttributes;

namespace RED2
{
    /// <summary>
    /// Handles tree related things
    /// 
    /// TODO: Handle null references within tree nodes handling
    /// </summary>
    public class TreeManager
    {
        private TreeView treeView = null;
        private TreeNode rootNode = null;
        private string rootPath = "";

        private Dictionary<String, TreeNode> directoryToTreeNodeMapping = null;
        private Dictionary<string, object> backupValues = new Dictionary<string, object>();

        public event EventHandler<ProtectionStatusChangedEventArgs> OnProtectionStatusChanged;
        public event EventHandler<DeleteRequestFromTreeEventArgs> OnDeleteRequest;

		public bool UpdateUi { get; set; } = true;

        public TreeManager(TreeView dirTree)
        {
            this.treeView = dirTree;
            this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvFolders_MouseClick);

            this.ClearTree();

            this.rootPath = "";
        }

        /// <summary>
        /// Hack to selected the correct node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFolders_MouseClick(object sender, MouseEventArgs e)
        {
            this.treeView.SelectedNode = this.treeView.GetNodeAt(e.X, e.Y);
        }

        public void ClearTree()
        {
            this.rootNode = null;
            this.directoryToTreeNodeMapping = new Dictionary<string, TreeNode>();
            this.backupValues = new Dictionary<string, object>();

            this.treeView.Nodes.Clear();
        }

        internal void CreateRootNode(DirectoryInfo directory, DirectoryIcons imageKey)
        {
            this.rootPath = directory.FullName.Trim('\\');

            rootNode = new TreeNode(directory.Name);
            rootNode.Tag = directory;
            rootNode.ImageKey = imageKey.ToString();
            rootNode.SelectedImageKey = imageKey.ToString();

            directoryToTreeNodeMapping = new Dictionary<String, TreeNode>();
            directoryToTreeNodeMapping.Add(directory.FullName, rootNode);

			if (this.UpdateUi)
				AddRootNode();
		}

		internal void AddRootNode()
		{
			if (rootNode == null || (treeView.Nodes.Count == 1 && treeView.Nodes[0] == rootNode))
				return;

			this.treeView.Nodes.Clear();
            this.treeView.Nodes.Add(rootNode);
        }

        internal void EnsureRootNodeIsVisible()
        {
            this.rootNode.EnsureVisible();
        }

        /// <summary>
        /// Marks a folder with the warning or deleted icon
        /// </summary>
        /// <param name="path"></param>
        /// <param name="iconKey"></param>
        internal void UpdateItemIcon(string path, DirectoryIcons iconKey)
        {
            var treeNode = this.findOrCreateDirectoryNodeByPath(path);

            treeNode.ImageKey = iconKey.ToString();
            treeNode.SelectedImageKey = iconKey.ToString();
			if (this.UpdateUi)
            treeNode.EnsureVisible();
        }

        // TODO: Find better code structure for the following two routines
        private TreeNode findOrCreateDirectoryNodeByPath(string path)
        {
            if (path == null) return null;

            if (directoryToTreeNodeMapping.ContainsKey(path))
                return directoryToTreeNodeMapping[path];
            else
                return AddOrUpdateDirectoryNode(path, DirectorySearchStatusTypes.NotEmpty, "");
        }

        public TreeNode AddOrUpdateDirectoryNode(string path, DirectorySearchStatusTypes statusType, string optionalErrorMsg)
        {
            if (directoryToTreeNodeMapping.ContainsKey(path))
            {
                // Just update the style if the node already exists
                var n = directoryToTreeNodeMapping[path];
                applyNodeStyle(n, path, statusType, optionalErrorMsg);
                return n;
            }

            var directory = new DirectoryInfo(path);

            // Create new tree node
            var newTreeNode = new TreeNode(directory.Name);

            applyNodeStyle(newTreeNode, path, statusType, optionalErrorMsg);

            newTreeNode.Tag = directory;

			if (directory.Parent.FullName.Trim('\\').Equals(this.rootPath, StringComparison.OrdinalIgnoreCase))
            {
                this.rootNode.Nodes.Add(newTreeNode);
            }
            else
            {
                var parentNode = this.findOrCreateDirectoryNodeByPath(directory.Parent.FullName);
                parentNode.Nodes.Add(newTreeNode);
            }

            directoryToTreeNodeMapping.Add(path, newTreeNode);

			if (this.UpdateUi)
            newTreeNode.EnsureVisible();

            return newTreeNode;
        }

        private void applyNodeStyle(TreeNode treeNode, string path, DirectorySearchStatusTypes statusType, string optionalErrorMsg)
        {
            var directory = new DirectoryInfo(path);

            // TODO: use enums for icon names
            treeNode.ForeColor = (statusType == DirectorySearchStatusTypes.Empty) ? Color.Red : Color.Gray;
            var iconKey = "";

            if (statusType == DirectorySearchStatusTypes.Empty)
            {
                var fileCount = directory.GetFiles().Length;
                var containsTrash = (fileCount > 0);

                iconKey = containsTrash ? "folder_trash_files" : "folder";

                // TODO: use data from scan thread
                if ((directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) iconKey = containsTrash ? "folder_hidden_trash_files" : "folder_hidden";
                if ((directory.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted) iconKey = containsTrash ? "folder_lock_trash_files" : "folder_lock";
                if ((directory.Attributes & FileAttributes.System) == FileAttributes.System) iconKey = containsTrash ? "folder_lock_trash_files" : "folder_lock";

                if (containsTrash)
                    treeNode.Text += " (contains " + fileCount.ToString() + " empty files)";
            }
            else if (statusType == DirectorySearchStatusTypes.Error)
            {
                iconKey = "folder_warning";

                if (optionalErrorMsg != "")
                {
                    optionalErrorMsg = optionalErrorMsg.Replace("\r", "").Replace("\n", "");
                    if (optionalErrorMsg.Length > 55) optionalErrorMsg = optionalErrorMsg.Substring(0, 55) + "...";
                    treeNode.Text += " (" + optionalErrorMsg + ")";
                }
            }
            else if (statusType == DirectorySearchStatusTypes.Ignore)
            {
                iconKey = "protected_icon";
                treeNode.ForeColor = Color.Blue;
            }

            if (treeNode != this.rootNode)
            {
                treeNode.ImageKey = iconKey;
                treeNode.SelectedImageKey = iconKey;
            }
        }

        /// <summary>
        /// Returns the selected folder path
        /// </summary>
        /// <returns></returns>
        public string GetSelectedFolderPath()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null && this.treeView.SelectedNode.Tag is DirectoryInfo)
                return ((DirectoryInfo)this.treeView.SelectedNode.Tag).FullName;

            return "";
        }

        internal void DeleteSelectedDirectory()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null && this.treeView.SelectedNode.Tag is DirectoryInfo)
            {
                var folder = (DirectoryInfo)this.treeView.SelectedNode.Tag;

                if (OnDeleteRequest != null)
                    OnDeleteRequest(this, new DeleteRequestFromTreeEventArgs(folder.FullName));
            }
        }

        internal void RemoveNode(string path)
        {
            if (this.backupValues.ContainsKey(path))
                this.backupValues.Remove(path);

            if (this.directoryToTreeNodeMapping.ContainsKey(path))
            {
                this.directoryToTreeNodeMapping[path].Remove();
                this.directoryToTreeNodeMapping.Remove(path);
            }
        }

        #region Directory protection methods

        internal void ProtectSelected()
        {
            if (treeView.SelectedNode != null)
                this.ProtectNode(treeView.SelectedNode);
        }

        internal void UnprotectSelected()
        {
            unprotectNode(treeView.SelectedNode);
        }

        private void unprotectNode(TreeNode node)
        {
            if (node != null)
            {
                var directory = ((DirectoryInfo)node.Tag);

                if (!this.backupValues.ContainsKey(directory.FullName))
                    return;

                string[] propList = ((string)this.backupValues[directory.FullName]).Split('|');

                this.backupValues.Remove(directory.FullName);

                node.ImageKey = propList[0];
                node.SelectedImageKey = propList[0];
                node.ForeColor = Color.FromArgb(Int32.Parse(propList[1]));

                if (OnProtectionStatusChanged != null)
                    OnProtectionStatusChanged(this, new ProtectionStatusChangedEventArgs(directory.FullName, false));

                // Unprotect all subnodes
                foreach (TreeNode subNode in node.Nodes)
                    this.unprotectNode(subNode);
            }
        }

        public void ProtectNode(TreeNode node)
        {
            DirectoryInfo directory = (DirectoryInfo)node.Tag;

            if (backupValues.ContainsKey(directory.FullName))
                return;

            if (OnProtectionStatusChanged != null)
                OnProtectionStatusChanged(this, new ProtectionStatusChangedEventArgs(directory.FullName, true));

            backupValues.Add(directory.FullName, node.ImageKey + "|" + node.ForeColor.ToArgb().ToString());

            node.ImageKey = "protected_icon";
            node.SelectedImageKey = "protected_icon";
            node.ForeColor = Color.Blue;

            // Recusive protect directories
            if (node.Parent != this.rootNode)
                ProtectNode(node.Parent);
        }

        #endregion
    }
}
