/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/28 23:37:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        /// 存活检测
        /// </summary>
        /// <returns></returns>
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
