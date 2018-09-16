/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 16:44:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using System;
using System.Diagnostics;
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
                SGLogger<OpenParameterUrlCommand>.Warn( "can't open " + parameter, ex);
            }
        }
    }
}
