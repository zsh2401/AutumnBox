using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 核心日志处理器
    /// </summary>
    public interface ICoreLogger : IDisposable
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();
        /// <summary>
        /// 处理日志
        /// </summary>
        /// <param name="log"></param>
        void Log(ILog log);
        /// <summary>
        /// 获取所有的日志
        /// </summary>
        ObservableCollection<ILog> Logs { get; }
    }
}
