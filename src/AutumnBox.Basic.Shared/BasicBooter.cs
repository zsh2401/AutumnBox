#nullable enable
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic
{
    public static class BasicBooter
    {
        /// <summary>
        /// 基于命令行的ADB管理器
        /// </summary>
        public static IAdbManager AdbManager
        {
            get
            {
                if (_adbManager == null)
                {
                    throw new InvalidOperationException("Adb Manager has not been load");
                }
                return _adbManager;
            }
            set
            {
                if (value == _adbManager) return;
                _adbManager = value ?? throw new ArgumentNullException("Value can not be null", nameof(value));
                _cachedCpm = null;
            }
        }
        private static IAdbManager? _adbManager;

        /// <summary>
        /// 获取一个已缓存的命令进程管理器
        /// </summary>
        public static ICommandProcedureManager CommandProcedureManager
        {
            get
            {
                if (_cachedCpm == null || _cachedCpm.Disposed)
                {
                    _cachedCpm = AdbManager.OpenCommandProcedureManager();
                }
                return _cachedCpm;
            }
        }

        private static ICommandProcedureManager? _cachedCpm = null;

        //public static object NetAdbManager { get; set; }
    }
}
