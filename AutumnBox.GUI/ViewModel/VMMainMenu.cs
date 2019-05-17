using AutumnBox.Basic.ManagedAdb;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.View.Slices;
using AutumnBox.GUI.View.Windows;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainMenu : ViewModelBase
    {
        public ICommand Exit { get;  }
        public ICommand OpenLoggingWindow { get;  }
        public ICommand OpenUpdateLogs { get;  }
        public ICommand OpenSettings { get;  }
        public ICommand OpenShell { get;  }
        public ICommand UpdateCheck { get;  }
        public ICommand OpenOSInformation { get; }
        public VMMainMenu()
        {
            Exit = new MVVMCommand(p => { App.Current.Shutdown(0); });
            OpenLoggingWindow = new MVVMCommand(p => { new LogWindow().Show(); });
            OpenUpdateLogs = new MVVMCommand(p => MainWindowBus.ShowSlice(new UpdateLog(), "Farewell under the stars"));
            OpenSettings = new MVVMCommand(p => MainWindowBus.ShowSlice(new Settings(), "Settings"));
            UpdateCheck = new MVVMCommand(P => MainWindowBus.Info("wtf"));
            OpenOSInformation = new MVVMCommand(p => MainWindowBus.ShowSlice(new OpenSourceInformation(),"Open Source"));
            OpenShell = new MVVMCommand(p =>
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    WorkingDirectory = Adb.AdbToolsDir.FullName,
                    FileName = "cmd",
                    UseShellExecute = false,
                    Verb = "runas",
                };
                info.EnvironmentVariables["ANDROID_ADB_SERVER_PORT"] = Adb.Server.Port.ToString();
                if (Properties.Settings.Default.EnvVarCmdWindow)
                {
                    var pathEnv = info.EnvironmentVariables["path"];
                    info.EnvironmentVariables["path"] = $"{Adb.AdbToolsDir.FullName};" + pathEnv;
                }
                if (Properties.Settings.Default.StartCmdAtDesktop)
                {
                    info.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                info.FileName = p?.ToString() ?? "cmd.exe";
                Process.Start(info);
            });
        }
    }
}
