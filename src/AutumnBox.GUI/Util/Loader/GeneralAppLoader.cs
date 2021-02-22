using AutumnBox.Basic;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Views.Controls;
using AutumnBox.Logging;
using AutumnBox.OpenFramework;
using HandyControl.Data;
using HandyControl.Tools;
using System;
using System.IO;
using System.Linq;
using System.Management;

namespace AutumnBox.GUI.Util.Loader
{
    sealed class GeneralAppLoader : AbstractAppLoader
    {
#pragma warning disable IDE0051 // 删除未使用的私有成员

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
            ConfigHelper.Instance.SystemVersionInfo = GetSystemVersionInfo();
        }

        [Step(6)]
        private void InitializeLanguageSystem(ILanguageManager languageManager, ISettings settings, IStorageManager storageManager)
        {
            if (storageManager.IsFirstLaunch)
            {
                languageManager.ApplyByEnvoriment();
            }
            else
            {
                languageManager.ApplyByLanguageCode(settings.LanguageCode);
            }
        }

        [Step(5)]
        private void InitErrorHandlerSystem()
        {
#if !DEBUG
            App.Current.DispatcherUnhandledException += FatalHandler.Current_DispatcherUnhandledException;
#endif
        }

        [Step(0)]
        private void InitLogSystem(ILoggingManager loggingManager)
        {
            loggingManager.Initialize();
        }

        [Step(1)]
        private void PrintInformations(IBuildInfo buildInfo)
        {
            Logger.Info("======================");
            Logger.Info($"Installed as {Environment.CurrentDirectory}");
            Logger.Info($"Run as {(Self.HaveAdminPermission ? "Administrator" : "Normal user")}");
            Logger.Info($"AutumnBox version: {Self.Version}");
            Logger.Info($"Core library version: {BuildInfo.SDK_VERSION}");
            Logger.Info($"Windows version: {Environment.OSVersion.Version}");
            Logger.Info($"Clr version: {Environment.Version}");
            Logger.Info($"Commit: {buildInfo.LatestCommit}");
            Logger.Info("======================");
        }

        [Step(3)]
        private void InitAutumnBoxBasic(IOperatingSystemService operatingSystemService)
        {
            try
            {
                Logger.Info("killing other adb processes");
                operatingSystemService.KillProcess("adb.exe");
                Logger.Info("autumnbox-adb-server is starting");
                BasicBooter.Use<Win32AdbManager>();
                Logger.Info($"autumnbox-adb-server is started at {BasicBooter.ServerEndPoint}");
            }
            catch (Exception e)
            {
                Logger.Warn("there's some error happened while starting autumnbox-adb-server", e);
                throw e;
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
        private void DisplayGuide(ISettings settings)
        {
            settings.GuidePassed = true;
        }

#pragma warning restore IDE0051 // 删除未使用的私有成员
    }
}
