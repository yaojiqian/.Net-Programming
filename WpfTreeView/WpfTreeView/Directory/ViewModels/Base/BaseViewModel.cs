
using System.ComponentModel;

namespace WpfTreeView
{
    /// <summary>
    /// the base view model
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// event for property's value changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
