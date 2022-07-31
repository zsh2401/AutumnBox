﻿using AutumnBox.Basic.ManagedAdb.CommandDriven;
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace AutumnBox.GUI.Util
{
    public class Win32AdbManager : StandardAdbManager, IAdbManager
    {
        public const string WIN32_ADB_BINARY_DIRECTORY = "adb_binary/win32";
        public const string LINUX_ADB_BINARY_DIRECTORY = "adb_binary/linux";
        public const string DARWIN_ADB_BINARY_DIRECTORY = "adb_binary/macos";

        public static bool Avaliable
        {
            get
            {
                return Directory.Exists(WIN32_ADB_BINARY_DIRECTORY);
            }
        }

        protected override DirectoryInfo InitializeClientFiles()
        {
            //Just support win32 currently.
            var toolsDir = new DirectoryInfo(toolsDirPath);
            if (!toolsDir.Exists) throw new InvalidOperationException("Adb files not found!");
            return toolsDir;
        }

        private readonly string toolsDirPath;
        public Win32AdbManager(string platformToolsDirectoryPath)
        {
            toolsDirPath = platformToolsDirectoryPath;
        }
        public Win32AdbManager() : this(WIN32_ADB_BINARY_DIRECTORY) { }
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
