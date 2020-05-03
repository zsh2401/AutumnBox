using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 核心日志处理器
    /// </summary>
    public interface ICoreLogger : IDisposable
    {
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="log"></param>
        void Log(ILog log);
    }

    /// <summary>
    /// 表示日志的记录集合
    /// </summary>
    public interface ILogsCollection : INotifyCollectionChanged, IEnumerable<ILog>, IEnumerable
    {
    }
}
