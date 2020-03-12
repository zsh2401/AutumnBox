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

using AutumnBox.OpenFramework.Implementation;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.ADBKit;
using AutumnBox.OpenFramework.Open.ProxyKit;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 湖加载器
    /// </summary>

    internal static class LakeLoader
    {
        public static void Load(IBaseApi baseApi)
        {
            LakeProvider.Lake = new ChinaLake();
            LakeProvider.Lake.RegisterSingleton<IProxyBuilder>(new ProxyBuilder());
            LakeProvider.Lake.RegisterSingleton<IBaseApi>(baseApi);
            LakeProvider.Lake.RegisterSingleton<ITaskManager, TaskManagerImpl>();
            LakeProvider.Lake.RegisterSingleton<IMd5, Md5Impl>();
            LakeProvider.Lake.RegisterSingleton<ISoundService, SoundImpl>();
            LakeProvider.Lake.RegisterSingleton<IEmbeddedFileManager, EmbeddedFileManagerImpl>();
            LakeProvider.Lake.RegisterSingleton<IDeviceManager, DeviceSelectorImpl>();
            LakeProvider.Lake.RegisterSingleton<IOperatingSystemAPI, OSImpl>();
            LakeProvider.Lake.Register<IStorageManager, StorageManagerImpl>();
        }
    }
}
