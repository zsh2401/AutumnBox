using AutumnBox.OpenFramework.Leafx;
using AutumnBox.OpenFramework.Leafx.Container.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.OpenFramework.Leafx.Container;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Implementation;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.ADBKit;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Open.LKit;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.Logging;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// OpenFx总管理
    /// </summary>
#if SDK
    internal
#else
    public
#endif
        static class OpenFx
    {

        /// <summary>
        /// 获取OpenFx总湖
        /// </summary>
        public static IRegisterableLake Lake { get; } = new SunsetLake();

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="baseAPI"></param>
        public static void Load(IBaseApi baseAPI)
        {
            RegisterComponent(baseAPI);
        }

        /// <summary>
        /// 刷新拓展模块列表
        /// </summary>
        public static void RefreshExtensionsList()
        {
            Lake.Get<ILibsManager>().Reload();
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public static void Unload()
        {
            //TODO
        }

        /// <summary>
        /// 注册组件
        /// </summary>
        /// <param name="baseApi"></param>
        private static void RegisterComponent(IBaseApi baseApi)
        {
            SLogger.Info(nameof(OpenFx), "Registering componnets");
            //最基础的组件
            Lake.RegisterSingleton<IBaseApi>(baseApi);
            Lake.RegisterSingleton<ILake>(Lake);
            SLogger.Info(nameof(OpenFx), "Base components are registered");

            //管理层面的组件
            Lake.RegisterSingleton<IManagementObjectBuilder, ManagementObjectBuilder>();
            Lake.RegisterSingleton<ILibsManager, DreamLibManager>();
            Lake.RegisterSingleton<IExtensionTaskManager, ExtensionTaskManager>();
            SLogger.Info(nameof(OpenFx), "Management components are registered");

            //API层面的组件
            Lake.RegisterSingleton<ITaskManager, TaskManagerImpl>();
            Lake.RegisterSingleton<IMd5, Md5Impl>();
            Lake.RegisterSingleton<ISoundService, SoundImpl>();
            Lake.RegisterSingleton<IEmbeddedFileManager, EmbeddedFileManagerImpl>();
            Lake.RegisterSingleton<IDeviceManager, DeviceSelectorImpl>();
            Lake.RegisterSingleton<IOperatingSystemAPI, OSImpl>();
            Lake.RegisterSingleton<IUx, UxImpl>();
            Lake.RegisterSingleton<IAppManager, AppManagerImpl>();
            Lake.RegisterSingleton<IDeviceManager, DeviceManager>();
            Lake.RegisterSingleton<IClassTextReader, ClassTextReader>();
            Lake.RegisterSingleton<ICompApi, CompImpl>();
            Lake.RegisterSingleton<INotificationManager, NotificationManager>();
            Lake.RegisterSingleton<IXCardsManager, XCardsManager>();
            SLogger.Info(nameof(OpenFx), "Open API components are registered");

            //一些特殊的实时构建组件
            Lake.Register<IStorageManager, StorageManagerImpl>();
            Lake.Register<ICommandExecutor, HestExecutor>();
            Lake.Register<IDevice>(() => Lake.Get<IBaseApi>().SelectedDevice);
            Lake.Register<ILeafUI>(() => Lake.Get<IBaseApi>().NewLeafUI());

            if (Lake.Get<IAppManager>().IsDeveloperMode)
            {
                Lake.Get<INotificationManager>().SendNotification("Default Lake Loaded");
            }
        }
    }
}
