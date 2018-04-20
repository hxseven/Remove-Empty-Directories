using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using Alphaleonis.Win32.Filesystem;
using FileAccess = System.IO.FileAccess;
using FileMode = System.IO.FileMode;
using FileShare = System.IO.FileShare;

namespace RED2
{
    public enum DeleteModes
    {
        RecycleBin = 0,
        RecycleBinShowErrors = 1,
        RecycleBinWithQuestion = 2,
        Direct = 3,
        Simulate = 4
    }

    [Serializable]
    public class REDPermissionDeniedException : Exception
    {
        public REDPermissionDeniedException() { }
        public REDPermissionDeniedException(string message) : base(message) { }
        public REDPermissionDeniedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// A collection of (generic) system functions
    /// 
    /// Exception handling should be made by the caller
    /// </summary>
    public class SystemFunctions
    {
        // Registry keys
        private const string registryMenuName = "Folder\\shell\\Remove empty dirs";
        private const string registryCommand = "Folder\\shell\\Remove empty dirs\\command";

        public static string ConvertLineBreaks(string str)
        {
            return str.Replace(@"\r\n", "\r\n").Replace(@"\n", "\n");
        }

        public static bool MatchesIgnorePattern(FileInfo file, int filesize, bool Ignore0kbFiles, string[] ignoreFileList, out string delPattern)
        {
            bool matches_pattern = false;
            Regex regexPattern = null;
            delPattern = "";

            for (int pos = 0; (pos < ignoreFileList.Length && !matches_pattern); pos++)
            {
                string pattern = ignoreFileList[pos];

                // TODO: Check patterns for errors

                // Skip empty patterns
                if (pattern == "") continue;

                if (Ignore0kbFiles && filesize == 0)
                {
                    delPattern = "[Empty file]";
                    matches_pattern = true;
                }
                else if (pattern.ToLower() == file.Name.ToLower())
                {
                    // Direct match - ignore case
                    delPattern = pattern;
                    matches_pattern = true;
                }
                else if (pattern.Contains("*") || (pattern.StartsWith("/") && pattern.EndsWith("/")))
                {
                    // Pattern is a regex
                    if (pattern.StartsWith("/") && pattern.EndsWith("/"))
                    {
                        regexPattern = new Regex(pattern.Substring(1, pattern.Length - 2));
                    }
                    else
                    {
                        pattern = Regex.Escape(pattern).Replace("\\*", ".*");
                        regexPattern = new Regex("^" + pattern + "$");
                    }

                    if (regexPattern.IsMatch(file.Name))
                    {
                        delPattern = pattern;
                        matches_pattern = true;
                    }
                }
            }

            return matches_pattern;
        }

        public static void ManuallyDeleteDirectory(string path, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.Simulate) return;

            if (path == "") throw new Exception("Could not delete directory because the path was empty.");

            //TODO: Add FileIOPermission code?

            FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
        }

        public static bool IsDirLocked(string path)
        {
            try
            {
                // UGLY hack to determine whether we have write access
                // to a specific directory

                var r = new Random();
                var tempName = path + "deltest";

                int counter = 0;
                while (Directory.Exists(tempName))
                {

                    tempName = path + "deltest" + r.Next(0, 9999).ToString();
                    if (counter > 100) return true; // Something strange is going on... stop here...
                    counter++;
                }

                Directory.Move(path, tempName);
                Directory.Move(tempName, path);

                return false;
            }
            catch //(Exception ex)
            {
                // Could not rename -> probably we have no 
                // write access to the directory
                return true;
            }
        }

