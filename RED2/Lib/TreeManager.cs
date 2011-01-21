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
        private TreeView tvFolders = null;
        private Dictionary<String, TreeNode> directoryToTreeNodeMapping = null;

        private TreeNode RootNode = null;

        public TreeManager(TreeView dirTree)
        {
            this.tvFolders = dirTree;
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
        internal void CreateRootNode(DirectoryInfo StartFolder, string imageKey)
        {
            RootNode = new TreeNode(StartFolder.Name);
            RootNode.Tag = StartFolder;
            RootNode.ImageKey = imageKey;
            RootNode.SelectedImageKey = imageKey;

            directoryToTreeNodeMapping = new Dictionary<String, TreeNode>();
            directoryToTreeNodeMapping.Add(StartFolder.FullName, RootNode);

            this.tvFolders.Nodes.Add(RootNode);
        }

        internal void EnsureRootVis()
        {
            this.RootNode.EnsureVisible();
        }

        internal bool ContainsDir(string FolderFullName)
        {
            return directoryToTreeNodeMapping.ContainsKey(FolderFullName);
        }

        internal TreeNode GetDir(string FolderFullName)
        {
            return directoryToTreeNodeMapping[FolderFullName];
        }

        /// <summary>
        /// Marks a folder with the warning or deleted icon
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="iconKey"></param>
        internal void UpdateItemIcon(DirectoryInfo folder, string iconKey)
        {
            TreeNode FNode = this._findTreeNodeByFolder(folder);
            FNode.ImageKey = iconKey;
            FNode.SelectedImageKey = iconKey;
            FNode.EnsureVisible();
        }

        /// <summary>
        /// Returns a TreeNode Object for a given Folder
        /// </summary>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private TreeNode _findTreeNodeByFolder(DirectoryInfo Folder)
        {
            // Folder exists already:
            if (directoryToTreeNodeMapping.ContainsKey(Folder.FullName))
                return directoryToTreeNodeMapping[Folder.FullName];
            else
                return UI_AddEmptyFolderToTreeView(Folder, false);
        }

        /// <summary>
        /// Adds a folder to the treeview
        /// </summary>
        /// <param name="Folder"></param>
        /// <param name="_isEmpty"></param>
        /// <returns></returns>
        public TreeNode UI_AddEmptyFolderToTreeView(DirectoryInfo Folder, bool _isEmpty)
        {
            // exists already:
            if (this.ContainsDir(Folder.FullName))
            {
                TreeNode n = this.GetDir(Folder.FullName);
                n.ForeColor = Color.Red;
                return n;
            }

            bool parentIsRoot = (Folder.Parent.FullName.Trim('\\') == ((DirectoryInfo)this.RootNode.Tag).FullName.Trim('\\'));

            return this.CreateNewDir(Folder, _isEmpty, parentIsRoot);
        }

        internal TreeNode CreateNewDir(DirectoryInfo Folder, bool _isEmpty, bool parentIsRoot)
        {
            TreeNode NewNode = new TreeNode(Folder.Name);
            NewNode.ForeColor = (_isEmpty) ? Color.Red : Color.Gray;

            bool containsTrash = (!(Folder.GetFiles().Length == 0) && _isEmpty);

            NewNode.ImageKey = containsTrash ? "folder_trash_files" : "folder";

            if ((Folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                NewNode.ImageKey = containsTrash ? "folder_hidden_trash_files" : "folder_hidden";

            if ((Folder.Attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted)
                NewNode.ImageKey = containsTrash ? "folder_lock_trash_files" : "folder_lock";

            if ((Folder.Attributes & FileAttributes.System) == FileAttributes.System)
                NewNode.ImageKey = containsTrash ? "folder_lock_trash_files" : "folder_lock";

            NewNode.SelectedImageKey = NewNode.ImageKey;

            NewNode.Tag = Folder;

            if (parentIsRoot)
                this.RootNode.Nodes.Add(NewNode);
            else
            {
                TreeNode ParentNode = this._findTreeNodeByFolder(Folder.Parent);
                ParentNode.Nodes.Add(NewNode);
            }

            directoryToTreeNodeMapping.Add(Folder.FullName, NewNode);

            // !?? this.CreateNewDir(Folder.FullName);

            NewNode.EnsureVisible();

            return NewNode;
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
            if (this.tvFolders.SelectedNode != null && this.tvFolders.SelectedNode.Tag != null)
            {
                DirectoryInfo folder = (DirectoryInfo)this.tvFolders.SelectedNode.Tag;
                return folder.FullName;
            }
            return "";
        }
    }
}
