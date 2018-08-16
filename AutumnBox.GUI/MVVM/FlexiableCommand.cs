/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 12:30:46 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Windows.Input;

namespace AutumnBox.GUI.MVVM
{
    class FlexiableCommand : ICommand
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
            }
        }
        private bool _canExecute = true;
        private readonly Action<object> act;
        public FlexiableCommand(Action act)
        {
            this.act = (arg) => act();
        }
        public FlexiableCommand(Action<object> act)
        {
            this.act = act;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecuteProp;
        }

        public void Execute(object parameter)
        {
            act(parameter);
        }
    }
}
