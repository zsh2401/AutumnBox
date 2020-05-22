/*

* ==============================================================================
*
* Filename: StorageManagerImpl
* Description: 
*
* Version: 1.0
* Created: 2020/5/19 19:57:18
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.Util;
using AutumnBox.Leafx.Container.Support;
using System;
using System.IO;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IStorageManager))]
    class StorageManagerImpl : IStorageManager
    {
        public StorageManagerImpl()
        {
            InitializeCacheDirectory();
            InitializeStorageDirectory();
        }

        private void InitializeCacheDirectory()
        {
#if DEBUG || GREEN_RELEASE
            CacheDirectory = new DirectoryInfo("cache");
#else
            string temp = Environment.GetEnvironmentVariable("TEMP");
            CacheDirectory = new DirectoryInfo(Path.Combine(temp, "AutumnBox"));
#endif
            if (!CacheDirectory.Exists) CacheDirectory.Create();
        }

        private void InitializeStorageDirectory()
        {
#if DEBUG || GREEN_RELEASE
            var atmbDirectory = new DirectoryInfo("storage");
            if (!atmbDirectory.Exists) atmbDirectory.Create();
#else
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            DirectoryInfo atmbDirectory = new DirectoryInfo(Path.Combine(appData, "AutumnBox"));
            if (!atmbDirectory.Exists) atmbDirectory.Create();
#endif
            StorageDirectory = new DirectoryInfo(Path.Combine(atmbDirectory.FullName, Self.Version.ToString()));
            if (!StorageDirectory.Exists)
            {
                IsFirstLaunch = true;
                StorageDirectory.Create();
            }
        }

        public DirectoryInfo CacheDirectory { get; private set; }

        public DirectoryInfo StorageDirectory { get; private set; }

        public bool IsFirstLaunch { get; private set; } = false;
    }
}
