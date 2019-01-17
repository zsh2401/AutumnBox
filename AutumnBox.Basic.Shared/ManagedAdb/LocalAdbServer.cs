/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/9 17:53:47 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Util;
using AutumnBox.Basic.Util.Debugging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// 默认Adb服务器
    /// </summary>
    public sealed class LocalAdbServer : IAdbServer
    {
        private KilledWrapper Killed { get; set; } = new KilledWrapper();
        private class KilledWrapper
        {
            public bool Value { get; set; } = false;
            public void ThrowIfKilled()
            {
                if (Value)
                {
                    throw new InvalidOperationException("Adb server is killed");
                }
            }
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static readonly LocalAdbServer Instance;

        /// <summary>
        /// 静态单例构造
        /// </summary>
        static LocalAdbServer()
        {
            Instance = new LocalAdbServer();
        }

        /// <summary>
        /// 私有构造方法
        /// </summary>
        private LocalAdbServer()
        {
            _lazyPort = new Lazy<ushort>(() => AllocatePort());
        }

        /// <summary>
        /// 端口
        /// </summary>
        public ushort Port
        {
            get
            {
                return _lazyPort.Value;
            }
        }
        private Lazy<ushort> _lazyPort;

        /// <summary>
        /// IP
        /// </summary>
        public IPAddress IP { get; } = IPAddress.Parse("127.0.0.1");

        /// <summary>
        /// 日志器
        /// </summary>
        private readonly Logger logger = new Logger<LocalAdbServer>();

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
        public void Start()
        {
            lock (Killed)
            {
                new ProcessBasedCommand(Adb.AdbFilePath, $"-P{_lazyPort.Value} start-server")
                   .To((e) =>
                   {
                       logger.Info(e.Text);
                   })
                   .Execute()
                  .ThrowIfExitCodeNotEqualsZero();
                Killed.Value = false;
            }
        }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable
        {
            get
            {
                lock (Killed)
                {
                    return (!Killed.Value);
                }
            }
        }

        /// <summary>
        /// 杀死
        /// </summary>
        public void Kill()
        {
            lock (Killed)
            {
                new ProcessBasedCommand(Adb.AdbFilePath, $"-P{Port} kill-server")
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
                new WindowsCmdCommand("taskkill /F /IM {adb.exe} /T").Execute();
                Killed.Value = true;
            }
        }

        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            Kill();
        }

        /// <summary>
        /// 服务最小端口
        /// </summary>
        private const ushort MIN_PORT = 1000;

        /// <summary>
        /// 最大端口
        /// </summary>
        private const ushort MAX_PORT = ushort.MaxValue;

        /// <summary>
        /// 随机器
        /// </summary>
        private static readonly Random ran = new Random();

        /// <summary>
        /// 分配端口
        /// </summary>
        /// <returns></returns>
        internal static ushort AllocatePort()
        {
            //if (!PortInUse(DEFAULT_PORT)) return DEFAULT_PORT;
            ushort port;
            do
            {
                port = (ushort)ran.Next(MIN_PORT, MAX_PORT);
            } while (PortInUse(port));
            return port;
        }

        /// <summary>
        /// 检查端口是否被使用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        internal static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
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
