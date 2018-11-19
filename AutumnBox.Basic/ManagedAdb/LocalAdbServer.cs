/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/9 17:53:47 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;
using AutumnBox.Basic.Util.Debugging;
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
    /// 默认Adb服务器
    /// </summary>
    public sealed class LocalAdbServer : IAdbServer
    {
        /// <summary>
        /// 默认端口
        /// </summary>
        public const ushort DEFAULT_PORT = 54030;
        /// <summary>
        /// 端口
        /// </summary>
        public ushort Port { get; } = DEFAULT_PORT;
        /// <summary>
        /// IP
        /// </summary>
        public IPAddress IP { get; } = IPAddress.Parse("127.0.0.1");
        private readonly Logger logger = new Logger<LocalAdbServer>();
        /// <summary>
        /// 存活检测
        /// </summary>
        /// <returns></returns>
        public bool AliveCheck()
        {
            return true;
        }
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            new ProcessBasedCommand(Adb.AdbFilePath, $"-P {Port} start-server")
                .To((e) =>
                {
                    logger.Info(e.Text);
                })
                .Execute()
               .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 杀死
        /// </summary>
        public void Kill()
        {
            new ProcessBasedCommand(Adb.AdbFilePath, $"-P {Port} kill-server")
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            Kill();
        }
        /// <summary>
        /// 析构
        /// </summary>
        ~LocalAdbServer()
        {
            Dispose();
        }
    }
}
