/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/28 23:37:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Net;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// Adb服务
    /// </summary>
    public interface IAdbServer : IDisposable
    {
        /// <summary>
        /// IP
        /// </summary>
        IPAddress IP { get; }
        /// <summary>
        /// 端口
        /// </summary>
        ushort Port { get; }
        /// <summary>
        /// 是否可用
        /// </summary>
        bool IsEnable { get; }
        /// <summary>
        /// 存活检测
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        bool AliveCheck();
        /// <summary>
        /// 启动
        /// </summary>
        void Start();
        /// <summary>
        /// 杀死
        /// </summary>
        void Kill();
    }
}
