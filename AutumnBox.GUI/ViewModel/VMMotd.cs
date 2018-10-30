/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 16:36:31 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    internal class VMMotd : ViewModelBase
    {
        public string Motd
        {
            get => _motd; set
            {
                _motd = value;
                RaisePropertyChanged();
            }
        }
        private string _motd = "...";

        public VMMotd()
        {
            new MOTDGetter().Try((result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Motd = result.Message;
                });
            });
        }
    }
}
