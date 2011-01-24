using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RED2
{
    /// <summary>
    /// Handles all tree related things
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

        public TreeManager(TreeView dirTree)
        {
            this.treeView = dirTree;
            this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvFolders_MouseClick);
            this.ClearTree();
            this.rootPath = "";
        }

        // Hack
        private void tvFolders_MouseClick(object sender, MouseEventArgs e)
        {
            this.treeView.SelectedNode = this.treeView.GetNodeAt(e.X, e.Y);
        }

        public void ClearTree()
        {
            this.rootNode = null;
            this.directoryToTreeNodeMapping = new Dictionary<string, TreeNode>();
            this.backupValues = new Dictionary<string, object>();

            // Reset TreeView
            this.treeView.Nodes.Clear();
        }

        internal void AddRootNode(DirectoryInfo directory, DirectoryIcons imageKey)
        {
            this.rootPath = directory.FullName.Trim('\\');

            rootNode = new TreeNode(directory.Name);
            rootNode.Tag = directory;
            rootNode.ImageKey = imageKey.ToString();
            rootNode.SelectedImageKey = imageKey.ToString();

            directoryToTreeNodeMapping = new Dictionary<String, TreeNode>();
            directoryToTreeNodeMapping.Add(directory.FullName, rootNode);

            this.treeView.Nodes.Add(rootNode);
        }

        internal void EnsureRootNodeIsVisible()
        {
            this.rootNode.EnsureVisible();
        }

        internal bool ContainsDirectory(string FolderFullName)
        {
            return directoryToTreeNodeMapping.ContainsKey(FolderFullName);
        }

        /// <summary>
        /// Marks a folder with the warning or deleted icon
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="iconKey"></param>
        internal void UpdateItemIcon(DirectoryInfo folder, DirectoryIcons iconKey)
        {
            var FNode = this.findOrCreateDirectoryNodeByPath(folder);

            if (FNode == null)
                return;

            FNode.ImageKey = iconKey.ToString();
            FNode.SelectedImageKey = iconKey.ToString();
            FNode.EnsureVisible();
        }

        private TreeNode findOrCreateDirectoryNodeByPath(DirectoryInfo Folder)
        {
            if (Folder == null) return null;

            // Folder exists already:
            if (directoryToTreeNodeMapping.ContainsKey(Folder.FullName))
                return directoryToTreeNodeMapping[Folder.FullName];
            else
                return AddOrUpdateDirectoryNode(Folder, DirectorySearchStatusTypes.NotEmpty, ""); // TODO: OK?
        }

        public TreeNode AddOrUpdateDirectoryNode(DirectoryInfo directory, DirectorySearchStatusTypes statusType, string optionalErrorMsg)
        {
            // exists already:
            if (this.ContainsDirectory(directory.FullName))
            {
                TreeNode n = null;

                if (directoryToTreeNodeMapping.ContainsKey(directory.FullName))
                    n = directoryToTreeNodeMapping[directory.FullName];
                //TODO: else

                applyNodeStyle(n, directory, statusType, optionalErrorMsg);

                return n;
            }

            //
            // Create new tree node
            //
            TreeNode newTreeNode = new TreeNode(directory.Name);

            applyNodeStyle(newTreeNode, directory, statusType, optionalErrorMsg);

            newTreeNode.Tag = directory;

            if (directory.Parent.FullName.Trim('\\') == this.rootPath)
                this.rootNode.Nodes.Add(newTreeNode);
            else
            {
                var parentNode = this.findOrCreateDirectoryNodeByPath(directory.Parent);

                if (parentNode != null)
                    parentNode.Nodes.Add(newTreeNode);
                //TODO: else?
            }

            directoryToTreeNodeMapping.Add(directory.FullName, newTreeNode);

            newTreeNode.EnsureVisible();

            return newTreeNode;
        }

        private void applyNodeStyle(TreeNode treeNode, DirectoryInfo directory, DirectorySearchStatusTypes statusType, string optionalErrorMsg)
        {
            treeNode.ForeColor = (statusType == DirectorySearchStatusTypes.Empty) ? Color.Red : Color.Gray;
            var iconKey = "";

            if (statusType == DirectorySearchStatusTypes.Empty)
            {
                var fileCount = directory.GetFiles().Length;
                bool containsTrash = (fileCount > 0);

                iconKey = containsTrash ? "folder_trash_files" : "folder";

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
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null)
            {
                var folder = (DirectoryInfo)this.treeView.SelectedNode.Tag;
                return folder.FullName;
            }

            return "";
        }

        #region Folder protection

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

        internal void DeleteSelectedDirectory()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null)
            {
                var folder = (DirectoryInfo)this.treeView.SelectedNode.Tag;

                if (OnDeleteRequest != null)
                    OnDeleteRequest(this, new DeleteRequestFromTreeEventArgs(folder));
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
    }
}
