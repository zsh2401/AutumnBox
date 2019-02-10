/* =============================================================================*\
*
* Filename: DevicesMonitor.cs
* Description: 
*
* Version: 1.0
* Created: 8/18/2017 22:09:36(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.MultipleDevices
{
    /// <summary>
    /// 设备拔插监视器
    /// </summary>
    public class DevicesMonitor
    {
        private static readonly ILogger logger;
        static DevicesMonitor()
        {
            logger = LoggerFactory.Auto<DevicesMonitor>();
        }
        /// <summary>
        /// 设备拔插时间
        /// </summary>
        public event DevicesChangedHandler DevicesChanged;
        private CancellationTokenSource tokenSource;
        private bool isStarted = false;
        /// <summary>
        /// 检测间隔
        /// </summary>
        public int Interval { get; set; } = 2000;
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            if (isStarted)
            {
                return;
            }
            Task.Run(() =>
            {
                tokenSource = new CancellationTokenSource();
                isStarted = true;
                try
                {
                    Loop();
                }
                catch (Exception ex)
                {
                    logger.Warn("Devices monitor error", ex);
                }
                isStarted = false; ;
            });
        }
        /// <summary>
        /// 循环检测设备信息
        /// </summary>
        private void Loop()
        {
            IDevicesGetter getter = new DevicesGetter();
            IEnumerable<IDevice> last = getter.GetDevices();
            DevicesChanged?.Invoke(this, new DevicesChangedEventArgs(last));
            IEnumerable<IDevice> _new = null;
            while (!tokenSource.Token.IsCancellationRequested)
            {
                _new = getter.GetDevices();
                if (!_new.DevicesEquals(last))
                {
                    last = _new;
                    DevicesChanged?.Invoke(this, new DevicesChangedEventArgs(last));
                }
                Thread.Sleep(Interval);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            tokenSource.Cancel();
        }
    }
}
