/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 12:30:46 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Windows.Input;

namespace AutumnBox.GUI.MVVM
{
    class FlexiableCommand : NotificationObject, ICommand
    {
        public bool CanExecuteProp
        {
            get
            {
                return _canExecute;
            }
            set
            {
                _canExecute = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
                RaisePropertyChanged();
            }
        }
        private bool _canExecute = true;
        public Action<object> Action { get; set; }
        public Action NoParmarAction
        {
            set
            {
                Action = arg => value();
            }
        }
        public FlexiableCommand(Action act)
        {
            NoParmarAction = act;
        }
        public FlexiableCommand(Action<object> act)
        {
            Action = act;
        }
        public FlexiableCommand() { }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecuteProp;
        }

        public virtual void Execute(object parameter)
        {
            Action?.Invoke(parameter);
        }
    }
}
