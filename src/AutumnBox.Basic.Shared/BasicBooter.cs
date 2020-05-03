#nullable enable
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using System;
using System.IO;
using System.Net;

namespace AutumnBox.Basic
{
    /// <summary>
    /// AutumnBox.Basic启动器
    /// </summary>
    public static class BasicBooter
    {
        /// <summary>
        /// 获取一个已缓存的命令进程管理器
        /// </summary>
        /// <exception cref="InvalidOperationException">操作无效</exception>
        public static ICommandProcedureManager CommandProcedureManager
        {
            get
            {
                if (_adbManager == null)
                {
                    throw new InvalidOperationException("Please load adb manager first!");
                }
                _cachedCpm ??= _adbManager.OpenCommandProcedureManager();
                _cachedCpm.Disposed += (s, e) =>
                {
                    _cachedCpm = null;
                };
                return _cachedCpm;
            }
        }

        /// <summary>
        /// 无需多言
        /// </summary>
        private static ICommandProcedureManager? _cachedCpm = null;

        /// <summary>
        /// 获取服务器终端点
        /// </summary>
        /// <exception cref="InvalidOperationException">操作无效</exception>
        public static IPEndPoint ServerEndPoint
        {
            get
            {
                if (_adbManager == null) throw new InvalidOperationException("Please load adb manager first");
                return _adbManager.ServerEndPoint;
            }
        }

        /// <summary>
        /// ADB可执行程序的位置
        /// </summary>
        /// <exception cref="InvalidOperationException">操作无效</exception>
        public static FileInfo AdbExecutableFile
        {
            get
            {
                if (_adbManager == null)
                {
                    throw new InvalidOperationException("Please load adb manager first!");
                }
                return _adbManager.AdbExecutableFile;
            }
        }


        /// <summary>
        /// Fastboot可执行程序的位置
        /// </summary>
        /// <exception cref="InvalidOperationException">操作无效</exception>
        public static FileInfo FastbootExecutableFile
        {
            get
            {
                if (_adbManager == null)
                {
                    throw new InvalidOperationException("Please load adb manager first!");
                }
                return _adbManager.FastbootExecutableFile;
            }
        }

        /// <summary>
        /// Fastboot可执行程序的位置
        /// </summary>
        public static DirectoryInfo AdbClientDirectory
        {
            get
            {
                if (_adbManager == null)
                {
                    throw new InvalidOperationException("Please load adb manager first");
                }
                return _adbManager.AdbClientDirectory;
            }
        }
        /// <summary>
        /// 内部存储的adb管理器
        /// </summary>
        private static IAdbManager? _adbManager;

        /// <summary>
        /// 加载ADB管理器
        /// </summary>
        /// <typeparam name="TAdbManager"></typeparam>
        public static void Use<TAdbManager>() where TAdbManager : IAdbManager, new()
        {
            _adbManager = new TAdbManager();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Free()
        {
            _adbManager?.Dispose();
        }
    }
}
