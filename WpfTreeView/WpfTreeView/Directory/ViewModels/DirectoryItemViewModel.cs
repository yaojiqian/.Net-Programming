
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

namespace WpfTreeView
{
    /// <summary>
    /// the view model for each directory item.
    /// </summary>
    public class DirectoryItemViewModel : INotifyPropertyChanged
    {
        private DirectoryItemType type = DirectoryItemType.Drive;
        private string fPath = string.Empty;
        //private string name = string.Empty;
        private ObservableCollection<DirectoryItemViewModel> children = null;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Public Properties
        /// <summary>
        /// the type of this directory item.
        /// </summary>
        public DirectoryItemType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Type)));
            }
        }

        /// <summary>
        /// the absolute path of this directory item.
        /// </summary>
        public string FullPath
        {
            get
            {
                return fPath;
            }
            set
            {
                fPath = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FullPath)));
            }
        }

        /// <summary>
        /// the name of this directory item.
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        /// <summary>
        /// A list of children in the item.
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children
        {
            get
            {
                if(children == null && type != DirectoryItemType.File )
                {
                    children = new ObservableCollection<DirectoryItemViewModel>();
                    children.Add(null);
                }
                return children;
            }
            set
            {
                children = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Children)));
            }
        }

        /// <summary>
        /// Indicates if the item can be expanded.
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if the directory item is expanded or not.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // If UI tells us to expend
                if (value == true)
                    // Find the all children.
                    Expand();
                // If Close the item
                else
                    ClearChildren();
            }
        }
        #endregion

        #region Public Command
        /// <summary>
        /// The Command to expand this item.
        /// </summary>
        public ICommand ExpandComand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// the constructor.
        /// </summary>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            ExpandComand = new RelayCommand(Expand);

            this.FullPath = fullPath;
            this.Type = type;
        }
        #endregion

        #region Helper Methods
        private void ClearChildren()
        {
            // Clear the children.
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show expand arrow if the type is not file.
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        /// <summary>
        /// Expand this directory item.
        /// </summary>
        private void Expand()
        {
            // we cannt expand file.
            if (Type == DirectoryItemType.File)
                return;

            // Find all children
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                                children.Select(item => new DirectoryItemViewModel(item.FullPath, item.Type)));
        }
        #endregion
    }
}
