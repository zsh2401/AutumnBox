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
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Event;
using AutumnBox.Support.CstmDebug;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 连接设备变化的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DevicesChangedHandler(object sender, DevicesChangedEventArgs e);
    /// <summary>
    /// 连接设备变化的事件的参数
    /// </summary>
    public class DevicesChangedEventArgs : EventArgs
    {
        public DevicesList DevicesList { get; }
        public DevicesChangedEventArgs(DevicesList devList)
        {
            DevicesList = devList;
        }
    }
    /// <summary>
    /// 设备监听器
    /// </summary>
    public sealed class DevicesMonitor
    {
        public event DevicesChangedHandler DevicesChanged;//当连接设备的情况变化时发生
        private bool _continue = true;
        private readonly int _interval;
        private IDevicesGetter _devGetter = new DevicesGetter();
        public DevicesMonitor(int interval = 2000)
        {
            this._interval = interval;
        }
        public void Begin() {
            _continue = true;
            ListenAsync();
        }
        private async void ListenAsync()
        {
            var last = new DevicesList();
            while (_continue)
            {
                var now = await Task.Run( ()=> {
                    Thread.Sleep(_interval);
                    return _devGetter.GetDevices(); });
                if (now != last) {
                    Logger.T("Devices Changed");
                    last = now;
                    DevicesChanged?.Invoke(this,new DevicesChangedEventArgs(now));
                }

            }
        }
        public void Cancel()
        {
            _continue = false;
        }
    }
}
