using System;
using System.Windows.Input;

namespace FolderDesigner
{
    public class DelegateCommand : ICommand
    {
        readonly Action _execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
