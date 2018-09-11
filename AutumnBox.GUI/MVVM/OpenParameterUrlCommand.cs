/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 16:44:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.MVVM
{
    public class OpenParameterUrlCommand : ICommand
    {
        public bool Executable
        {
            get => executable; set
            {
                executable = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }
        private bool executable;
        public event EventHandler CanExecuteChanged;
        private readonly string defaultUrl;
        public OpenParameterUrlCommand(string defaultUrl):this()
        {
            this.defaultUrl = defaultUrl;
        }
        public OpenParameterUrlCommand()
        {
            Executable = true;
        }
        public bool CanExecute(object parameter)
        {
            return Executable;
        }

        public void Execute(object parameter)
        {
            try
            {
                string url = parameter?.ToString() ?? defaultUrl;
                if (url == null) return;
                Process.Start(url);
            }
            catch (Exception ex)
            {
                Logger.Warn(this, "can't open " + parameter, ex);
            }
        }
    }
}
