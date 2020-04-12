using AutumnBox.ADBProvider;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.Logging.Management;
using AutumnBox.OpenFramework;
using HandyControl.Data;
using HandyControl.Tools;
using System;
using System.Linq;
using System.Management;

namespace AutumnBox.GUI.Util.Loader
{
    sealed class GeneralAppLoader : AbstractAppLoader
    {
#pragma warning disable IDE0051 // 删除未使用的私有成员
        [Step(0)]
        private void CheckOtherAutumnBox(IOperatingSystemService operatingSystemService)
        {
            if (!operatingSystemService.ThereIsOtherAutumnBoxProcess())
            {
                OnError("There's other AutumnBox process", new System.Exception("There's other AutumnBox process"));
            }
        }

        private SystemVersionInfo GetSystemVersionInfo()
        {
            var managementClass = new ManagementClass("Win32_OperatingSystem");
            var instances = managementClass.GetInstances();
            foreach (var instance in instances)
            {
                if (instance["Version"] is string version)
                {
                    var nums = version.Split('.').Select(int.Parse).ToList();
                    var info = new SystemVersionInfo(nums[0], nums[1], nums[2]);
                    return info;
                }
            }
            return default(SystemVersionInfo);
        }

        [Step(2)]
        private void InitHandyControl()
        {
            ConfigHelper.Instance.SetSystemVersionInfo(GetSystemVersionInfo());
        }

        [Step(6)]
        private void InitLanguageSystem(ILanguageManager languageManager)
        {
            if (Properties.Settings.Default.IsFirstLaunch)
            {
                languageManager.ApplyByEnvoriment();
            }
            else
            {
                languageManager.ApplyByLanguageCode(Properties.Settings.Default.Language);
            }
        }

        [Step(5)]
        private void InitErrorHandlerSystem()
        {
#if !DEBUG
            App.Current.DispatcherUnhandledException += FatalHandler.Current_DispatcherUnhandledException;
#endif
        }

        [Step(1)]
        private void InitLogSystem(ILoggingManager loggingManager)
        {
            loggingManager.Initialize();
        }

        [Step(7)]
        private void PrintInformations()
        {
            Logger.Info("======================");
            Logger.Info($"Run as " + (Self.HaveAdminPermission ? "Admin" : "Normal user"));
            Logger.Info($"AutumnBox version: {Self.Version}");
            Logger.Info($"SDK version: {BuildInfo.SDK_VERSION}");
            Logger.Info($"Windows version {Environment.OSVersion.Version}");
            Logger.Info("======================");
        }

        [Step(3)]
        private void InitAutumnBoxBasic(IOperatingSystemService operatingSystemService)
        {
            Settings.CreateNewWindow = Properties.Settings.Default.DisplayCmdWindow;
            try
            {
                Logger.Info("killing other adb processes");
                operatingSystemService.KillProcess("adb.exe");
                Logger.Info("autumnbox-adb-server is starting");
                var adbManager = AdbProviderFactory.Get(true).AdbManager;
                Adb.Load(adbManager);
                Adb.Server.Start();
                Logger.Info($"autumnbox-adb-server is started at {Adb.Server.IP}:{Adb.Server.Port}");
            }
            catch (Exception e)
            {
                Logger.Warn("there's some error happened while starting autumnbox-adb-server", e);
                App.Current.Dispatcher.Invoke(() =>
                {
                    new AdbFailedWindow(e.Message)
                    {
                        Owner = App.Current.MainWindow
                    }.ShowDialog();
                });
                OnError("Can't start adb server&client", e);
            }
        }


        [Step(4)]
        private void InitAutumnBoxOpenFx(IOpenFxManager openFxManager)
        {
            openFxManager.LoadOpenFx();
        }

        [Step(8)]
        private void InitUtilities()
        {
            _ = VersionInfos.Adb;
        }

        [Step(9)]
        private void RunDeviceListener(IAdbDevicesManager devicesManager)
        {
            devicesManager.Initialize();
        }
        [Step(10)]
        private void AddLeafCards(ILeafCardManager leafCardManager)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                leafCardManager.Add(new DeviceSelector(), 0);
                leafCardManager.Add(new DeviceDash(), 0);
            });
        }

        [Step(11)]
        private void DisplayGuide()
        {
            Properties.Settings.Default.GuidePassed = true;
        }

        //[Step(10)]
        //private void FetchRemoteData()
        //{
        //    _ = new RemoteInteractivator().DoInteractivate();
        //}
#pragma warning restore IDE0051 // 删除未使用的私有成员
    }
}
