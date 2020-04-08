using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.OS;
using AutumnBox.Logging;
using AutumnBox.Logging.Management;
using AutumnBox.OpenFramework;
using HandyControl.Data;
using HandyControl.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Loader
{
    internal sealed class AppLoader
    {
        private readonly IEnumerable<MethodInfo> stepMethods;
        private readonly ILogger logger;
        internal AppLoader()
        {
            logger = LoggerFactory.Auto<AppLoader>();
            stepMethods = FindStepMethods();
        }
        private const BindingFlags FINDING_FLAGS = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        private IEnumerable<MethodInfo> FindStepMethods()
        {
            return from method in GetType().GetMethods(FINDING_FLAGS)
                   where method.GetCustomAttribute<StepAttribute>() != null
                   orderby method.GetCustomAttribute<StepAttribute>().Step ascending
                   select method;
        }

        public Task LoadAsync()
        {
            return new Task(Load);
        }
        public void Load()
        {
            foreach (var step in stepMethods)
            {
                step.Invoke(this, new object[0]);
            }
        }

#pragma warning disable IDE0051 // 删除未使用的私有成员
        [Step(0)]
        private void CheckOtherAutumnBox()
        {
            if (!OtherProcessChecker.ThereIsOtherAutumnBox())
            {
                Fail();
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
        private void InitThemeSystem()
        {
            Util.Theme.ThemeManager.Instance.Reload();
        }
        [Step(6)]
        private void InitLanguageSystem()
        {
            if (Settings.Default.IsFirstLaunch)
            {
                LanguageManager.Instance.ApplyByEnvoriment();
            }
            else
            {
                LanguageManager.Instance.ApplyByLanguageCode(Settings.Default.Language);
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
        private void InitLogSystem()
        {
            LoggingStation.Instance.Work();
            LoggingManager.SetLogStation(LoggingStation.Instance, true);
        }
        private void ShowGuideIfNeed()
        {
            bool need = !Settings.Default.GuidePassed;
            //如果没有通过引导,启动引导
            if (need)
            {
                Task<object> dialogTask = null;
                App.Current.Dispatcher.Invoke(() =>
                {
                    dialogTask = DialogManager.Show(MainWindowBus.TOKEN_DIALOG, new ContentGuide());
                });
                dialogTask.Wait();
                Settings.Default.GuidePassed = ((dialogTask.Result as bool?) == true);
            }
            if (!Settings.Default.GuidePassed)
            {
                Fail();
            }
        }
        private void ShowDebugWindowIfNeed()
        {
            //如果设置在启动时打开调试窗口
            if (Settings.Default.ShowDebuggingWindowNextLaunch)
            {
                //打开调试窗口
                App.Current.Dispatcher.Invoke(() =>
                {
                    WinM.D("Log");
                });
            }
        }
        private void PrintInformations()
        {
            logger.Info("======================");
            logger.Info($"Run as " + (Self.HaveAdminPermission ? "Admin" : "Normal user"));
            logger.Info($"AutumnBox version: {Self.Version}");
            logger.Info($"SDK version: {BuildInfo.SDK_VERSION}");
            logger.Info($"Windows version {Environment.OSVersion.Version}");
            logger.Info("======================");
        }
        [Step(3)]
        private void InitAutumnBoxBasic()
        {
            Basic.Util.Settings.CreateNewWindow = Settings.Default.DisplayCmdWindow;
            try
            {
                logger.Info("killing other adb processes");
                TaskKill.Kill("adb.exe");
                logger.Info("autumnbox-adb-server is starting");
                var adbManager = AdbProviderFactory.Get(true).AdbManager;
                Adb.Load(adbManager);
                Adb.Server.Start();
                logger.Info($"autumnbox-adb-server is started at {Adb.Server.IP}:{Adb.Server.Port}");
            }
            catch (Exception e)
            {
                logger.Warn("there's some error happened while starting autumnbox-adb-server", e);
                App.Current.Dispatcher.Invoke(() =>
                {
                    new AdbFailedWindow(e.Message)
                    {
                        Owner = App.Current.MainWindow
                    }.ShowDialog();
                });
                Fail();
            }
        }
        [Step(4)]
        private void InitAutumnBoxOpenFx()
        {
            OpenFrameworkManager.Init();
            OpenFxEventBus.OnLoaded();
        }
        private void InitUtilities()
        {
            _ = VersionInfos.Adb;
        }
        private void RunDeviceListener()
        {
            ConnectedDevicesListener.Instance.Work();
        }
        private void FetchRemoteData()
        {
            _ = new RemoteInteractivator().DoInteractivate();
            //Updater.Do();
            //Statistics.Do();
            //ToastMotd.Do();
            //Banner.Check();
        }
#pragma warning restore IDE0051 // 删除未使用的私有成员
    }
}
