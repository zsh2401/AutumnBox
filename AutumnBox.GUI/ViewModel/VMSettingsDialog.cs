/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/21 20:19:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.Custom;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.GUI.Util.OS;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework.ExtLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMSettingsDialog : ViewModelBase
    {
        #region MVVM
        public bool DisplayCmdWindow
        {
            get
            {
                return Settings.Default.DisplayCmdWindow;
            }
            set
            {
                Settings.Default.DisplayCmdWindow = value;
                Basic.Util.Settings.CreateNewWindow = value;
                RaisePropertyChanged();
            }
        }

        public bool DeveloperMode
        {
            get
            {
                return Settings.Default.DeveloperMode;
            }
            set
            {
                Settings.Default.DeveloperMode = value;
                ExtensionViewRefresher.Instance.Refresh();
                RaisePropertyChanged();
            }
        }

        public string GUIVersion
        {
            get => _guiVersion; set
            {
                _guiVersion = value;
                RaisePropertyChanged();
            }
        }
        private string _guiVersion = "...";

        public string BasicVersion
        {
            get => _basicVersion; set
            {
                _basicVersion = value;
                RaisePropertyChanged();
            }
        }
        private string _basicVersion = "...";

        public string OpenFxVersion
        {
            get => _openFxVersion; set
            {
                _openFxVersion = value;
                RaisePropertyChanged();
            }
        }
        private string _openFxVersion = "...";

        public string CoreLibVersion
        {
            get => _coreLibVersion; set
            {
                _coreLibVersion = value;
                RaisePropertyChanged();
            }
        }
        private string _coreLibVersion = "...";

        public string AdbVersion
        {
            get => _adbVersion; set
            {
                _adbVersion = value;
                RaisePropertyChanged();
            }
        }
        private string _adbVersion = "...";

        public ICommand UpdateCheck
        {
            get => _updateCheck; set
            {
                _updateCheck = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _updateCheck;

        public ICommand OpenLogFloder
        {
            get => _openLogFloder; set
            {
                _openLogFloder = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _openLogFloder;


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
            }
        }

        public bool StartCmdAtDesktop
        {
            get
            {
                return Settings.Default.StartCmdAtDesktop;
            }
            set
            {
                Settings.Default.StartCmdAtDesktop = value;
                RaisePropertyChanged();
            }
        }

        public bool StartCmdAtDesktopEnable
        {
            get
            {
                return Settings.Default.EnvVarCmdWindow;
            }
            set
            {
                RaisePropertyChanged();
            }
        }

        public bool UseEnvVarCmd
        {
            get => Settings.Default.EnvVarCmdWindow;
            set
            {
                Settings.Default.EnvVarCmdWindow = value;
                StartCmdAtDesktopEnable = value;
                if (!value)
                {
                    StartCmdAtDesktop = false;
                }
            }
        }

        public bool DoubleClickToStartExtension
        {
            get => Settings.Default.DoubleClickRunExt;
            set
            {
                Settings.Default.DoubleClickRunExt = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SendToDesktop { get; private set; }

        public ICommand ShowDebugWindow { get; private set; }

        public ICommand ResetSettings { get; private set; }

        public string ThemeDisplayMemberPath { get; set; } = nameof(ITheme.Name);
        public string LanguageDisplayMemberPath { get; set; } = nameof(ILanguage.LangName);

        #endregion
        public VMSettingsDialog()
        {
            RaisePropertyChangedOnDispatcher = true;
            ResetSettings = new MVVMCommand(ResetSettingsMethod);
            SendToDesktop = new MVVMCommand((_) =>
            {
                ShortcutHelper.CreateShortcutOnDesktop("AutumnBox", System.Environment.CurrentDirectory + "/AutumnBox.GUI.exe", "The AutumnBox-Dream of us");
            });
            ShowDebugWindow = new MVVMCommand((_) =>
            {
                new LogWindow().Show();
            });
            UpdateCheck = new FlexiableCommand(() =>
            {
                Updater.RefreshAsync(() =>
                {
                    Updater.ShowUI(true, true);
                });
            });
            OpenLogFloder = new MVVMCommand((p) =>
            {
                try
                {
                    Process.Start(LoggingStation.LOG_FLODER);
                }
                catch { }
            });
            Task.Run(() => LoadVersionInfoAsync());
        }

        private void LoadVersionInfoAsync()
        {
            try
            {
                GUIVersion = Self.Version.ToString();
                BasicVersion = typeof(Basic.ManagedAdb.LocalAdbServer).Assembly.GetName().Version.ToString();
                OpenFxVersion = OpenFramework.BuildInfo.SDK_VERSION.ToString();

                var coreLibFilterResult = from lib in OpenFramework.Management.OpenFx.LibsManager.Librarians
                                          where lib.Name == "AutumnBox Core Modules"
                                          select lib;
                if (coreLibFilterResult.Count() == 0) return;
                var assemblyLib = coreLibFilterResult.First() as AssemblyBasedLibrarian;
                Assembly assembly = assemblyLib.ManagedAssembly;
                CoreLibVersion = assembly.GetName().Version.ToString();
                string versionOutput = new AdbCommand("version").Execute().Output;
                var match = Regex.Match(versionOutput, @"[\w|\s]*[version\s](?<name>[\d|\.]+)([\r\n|\n]*)Version\s(?<code>\d+)", RegexOptions.Multiline);
                if (match.Success)
                {
                    AdbVersion = match.Result("${name}(${code})");
                }
            }
            catch
            {
            }
        }

        private void ResetSettingsMethod(object para)
        {
            Settings.Default.Reset();
            Settings.Default.Save();
            foreach (var prop in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                RaisePropertyChanged(prop.Name);
            }
        }
    }
}
