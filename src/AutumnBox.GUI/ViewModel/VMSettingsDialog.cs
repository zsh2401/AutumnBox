/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/21 20:19:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMSettingsDialog : ViewModelBase
    {
        [AutoInject] public ISettings Settings { get; set; }

        #region MVVM
        public bool ShouldUseDarkTheme
        {
            get => ThemeManager.ThemeMode == ThemeMode.Dark;
            set
            {
                if (value)
                {
                    ThemeManager.ThemeMode = ThemeMode.Dark;
                }
                RaisePropertyChanged();
            }
        }

        public bool ShouldUseAutoTheme
        {
            get => ThemeManager.ThemeMode == ThemeMode.Auto;
            set
            {
                if (value)
                {
                    ThemeManager.ThemeMode = ThemeMode.Auto;
                }
                RaisePropertyChanged();
            }
        }
        public bool ShouldUseLightTheme
        {
            get => ThemeManager.ThemeMode == ThemeMode.Light;
            set
            {
                if (value)
                {
                    ThemeManager.ThemeMode = ThemeMode.Light;
                }
                RaisePropertyChanged();
            }
        }

        public bool DisplayCmdWindow
        {
            get
            {
                return false;
            }
            set
            {
                throw new PlatformNotSupportedException();
            }
        }

        public bool DeveloperMode
        {
            get
            {
                return Settings.DeveloperMode;
            }
            set
            {
                Settings.DeveloperMode = value;
                messageBus.SendMessage(Messages.REFRESH_EXTENSIONS_VIEW);
                RaisePropertyChanged();
            }
        }


        public bool DebugOnNext
        {
            get
            {
                return Settings.ShowDebugWindowNextLaunch;
            }
            set
            {
                Settings.ShowDebugWindowNextLaunch = value;
                RaisePropertyChanged();
            }
        }

        public bool SoundEffectEnable
        {
            get => Settings.SoundEffect; set
            {
                Settings.SoundEffect = value;
            }
        }

        public bool StartCmdAtDesktop
        {
            get
            {
                return Settings.StartCmdAtDesktop;
            }
            set
            {
                Settings.StartCmdAtDesktop = value;
                RaisePropertyChanged();
            }
        }

        public bool UseEnvVarCmd
        {
            get => Settings.EnvVarCmdWindow;
            set
            {
                Settings.EnvVarCmdWindow = value;
                RaisePropertyChanged();
                if (!value)
                {
                    StartCmdAtDesktop = false;
                }
            }
        }

        public IEnumerable<ILanguage> Languages
        {
            get => languageManager.LoadedLanguages;
        }

        public ILanguage SelectedLanguage
        {
            get => languageManager.Current;
            set
            {
                languageManager.Current = value;
                RaisePropertyChanged();
            }
        }

        public string LanguageDisplayMemberPath { get; set; } = nameof(ILanguage.LangName);


        #endregion
        [AutoInject] IThemeManager ThemeManager { get; set; }

        [AutoInject]
        private readonly ILanguageManager languageManager;

        [AutoInject]
        private readonly IOperatingSystemService operatingSystemService;

        [AutoInject]
        private readonly IOpenFxManager openFxManager;

        [AutoInject]
        private readonly INotificationManager notificationManager;

        [AutoInject]
        private readonly IMessageBus messageBus;

        public VMSettingsDialog()
        {
            if (IsDesignMode()) return;
            RaisePropertyChangedOnUIThread = true;
        }
    }
}
