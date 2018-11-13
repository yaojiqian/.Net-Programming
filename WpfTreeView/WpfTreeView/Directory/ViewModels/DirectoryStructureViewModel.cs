

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace WpfTreeView
{
    public class DirectoryStructureViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DirectoryItemViewModel> items = null;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public ObservableCollection<DirectoryItemViewModel> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }


        public DirectoryStructureViewModel()
        {
            var children = DirectoryStructure.GetLogicalDrives();

            Items = new ObservableCollection<DirectoryItemViewModel>(children.Select(d => new DirectoryItemViewModel(d.FullPath, DirectoryItemType.Drive)));
        }
    }
}
