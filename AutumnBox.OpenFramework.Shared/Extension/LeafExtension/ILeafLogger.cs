using System;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// LeafExtension专用日志器
    /// </summary>
    public interface ILeafLogger
    {
        /// <summary>
        /// Debug类型,仅在秋之盒调试模式开启时可用
        /// </summary>
        /// <param name="message"></param>
        void Debug(object message);
        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="message"></param>
        void Warn(object message);
        /// <summary>
        /// 带有异常的警告信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        void Warn(object message, Exception e);
        /// <summary>
        /// 致命问题
        /// </summary>
        /// <param name="message"></param>
        void Fatal(object message);
        /// <summary>
        /// 带有异常的致命信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Fatal(object message, Exception ex);
        /// <summary>
        /// 简单地打印异常
        /// </summary>
        /// <param name="e"></param>
        void Exception(Exception e);
    }
}
