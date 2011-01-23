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

        private Dictionary<String, TreeNode> directoryToTreeNodeMapping = null;
        private Dictionary<string, object> backupValues = new Dictionary<string, object>();

        public event EventHandler<ProtectionStatusChangedEventArgs> OnProtectionStatusChanged;

        public TreeManager(TreeView dirTree)
        {
            this.treeView = dirTree;
            this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvFolders_MouseClick);
            this.Clear();
        }

        private void tvFolders_MouseClick(object sender, MouseEventArgs e)
        {
            this.treeView.SelectedNode = this.treeView.GetNodeAt(e.X, e.Y);
        }

        public void Clear()
        {
            this.rootNode = null;
            this.directoryToTreeNodeMapping = new Dictionary<string, TreeNode>();
            this.backupValues = new Dictionary<string, object>();
        }

        /// <summary>
        /// Create root node
        /// </summary>
        /// <param name="StartFolder"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal void CreateRootNode(DirectoryInfo StartFolder, DirectoryIcons imageKey)
        {
            rootNode = new TreeNode(StartFolder.Name);
            rootNode.Tag = StartFolder;
            rootNode.ImageKey = imageKey.ToString();
            rootNode.SelectedImageKey = imageKey.ToString();

            directoryToTreeNodeMapping = new Dictionary<String, TreeNode>();
            directoryToTreeNodeMapping.Add(StartFolder.FullName, rootNode);

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

        internal TreeNode GetDirectory(string FolderFullName)
        {
            return directoryToTreeNodeMapping[FolderFullName];
        }

        /// <summary>
        /// Marks a folder with the warning or deleted icon
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="iconKey"></param>
        internal void UpdateItemIcon(DirectoryInfo folder, DirectoryIcons iconKey)
        {
            TreeNode FNode = this.findTreeNodeByFolder(folder);
            FNode.ImageKey = iconKey.ToString();
            FNode.SelectedImageKey = iconKey.ToString();
            FNode.EnsureVisible();
        }

        /// <summary>
        /// Returns a TreeNode Object for a given Folder
        /// </summary>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private TreeNode findTreeNodeByFolder(DirectoryInfo Folder)
        {
            // Folder exists already:
            if (directoryToTreeNodeMapping.ContainsKey(Folder.FullName))
                return directoryToTreeNodeMapping[Folder.FullName];
            else
                return AddEmptyFolderToTreeView(Folder, false);
        }

        /// <summary>
        /// Adds a folder to the treeview
        /// </summary>
        /// <param name="Folder"></param>
        /// <param name="_isEmpty"></param>
        /// <returns></returns>
        public TreeNode AddEmptyFolderToTreeView(DirectoryInfo Folder, bool _isEmpty)
        {
            // exists already:
            if (this.ContainsDirectory(Folder.FullName))
            {
                TreeNode n = this.GetDirectory(Folder.FullName);
                n.ForeColor = Color.Red;
                return n;
            }

            bool parentIsRoot = (Folder.Parent.FullName.Trim('\\') == ((DirectoryInfo)this.rootNode.Tag).FullName.Trim('\\'));

            return this.AddNewDirectory(Folder, _isEmpty, parentIsRoot);
        }

        internal TreeNode AddNewDirectory(DirectoryInfo directory, bool isEmpty, bool parentIsRoot)
        {
            TreeNode newTreeNode = new TreeNode(directory.Name);
            newTreeNode.ForeColor = (isEmpty) ? Color.Red : Color.Gray;

            var fileCount = directory.GetFiles().Length;

            bool containsTrash = (fileCount > 0 && isEmpty);

            newTreeNode.ImageKey = containsTrash ? "folder_trash_files" : "folder";

            if ((directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                newTreeNode.ImageKey = containsTrash ? "folder_hidden_trash_files" : "folder_hidden";

            if ((directory.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted)
                newTreeNode.ImageKey = containsTrash ? "folder_lock_trash_files" : "folder_lock";

            if ((directory.Attributes & FileAttributes.System) == FileAttributes.System)
                newTreeNode.ImageKey = containsTrash ? "folder_lock_trash_files" : "folder_lock";

            newTreeNode.SelectedImageKey = newTreeNode.ImageKey;

            newTreeNode.Tag = directory;

            if (containsTrash)
                newTreeNode.Text += " (" + fileCount.ToString() + " files)";

            if (parentIsRoot)
                this.rootNode.Nodes.Add(newTreeNode);
            else
            {
                TreeNode ParentNode = this.findTreeNodeByFolder(directory.Parent);
                ParentNode.Nodes.Add(newTreeNode);
            }

            directoryToTreeNodeMapping.Add(directory.FullName, newTreeNode);

            newTreeNode.EnsureVisible();

            return newTreeNode;
        }

        internal bool IsRootNode(TreeNode treeNode)
        {
            return (treeNode == this.rootNode);
        }

        /// <summary>
        /// Returns the selected folder path
        /// </summary>
        /// <returns></returns>
        public string GetSelectedFolderPath()
        {
            if (this.treeView.SelectedNode != null && this.treeView.SelectedNode.Tag != null)
            {
                DirectoryInfo folder = (DirectoryInfo)this.treeView.SelectedNode.Tag;
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
                var dir = ((DirectoryInfo)node.Tag);

                if (OnProtectionStatusChanged != null)
                    OnProtectionStatusChanged(this, new ProtectionStatusChangedEventArgs(dir.FullName, false));

                string[] backupValues = ((string)this.backupValues[dir.FullName]).Split('|');

                node.ImageKey = backupValues[0];
                node.SelectedImageKey = backupValues[0];
                node.ForeColor = Color.FromArgb(Int32.Parse(backupValues[1]));

                foreach (TreeNode subNode in node.Nodes)
                    this.unprotectNode(subNode);
            }
        }

        public void ProtectNode(TreeNode Node)
        {
            DirectoryInfo Folder = (DirectoryInfo)Node.Tag;

            if (OnProtectionStatusChanged != null)
                OnProtectionStatusChanged(this, new ProtectionStatusChangedEventArgs(Folder.FullName, true));

            backupValues.Add(Folder.FullName, Node.ImageKey + "|" + Node.ForeColor.ToArgb().ToString());

            Node.ImageKey = "protected_icon";
            Node.SelectedImageKey = "protected_icon";
            Node.ForeColor = Color.Blue;

            // Recusive protect directories
            if (!this.IsRootNode(Node.Parent))
                ProtectNode(Node.Parent);
        }

        #endregion
    }
}
