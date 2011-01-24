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
        public event EventHandler<DeleteRequestFromTreeEventArgs> OnDeleteRequest;

        public TreeManager(TreeView dirTree)
        {
            this.treeView = dirTree;
            this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvFolders_MouseClick);
            this.ClearTree();
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
                newTreeNode.Text += " (" + fileCount.ToString() + " empty files)";

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
