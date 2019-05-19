/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 18:44:18 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.Util.Net.Getters;
using System.Diagnostics;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMUpdateWindow : ViewModelBase
    {
        public RemoteVersionInfoGetter.Result Model
        {
            get => model; set
            {
                model = value;
                RaisePropertyChanged();
            }
        }
        private RemoteVersionInfoGetter.Result model;

        public ICommand GotoUpdate
        {
            get; private set;
        }
        public ICommand SkipThisVersion { get; private set; }
        public VMUpdateWindow()
        {
            Model = Updater.Result;
            GotoUpdate = new MVVMCommand((para) =>
            {
                Process.Start(Model.UpdateUrl);
            });
            SkipThisVersion = new MVVMCommand((p) =>
            {
                Settings.Default.SkipVersion = model.VersionString;
                Settings.Default.Save();
            });
        }
    }
}
