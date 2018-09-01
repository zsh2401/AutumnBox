/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:53:29 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Net;

namespace AutumnBox.Basic.Device
{
    public sealed class NetDevice : DeviceBase
    {
        public IPEndPoint IPEndPoint { get; internal set; }
        public void Disconnect(bool closeNetDebugging = true)
        {
            throw new NotImplementedException();
        }
    }
}
