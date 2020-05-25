using AutumnBox.Basic;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Util;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Management.ExtTask;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModels
{
    class VMMainMenu : ViewModelBase
    {
        [AutoInject]
        private readonly IMessageBus messageBus;
        [AutoInject] ISettings Settings { get; set; }
        public bool DebugMode
        {
            get => Settings.DeveloperMode; set
            {
                Settings.DeveloperMode = value;
                messageBus.SendMessage(Messages.REFRESH_EXTENSIONS_VIEW);
                RaisePropertyChanged();
            }
        }
        public bool ShowDebugWindowNextTime
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
        public ICommand Exit { get; }
        public ICommand OpenShell { get; }
        public ICommand UpdateCheck { get; }
        public ICommand InstallExtension { get; }
        public ICommand Restart { get; }
        public ICommand OpenExtFloder { get; }
        public ICommand ViewGuide { get; }
        public ICommand ExtensionStore { get; }
        public ICommand HowToInstallExtension { get; }
        public ICommand ViewLibs { get; }

        [AutoInject]
        private readonly IOpenFxManager openFxManager;
        [AutoInject]
        private readonly IWindowManager windowManager;
        public VMMainMenu()
        {
            HowToInstallExtension = new MVVMCommand(p => this.GetComponent<IExtensionTaskManager>().Start("EAutumnBoxGuideViewer", new System.Collections.Generic.Dictionary<string, object>() { { "path", "basic/install_extension" } }));
            ExtensionStore = new MVVMCommand(p => openFxManager.RunExtension("EAutumnBoxExtensionStore"));
            ViewGuide = new MVVMCommand(p => openFxManager.RunExtension("EAutumnBoxGuideViewer"));
            Restart = new MVVMCommand(p => openFxManager.RunExtension("EAutumnBoxRestarter"));
            Exit = new MVVMCommand(p => { App.Current.Shutdown(0); });
            UpdateCheck = new MVVMCommand(P => openFxManager.RunExtension("EAutumnBoxUpdateChecker"));
            OpenShell = new MVVMCommand(p => OpenShellMethod(p?.ToString()));
            InstallExtension = new MVVMCommand(p => openFxManager.RunExtension("EExtensionInstaller"));
            OpenExtFloder = new MVVMCommand(p =>
            {
                Process.Start(new ProcessStartInfo(BuildInfo.ExtensionStore.FullName)
                {
                    UseShellExecute = true,
                    Verb = "open"
                });
            });
            ViewLibs = new MVVMCommand(p => windowManager.ShowDialog("Libs"));
        }
        private void OpenShellMethod(string fileName)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                WorkingDirectory = BasicBooter.AdbClientDirectory.ToString(),
                FileName = fileName ?? "cmd.exe",
                UseShellExecute = false,
                Verb = "runas",
            };
            info.EnvironmentVariables["ANDROID_ADB_SERVER_PORT"] = BasicBooter.ServerEndPoint.Port.ToString();
            if (Settings.EnvVarCmdWindow)
            {
                var pathEnv = info.EnvironmentVariables["path"];
                info.EnvironmentVariables["path"] = $"{BasicBooter.AdbClientDirectory.FullName};" + pathEnv;
            }
            if (Settings.StartCmdAtDesktop)
            {
                info.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            Process.Start(info);
        }
    }
}
