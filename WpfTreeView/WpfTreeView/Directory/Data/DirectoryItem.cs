
namespace WpfTreeView
{
    /// <summary>
    /// Information about directory item such as a Drive, a file or a folder.
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// the type of this directory item.
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// the absolute path of this directory item.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// the name of this directory item.
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath: DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
