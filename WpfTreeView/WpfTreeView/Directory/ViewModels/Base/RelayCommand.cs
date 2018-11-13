
using System;
using System.Windows.Input;

namespace WpfTreeView
{
    /// <summary>
    /// A basic command that run an action.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members
        // the action to run.
        private Action action;
        #endregion

        #region Public Event
        /// <summary>
        /// The event that fired when <see cref="CanExecute(object)"/> value has be changed.
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        public RelayCommand(Action a)
        {
            this.action = a;
        }
        #endregion

        #region Comand Methods
        /// <summary>
        /// A relay command can execute always.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute command action.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            action();
        }
        #endregion
    }
}
