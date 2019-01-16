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
        /// <summary>
        /// 实例
        /// </summary>
        public static readonly LocalAdbServer Instance;
        static LocalAdbServer()
        {
            Instance = new LocalAdbServer();
        }
        private LocalAdbServer()
        {
            _lazyPort = new Lazy<ushort>(() => AllocatePort());
        }
        /// <summary>
        /// 默认端口
        /// </summary>
        public const ushort DEFAULT_PORT = 54030;
        /// <summary>
        /// 端口
        /// </summary>
        public ushort Port
        {
            get
            {
                lock (portGettingLock)
                {
                    if (!running) throw new InvalidOperationException("server is killed");
                    return _lazyPort.Value;
                }
            }
        }
        private Lazy<ushort> _lazyPort;

        private bool running = false;
        private readonly object portGettingLock = new object();

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
            lock (portGettingLock)
            {
                new ProcessBasedCommand(Adb.AdbFilePath, $"-P{_lazyPort.Value} start-server")
                   .To((e) =>
                   {
                       logger.Info(e.Text);
                   })
                   .Execute()
                  .ThrowIfExitCodeNotEqualsZero();
                running = true;
            }
        }
        /// <summary>
        /// 为ture将使得Kill方法失效
        /// </summary>
#if SDK
        internal
#else
        public
#endif
        bool InvalidKill
        { get; set; } = false;

        /// <summary>
        /// 杀死
        /// </summary>
        public void Kill()
        {
            lock (portGettingLock)
            {
                if (InvalidKill) return;
                new ProcessBasedCommand(Adb.AdbFilePath, $"-P{Port} kill-server")
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
                new WindowsCmdCommand("taskkill /F /IM {adb.exe} /T").Execute();
                running = false;
            }
        }
        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            Kill();
        }

        private const ushort MIN_PORT = 1000;
        private const ushort MAX_PORT = ushort.MaxValue;
        private static readonly Random ran = new Random();
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
