/*

* ==============================================================================
*
* Filename: CoreLoggerBase
* Description: 
*
* Version: 1.0
* Created: 2020/4/26 2:07:19
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 核心日志器的基础
    /// </summary>
    public abstract class CoreLoggerBase : ICoreLogger
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="log"></param>
        public abstract void Log(ILog log);

        /// <summary>
        /// 将两个日志器合并
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static CoreLoggerBase operator +(CoreLoggerBase left, CoreLoggerBase right)
        {
            if (left is MergedCoreLogger lmc)
            {
                lmc.Loggers.Add(right);
                return lmc;
            }
            else if (right is MergedCoreLogger rmc)
            {
                rmc.Loggers.Add(left);
                return rmc;
            }
            else
            {
                var mc = new MergedCoreLogger();
                mc.Loggers.Add(left);
                mc.Loggers.Add(right);
                return mc;
            }
        }

        /// <summary>
        /// 合并日志器的实现
        /// </summary>
        private sealed class MergedCoreLogger : CoreLoggerBase
        {
            public List<CoreLoggerBase> Loggers { get; } = new List<CoreLoggerBase>();
            public override void Log(ILog log)
            {
                Loggers.ForEach(logger => logger.Log(log));
            }
            protected override void Dispose(bool disposing)
            {
                Loggers.ForEach(logger => logger.Dispose(disposing));
                base.Dispose(disposing);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 释放后发生
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// 虚释放函数
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
                Disposed?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// 终结器
        /// </summary>
        ~CoreLoggerBase()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        /// <summary>
        /// 释放函数
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
