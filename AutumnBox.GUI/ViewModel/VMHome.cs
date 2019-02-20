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


        public VMHome()
        {
            Settings = new HomeSettings()
            {
                TipsEnable = true,
                CstEnable = true
            };
            FetchSettings();
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
