/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 2:10:31 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// Adb服务抽象化的控制对象
    /// </summary>
    public class AdbServer : IAdbServer
    {
        /// <summary>
        /// 实例(单例)
        /// </summary>
        public static IAdbServer Instance { get; private set; }

        /// <summary>
        /// 端口
        /// </summary>
        public ushort Port { get; private set; }

        static AdbServer()
        {
            Instance = new AdbServer();
        }
        /// <summary>
        /// 构造Adb服务
        /// </summary>
        private AdbServer() { }
        /// <summary>
        /// 存活检测
        /// </summary>
        /// <returns></returns>
        public bool AliveCheck()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="port"></param>
        public void Start(ushort port = 54030)
        {
            this.Port = port;
            new AdbCommand($"-P {Port} start-server").Execute()
               .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 重启
        /// </summary>
        public void Restart()
        {
            Kill();
            Start();
        }
        /// <summary>
        /// 杀死
        /// </summary>
        public void Kill()
        {
            new AdbCommand($"-P {Port} kill-server").Execute()
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
        ~AdbServer() {
            Dispose();
        }
    }
}
