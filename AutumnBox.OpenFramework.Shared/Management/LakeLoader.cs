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

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 湖加载器
    /// </summary>

    internal static class LakeLoader
    {
        public static void Load(IBaseApi baseApi)
        {
            var lake = new ChinaLake();

            lake.RegisterSingleton(baseApi);
            lake.RegisterSingleton<IMd5, Md5Impl>();
            lake.RegisterSingleton<IEmbeddedFileManager, EmbeddedFileManagerImpl>();
            lake.RegisterSingleton<IDeviceManager, DeviceSelectorImpl>();
            lake.RegisterSingleton<IOperatingSystemAPI, OSImpl>();
            lake.Register<IStorageManager, StorageManagerImpl>();

            LakeProvider.Lake = lake;
        }
    }
}
