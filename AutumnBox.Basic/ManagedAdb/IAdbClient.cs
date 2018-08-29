/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 1:35:55 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb
{
    public interface IAdbClient : IDisposable
    {
        Socket InnerSocket { get; }
        bool IsConnected { get; }
        void Connect();
        void SendRequest(string request);
        byte[] ReceiveState();
        byte[] ReceiveData();
    }
}
