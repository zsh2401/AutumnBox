#nullable enable
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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
        public static ICommandProcedureManager CommandProcedureManager
        {
            get
            {
                if (_adbManager == null)
                {
                    throw new InvalidOperationException("Please load adb manager first!");
                }
                if (_cachedCpm == null || _cachedCpm.DisposedValue)
                {
                    _cachedCpm = _adbManager.OpenCommandProcedureManager();
                }
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
        public static IPEndPoint ServerEndPoint
        {
            get
            {
                if (_adbManager == null) throw new InvalidOperationException("Please load adb manager first");
                return _adbManager.ServerEndPoint;
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
        public static void Load<TAdbManager>() where TAdbManager : IAdbManager, new()
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
