/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:47:35 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Windows.Input;

namespace AutumnBox.GUI.MVVM
{
    class MVVMCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        private readonly Action<object> impl;
        private readonly Func<object, bool> canExecute;
        public MVVMCommand(Action<object> impl) : this(impl, null)
        {
        }
        public MVVMCommand(Action<object> impl, Func<object, bool> canExecute)
        {
            this.impl = impl;
            this.canExecute = canExecute ?? (obj => true);
        }
        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            impl(parameter);
        }
    }
}
