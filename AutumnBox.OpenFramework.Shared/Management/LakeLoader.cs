/*

* ==============================================================================
*
* Filename: LakeLoader
* Description: 
*
* Version: 1.0
* Created: 2020/3/7 0:39:52
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.Basic.Calling;
using AutumnBox.OpenFramework.Leafx.Container;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Implementation;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.ADBKit;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 湖加载器
    /// </summary>

    internal static class LakeLoader
    {
        public static void Load(IBaseApi baseApi)
        {
            OpenFxLake.Lake
                            .RegisterSingleton<IBaseApi>(baseApi)
                            .RegisterSingleton<ITaskManager, TaskManagerImpl>()
                            .RegisterSingleton<IMd5, Md5Impl>()
                            .RegisterSingleton<ISoundService, SoundImpl>()
                            .RegisterSingleton<IEmbeddedFileManager, EmbeddedFileManagerImpl>()
                            .RegisterSingleton<IDeviceManager, DeviceSelectorImpl>()
                            .RegisterSingleton<IOperatingSystemAPI, OSImpl>()
                            .RegisterSingleton<IUx, UxImpl>()
                            .RegisterSingleton<IAppManager, AppManagerImpl>()
                            .RegisterSingleton<IDeviceManager, DeviceManager>()
                            .RegisterSingleton<IClassTextReader, ClassTextReader>()
                            .RegisterSingleton<ICompApi, CompImpl>()
                            .RegisterSingleton<INotificationManager, NotificationManager>()
                            .RegisterSingleton<ILibsManager>(OpenFxLoader.LibsManager)

                            .Register<IStorageManager, StorageManagerImpl>()
                            .Register<ICommandExecutor, HestExecutor>()
                            .Register<IDevice>(() => LakeProvider.Lake.Get<IBaseApi>().SelectedDevice)
                            .Register<ILeafUI>(() => LakeProvider.Lake.Get<IBaseApi>().NewLeafUI());

            if (OpenFxLake.Lake.Get<IAppManager>().IsDeveloperMode)
            {
                OpenFxLake.Lake.Get<INotificationManager>().SendNotification("Default Lake Loaded");
            }
        }
    }
}
