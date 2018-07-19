using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace WpfTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region OnLoaded
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive on the machine
            foreach(var drive in Directory.GetLogicalDrives())
            {
                // Create a new item.
                var item = new TreeViewItem()
                {
                    // Set the header and path
                    Header = drive,
                    // And full path
                    Tag = drive
                };


                // Add a dummy sub item
                item.Items.Add(null);

                // Listen out for item expaned
                item.Expanded += Folder_Expanded;

                // Add it to the main tree-view
                FolderView.Items.Add(item);
            }
        }

        #endregion OnLoaded

        #region Folder_Expanded

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks
            var item = (TreeViewItem)sender;

            // If the item only contains the dummy data;
            if(item.Items.Count !=1 || item.Items[0] != null)
            {
                return;
            }

            // Clear dummy data
            item.Items.Clear();

            // Get full path
            var fullPath = (string)item.Tag;
            #endregion

            #region Get Directories
            // Create a blank list for directories.
            var directories = new List<string>();

            // Try and get directories from the folder.
            // ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // For each directory...
            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    // Set header as folder name
                    Header = GetFileFolderName(directoryPath),
                    // And tag as full path
                    Tag = directoryPath
                };

                // Add dummy Item
                subItem.Items.Add(null);

                subItem.Expanded += Folder_Expanded;

                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files
            // Create a blank list for directories.
            var files = new List<string>();

            // Try and get directories from the folder.
            // ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            // For each file...
            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    // Set header as folder name
                    Header = GetFileFolderName(filePath),
                    // And tag as full path
                    Tag = filePath
                };

                item.Items.Add(subItem);
            });
            #endregion
        }
        #endregion Folder_Expanded

        #region Helpers
        /// <summary>
        /// Find the file or folder name from full path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return String.Empty;

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
