using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;

namespace RED2
{
    public enum DeleteModes
    {
        RecycleBin,
        RecycleBinWithQuestion,
        Direct,
        Simulate
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
        
        public static string FixLineBreaks(string str)
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

            FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
        }

        public static void SecureDeleteDirectory(string path, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.Simulate) return;

            // Last security check before deletion
            if (Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0)
            {
                if (deleteMode == DeleteModes.RecycleBin) FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                else if (deleteMode == DeleteModes.RecycleBinWithQuestion) FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                else if (deleteMode == DeleteModes.Direct) Directory.Delete(path);
                else throw new Exception("Internal error: Unknown delete mode: \"" + deleteMode.ToString() + "\"");
            }
            else
                throw new Exception("Failed to delete the directory: \"" + path + "\" because it is no longer empty.");
        }

        public static void SecureDeleteFile(FileInfo file, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.Simulate) return;

            if (deleteMode == DeleteModes.RecycleBin) FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            else if (deleteMode == DeleteModes.RecycleBinWithQuestion) FileSystem.DeleteFile(file.FullName, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            else if (deleteMode == DeleteModes.Direct)
            {
                //if (SystemFunctions.random.NextDouble() > 0.5) throw new Exception("Test error");
                file.Delete();
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
