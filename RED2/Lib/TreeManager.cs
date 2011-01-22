using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace RED2
{
    public class TreeManager
    {
        private TreeView treeView = null;
        private Dictionary<String, TreeNode> directoryToTreeNodeMapping = null;

        private TreeNode RootNode = null;

        public TreeManager(TreeView dirTree)
        {
            this.treeView = dirTree;
            this.Init();
        }

        public void Init()
        {
            directoryToTreeNodeMapping = new Dictionary<string, TreeNode>();
        }

        /// <summary>
        /// Create root node
        /// </summary>
        /// <param name="StartFolder"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal void CreateRootNode(DirectoryInfo StartFolder, DirectoryIcons imageKey)
        {
            RootNode = new TreeNode(StartFolder.Name);
            RootNode.Tag = StartFolder;
            RootNode.ImageKey = imageKey.ToString();
            RootNode.SelectedImageKey = imageKey.ToString();

            directoryToTreeNodeMapping = new Dictionary<String, TreeNode>();
            directoryToTreeNodeMapping.Add(StartFolder.FullName, RootNode);

            this.treeView.Nodes.Add(RootNode);
        }

        internal void EnsureRootNodeIsVisible()
        {
            this.RootNode.EnsureVisible();
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

            bool parentIsRoot = (Folder.Parent.FullName.Trim('\\') == ((DirectoryInfo)this.RootNode.Tag).FullName.Trim('\\'));

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
                this.RootNode.Nodes.Add(newTreeNode);
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
            return (treeNode == this.RootNode);
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
    }
}
