#define MUST_SHOW_GUIDE
/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/19 20:39:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.ADBProvider;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.Custom;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.Util.OpenFxManagement;
using AutumnBox.GUI.Util.OS;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Logging;
using AutumnBox.Logging.Management;
using AutumnBox.OpenFramework;
using HandyControl.Data;
using HandyControl.Tools;
using System;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    class AppLoader
    {
        public static AppLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppLoader();
                }
                return _instance;
            }
        }
        private static AppLoader _instance;
        public event EventHandler Loaded
        {
            add
            {
                if (IsLoaded)
                {
                    value?.Invoke(this, new EventArgs());
                }
                else
                {
                    _loadedSource += value;
                }
            }
            remove
            {
                _loadedSource -= value;
            }
        }
        private event EventHandler _loadedSource;
        public event EventHandler Loading;
        public event EventHandler Failed;
        public bool IsLoaded { get; private set; } = false;
        private readonly ILogger logger;
        private AppLoader()
        {
            logger = LoggerFactory.Auto<AppLoader>();
        }
        public Task LoadAsync()
        {
            return Task.Run(() =>
            {
                Load();
            });
        }
        private void Load()
        {
            CheckOtherAutumnBox();
            OnLoading();

            InitLogSystem();
            InitHandyControl();

            InitErrorHandlerSystem();
            InitLanguageSystem();
            ShowGuideIfNeed();
            ShowDebugWindowIfNeed();
            PrintInformations();
            InitAutumnBoxBasic();
            InitAutumnBoxOpenFx();
            InitUtilities();
            RunDeviceListener();
            FetchRemoteData();
            OnLoaded();
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
        private void CheckOtherAutumnBox()
        {
            if (!OtherProcessChecker.Do())
            {
                Fail();
            }
        }
        private void InitHandyControl()
        {
            ConfigHelper.Instance.SetSystemVersionInfo(GetSystemVersionInfo());
        }
        private void InitThemeSystem()
        {
            Util.Theme.ThemeManager.Instance.Reload();
        }
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
        private void InitErrorHandlerSystem()
        {
#if !DEBUG
            App.Current.DispatcherUnhandledException += FatalHandler.Current_DispatcherUnhandledException;
#endif
        }
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
        private void InitAutumnBoxBasic()
        {
            Basic.Util.Settings.CreateNewWindow = Settings.Default.DisplayCmdWindow;
            try
            {
                TaskKill.Kill("adb.exe");
                logger.Info("adb server starting");
                var adbManager = new ATMBWin32AdbManager();
                adbManager.Extract();
                Adb.Load(adbManager);
                Adb.Server.Start();
                logger.Info($"adb server started at {Adb.Server.IP}:{Adb.Server.Port}");
            }
            catch (Exception e)
            {
                logger.Warn("can not start adb server!", e);
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
        private void InitAutumnBoxOpenFx()
        {
            OpenFrameworkManager.Init();
            OpenFxEventBus.OnLoaded();
        }
        private void InitUtilities()
        {
            _ = VersionInformation.Adb;
        }
        private void RunDeviceListener()
        {
            ConnectedDevicesListener.Instance.Work();
        }
        private void FetchRemoteData()
        {
            Updater.Do();
            Statistics.Do();
            ToastMotd.Do();
            Banner.Check();
        }
        private void OnLoading()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Loading?.Invoke(this, new EventArgs());
            });
        }
        private void OnLoaded()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                _loadedSource?.Invoke(this, new EventArgs());
                IsLoaded = true;
            });
        }
        private void Fail()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Failed?.Invoke(this, new EventArgs());
            });
        }
    }
}
