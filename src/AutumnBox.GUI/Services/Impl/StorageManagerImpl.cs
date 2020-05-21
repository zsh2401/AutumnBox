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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IStorageManager))]
    class StorageManagerImpl : IStorageManager
    {
        public StorageManagerImpl()
        {
            string temp = Environment.GetEnvironmentVariable("TEMP");
            CacheDirectory = new DirectoryInfo(Path.Combine(temp, "autumnbox_temp"));
            if (!CacheDirectory.Exists) CacheDirectory.Create();

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            DirectoryInfo atmbDirectory = new DirectoryInfo(Path.Combine(appData, "AutumnBox"));
            if (!atmbDirectory.Exists) atmbDirectory.Create();

            StorageDirectory = new DirectoryInfo(Path.Combine(atmbDirectory.FullName, Self.Version.ToString()));
            if (!StorageDirectory.Exists)
            {
                IsFirstLaunch = true;
                StorageDirectory.Create();
            }
        }
        public DirectoryInfo CacheDirectory { get; }

        public DirectoryInfo StorageDirectory { get; }

        public bool IsFirstLaunch { get; set; } = false;
    }
}