        public static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false;
                }
            }
            catch //(IOException)
            {
                // Could not open file -> probably we have no 
                // write access to the file
                return true;
            }
        }

        public static void SecureDeleteDirectory(string path, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.Simulate) return;
            if (deleteMode == DeleteModes.Direct)
            {
                Directory.Delete(path, recursive: false, ignoreReadOnly: true); //throws IOException if not empty anymore
                return;
            }

            // Last security check before deletion
            if (Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0)
            {
                if (deleteMode == DeleteModes.RecycleBin)
                {
                    // Check CLR permissions -> could raise a exception
                    new FileIOPermission(FileIOPermissionAccess.Write, path + Path.DirectorySeparatorChar.ToString()).Demand();

                    //if (!CheckWriteAccess(Directory.GetAccessControl(path)))
                    if (IsDirLocked(path))
                        throw new REDPermissionDeniedException("Could not delete directory \"" + path + "\" because the access is protected by the (file) system (permission denied).");

                    FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                }
                else if (deleteMode == DeleteModes.RecycleBinShowErrors)
                {
                    FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                }
                else if (deleteMode == DeleteModes.RecycleBinWithQuestion) FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                else throw new Exception("Internal error: Unknown delete mode: \"" + deleteMode.ToString() + "\"");
            }
            else
                throw new Exception("Aborted deletion of the directory \"" + path + "\" because it is no longer empty. This can happen if RED previously failed to delete a empty (trash) file.");
        }

        public static void SecureDeleteFile(FileInfo file, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.Simulate) return;

            if (deleteMode == DeleteModes.RecycleBin)
            {
                // Check CLR permissions -> could raise a exception
                new FileIOPermission(FileIOPermissionAccess.Write, file.FullName).Demand();

                if (IsFileLocked(file))
                    throw new REDPermissionDeniedException("Could not delete file \"" + file.FullName + "\" because the access is protected by the (file) system (permission denied).");

                FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            }
            else if (deleteMode == DeleteModes.RecycleBinShowErrors)
            {
                FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            }
            else if (deleteMode == DeleteModes.RecycleBinWithQuestion) FileSystem.DeleteFile(file.FullName, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            else if (deleteMode == DeleteModes.Direct)
            {
                // Was used for testing the error handling:
                // if (SystemFunctions.random.NextDouble() > 0.5) throw new Exception("Test error");
                file.Delete(ignoreReadOnly: true);
            }
            else throw new Exception("Internal error: Unknown delete mode: \"" + deleteMode.ToString() + "\"");
        }

        public static string ChooseDirectoryDialog(string path)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.Description = RED2.Properties.Resources.please_select;
            folderDialog.ShowNewFolderButton = false;

            if (path != "")
            {
                DirectoryInfo dir = new DirectoryInfo(path);

                if (dir.Exists)
                    folderDialog.SelectedPath = path;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
                path = folderDialog.SelectedPath;

            folderDialog.Dispose();
            folderDialog = null;

            return path;
        }

        /// <summary>
        /// Opens a folder
        /// </summary>
        public static void OpenDirectoryWithExplorer(string path)
        {
            if (path == "")
                return;

            string windows_folder = Environment.GetEnvironmentVariable("SystemRoot");

            Process.Start(windows_folder + "\\explorer.exe", "/e,\"" + path + "\"");
        }

        /// <summary>
        /// Check for the registry key
        /// </summary>
        /// <returns></returns>
        public static bool IsRegKeyIntegratedIntoWindowsExplorer()
        {
            return (Registry.ClassesRoot.OpenSubKey(registryMenuName) != null);
        }

        internal static void AddOrRemoveRegKey(bool add)
        {
            RegistryKey regmenu = null;
            RegistryKey regcmd = null;

            if (add)
            {
                try
                {
                    regmenu = Registry.ClassesRoot.CreateSubKey(registryMenuName);

                    if (regmenu != null)
                        regmenu.SetValue("", "Remove empty dirs");

                    regcmd = Registry.ClassesRoot.CreateSubKey(registryCommand);

                    if (regcmd != null)
                        regcmd.SetValue("", Application.ExecutablePath + " \"%1\"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (regmenu != null)
                        regmenu.Close();

                    if (regcmd != null)
                        regcmd.Close();
                }
            }
            else
            {
                try
                {
                    var reg = Registry.ClassesRoot.OpenSubKey(registryCommand);

                    if (reg != null)
                    {
                        reg.Close();
                        Registry.ClassesRoot.DeleteSubKey(registryCommand);
                    }
                    reg = Registry.ClassesRoot.OpenSubKey(registryMenuName);
                    if (reg != null)
                    {
                        reg.Close();
                        Registry.ClassesRoot.DeleteSubKey(registryMenuName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(RED2.Properties.Resources.error + "\nCould not change registry settings: " + ex.ToString());
                }
            }
        }
    }
}
