using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

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
                    delPattern = "[empty file (0 KB)]";
                    matches_pattern = true;
                }
                else if (pattern.ToLower() == file.Name.ToLower())
                {
                    delPattern = pattern;
                    matches_pattern = true;
                }
                else if (pattern.Contains("*"))
                {
                    pattern = Regex.Escape(pattern);
                    pattern = pattern.Replace("\\*", ".*");

                    regexPattern = new Regex("^" + pattern + "$");

                    if (regexPattern.IsMatch(file.Name))
                    {
                        delPattern = pattern;
                        matches_pattern = true;
                    }
                }
                else if (pattern.StartsWith("/") && pattern.EndsWith("/"))
                {
                    regexPattern = new Regex(pattern.Substring(1, pattern.Length - 2));

                    if (regexPattern.IsMatch(file.Name))
                    {
                        delPattern = pattern;
                        matches_pattern = true;
                    }
                }
            }

            return matches_pattern;
        }

        public static void SecureDeleteDirectory(DirectoryInfo Folder, DeleteModes deleteMode)
        {
            // last security check (for files):
            if (Folder.GetFiles().Length == 0 && Folder.GetDirectories().Length == 0)
            {
                if (deleteMode == DeleteModes.RecycleBin) FileSystem.DeleteDirectory(Folder.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                else if (deleteMode == DeleteModes.RecycleBinWithQuestion) FileSystem.DeleteDirectory(Folder.FullName, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                //else if (deleteMode == DeleteModes.Direct) Folder.Delete();
                else if (deleteMode != DeleteModes.Simulate) throw new Exception("Unknown delete mode: \"" + deleteMode.ToString() + "\"");
            }
        }

        public static void SecureDeleteFile(FileInfo File, DeleteModes deleteMode)
        {
            if (deleteMode == DeleteModes.RecycleBin) FileSystem.DeleteFile(File.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            else if (deleteMode == DeleteModes.RecycleBinWithQuestion) FileSystem.DeleteDirectory(File.FullName, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
            //else if (deleteMode == DeleteModes.Direct) File.Delete();
            else if (deleteMode != DeleteModes.Simulate) throw new Exception("Unknown delete mode: \"" + deleteMode.ToString() + "\"");
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
        public static bool IsRegKeyIntegratedIntoWindowsExplorer(string MenuName)
        {
            return (Registry.ClassesRoot.OpenSubKey(MenuName) != null);
        }

        internal static void AddOrRemoveRegKey(bool remove, string MenuName, string Command)
        {
            RegistryKey regmenu = null;
            RegistryKey regcmd = null;

            if (!remove)
            {
                try
                {
                    regmenu = Registry.ClassesRoot.CreateSubKey(MenuName);

                    if (regmenu != null)
                        regmenu.SetValue("", RED2.Properties.Resources.registry_name);

                    regcmd = Registry.ClassesRoot.CreateSubKey(Command);

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
                    RegistryKey reg = Registry.ClassesRoot.OpenSubKey(Command);
                    if (reg != null)
                    {
                        reg.Close();
                        Registry.ClassesRoot.DeleteSubKey(Command);
                    }
                    reg = Registry.ClassesRoot.OpenSubKey(MenuName);
                    if (reg != null)
                    {
                        reg.Close();
                        Registry.ClassesRoot.DeleteSubKey(MenuName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(RED2.Properties.Resources.error + "\n\n" + ex.ToString());
                }
            }
        }
    }
}
