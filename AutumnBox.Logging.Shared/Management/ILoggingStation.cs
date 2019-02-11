using System.Collections.Generic;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 日志管理站,实现此接口的类,可用于管理日志
    /// </summary>
    public interface ILoggingStation
    {
        /// <summary>
        /// 已处理的日志
        /// </summary>
        IEnumerable<ILog> Logs { get; }
        /// <summary>
        /// 处理一个日志
        /// </summary>
        /// <param name="log"></param>
        void Log(ILog log);
    }
}
