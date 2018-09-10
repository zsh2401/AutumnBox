/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 18:44:18 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMUpdateWindow : ViewModelBase
    {
        public UpdateCheckResult Model
        {
            get => model; set
            {
                model = value;
                RaisePropertyChanged();
            }
        }
        private UpdateCheckResult model;

        public ICommand GotoUpdate
        {
            get; private set;
        }

        public bool SkipThisVersion
        {
            get => skipThisVersion; set
            {
                skipThisVersion = value;
                Settings.Default.SkipVersion = value ? Model.VersionString : "0.0.0";
                Settings.Default.Save();
                RaisePropertyChanged();
            }
        }
        private bool skipThisVersion;
        public VMUpdateWindow(UpdateCheckResult result)
        {
            this.Model = result;
            GotoUpdate = new MVVMCommand((para) =>
            {
                Process.Start(result.UpdateUrl);
            });
        }
    }
}
