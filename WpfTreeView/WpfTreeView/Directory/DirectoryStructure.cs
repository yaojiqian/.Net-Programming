
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WpfTreeView
{
    /// <summary>
    /// Helper class to query the information of the directories.
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Get logical drives on the computer.
        /// </summary>
        /// <returns>Logical drives.</returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // Get every logical drive on the machine
            return Directory.GetLogicalDrives().Select(d => new DirectoryItem { FullPath = d, Type = DirectoryItemType.Drive }).ToList();            
        }

        /// <summary>
        /// Get the directory top-level contents.
        /// </summary>
        /// <param name="fullPath">the directory's full path.</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            // Create empty item list.
            var items = new List<DirectoryItem>();

            #region Get Directories
            // Try and get directories from the folder.
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }).ToList());
            }
            catch { }
            #endregion

            #region Get Files
            // Try and get directories from the folder.
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(f => new DirectoryItem { FullPath = f,  Type= DirectoryItemType.File}).ToList());
            }
            catch { }
            #endregion

            return items;
        }

        #region Helpers
        /// <summary>
        /// Find the file or folder name from full path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return System.String.Empty;

            // Make all slashes back slashes
            var normalizePath = path.Replace('/', '\\');

            // Find the last back slash in the path.
            var lastIndex = normalizePath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}
