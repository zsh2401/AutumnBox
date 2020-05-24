/*

* ==============================================================================
*
* Filename: Win32AdbManager
* Description: 
*
* Version: 1.0
* Created: 2020/5/24 16:40:03
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace AutumnBox.Tests.Util
{
    public class Win32AdbManager : StandardAdbManager, IAdbManager
    {
        const string WIN32_ADB_BINARY_DIRECTORY = "adb_binary/win32";
        const string LINUX_ADB_BINARY_DIRECTORY = "adb_binary/linux";
        const string DARWIN_ADB_BINARY_DIRECTORY = "adb_binary/macos";
        protected override DirectoryInfo InitializeClientFiles()
        {
            //Just support win32 currently.
            var toolsDir = new DirectoryInfo(WIN32_ADB_BINARY_DIRECTORY);
            if (!toolsDir.Exists) throw new InvalidOperationException("Adb files not found!");
            return toolsDir;
        }

        protected override IPEndPoint StartServer(ushort port = 6605)
        {
            //在随机端口启动ADB服务
            var random = new Random();
            ushort _randomPort;
            do
            {
                _randomPort = (ushort)random.Next(IPEndPoint.MinPort, IPEndPoint.MaxPort);
            } while (PortIsUsinngNow(_randomPort));
            return base.StartServer(_randomPort);
        }

        private bool PortIsUsinngNow(ushort port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }
    }
}
