/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:57:19 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.GUI.Util.UI;
using AutumnBox.Logging;
using System.Threading;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMHome : ViewModelBase
    {
        public string BottomSentence => Sentences.Next();

        public HomeSettings Settings
        {
            get => _settings; set
            {
                _settings = value;
                RaisePropertyChanged();
            }
        }
        private HomeSettings _settings;


        public ICommand Refresh
        {
            get => _ref; set
            {
                _ref = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _ref;

        public VMHome()
        {
            RaisePropertyChangedOnDispatcher = true;
            Settings = new HomeSettings();
            FetchSettings();
            Refresh = new MVVMCommand(p =>
            {
                FetchSettings();
                RaisePropertyChanged(nameof(BottomSentence));
            });
        }

        private void FetchSettings()
        {
            new HomeSettingsGetter().Advance().ContinueWith((task) =>
            {
                if (task.IsFaulted)
                {
                    SLogger.Warn(this, "can not get home settings", task.Exception);
                }
                else
                {
                    Settings = task.Result;
                }
            });
        }
    }
}
