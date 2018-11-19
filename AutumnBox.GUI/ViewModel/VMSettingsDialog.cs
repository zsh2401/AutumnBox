/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/21 20:19:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Custom;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.GUI.Util.OS;
using AutumnBox.GUI.View.Windows;
using System.Collections.Generic;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMSettingsDialog : ViewModelBase
    {
        #region MVVM
        public bool UseRandomTheme
        {
            get
            {
                return Settings.Default.RandomTheme;
            }
            set
            {
                Settings.Default.RandomTheme = value;
                Settings.Default.Save();
                if (value)
                {
                    ThemeManager.Instance.ApplyBySetting();
                }
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(SelectedTheme));
                RaisePropertyChanged(nameof(ThemeComboBoxEnabled));
            }
        }

        public bool ThemeComboBoxEnabled
        {
            get
            {
                return !Settings.Default.RandomTheme;
            }
        }

        public IEnumerable<ILanguage> Languages
        {
            get => LanguageManager.Instance.Languages;
        }
        public ILanguage SelectedLanguage
        {
            get => LanguageManager.Instance.Current;
            set
            {
                LanguageManager.Instance.Current = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<ITheme> Themes { get => ThemeManager.Instance.Themes; }
        public ITheme SelectedTheme
        {
            get => ThemeManager.Instance.Current; set
            {
                Settings.Default.Theme = value.Name;
                Settings.Default.Save();
                ThemeManager.Instance.ApplyBySetting();
                RaisePropertyChanged();
            }
        }

        public bool DebugOnNext
        {
            get
            {
                return Settings.Default.ShowDebuggingWindowNextLaunch;
            }
            set
            {
                Settings.Default.ShowDebuggingWindowNextLaunch = value;
                RaisePropertyChanged();
            }
        }

        public bool SoundEffectEnable
        {
            get => Settings.Default.NotifyOnFinish; set
            {
                Settings.Default.NotifyOnFinish = value;
                Settings.Default.Save();
            }
        }

        public ICommand SendToDesktop { get; private set; }

        public ICommand ShowDebugWindow { get; private set; }

        public string ThemeDisplayMemberPath { get; set; } = nameof(ITheme.Name);
        public string LanguageDisplayMemberPath { get; set; } = nameof(ILanguage.LangName);
        #endregion
        public VMSettingsDialog()
        {
            SendToDesktop = new MVVMCommand((_) =>
            {
                ShortcutHelper.CreateShortcutOnDesktop("AutumnBox", System.Environment.CurrentDirectory + "/AutumnBox.GUI.exe", "The AutumnBox-Dream of us");
            });
            ShowDebugWindow = new MVVMCommand((_) =>
            {
                new LogWindow().Show();
            });
        }
    }
}
