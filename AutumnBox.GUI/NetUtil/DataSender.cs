/* =============================================================================*\
*
* Filename: DataSender
* Description: 
*
* Version: 1.0
* Created: 2017/11/28 15:25:58 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.NetUtil
{
    public abstract class DataSender : Object
    {
        protected abstract string Address { get; }
        protected virtual int Port { get; } = 24010;
        protected abstract string Mac { get; }
        protected abstract string Datas { get; }
        private readonly Socket ClientSocket;
        public DataSender()
        {
            IPAddress ip = IPAddress.Parse(Address);
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket.Connect(new IPEndPoint(ip, Port));
        }
        public async void RunAsync(Action callback = null)
        {
            await Task.Run(() =>
            {
                Send();
            });
            callback?.Invoke();
        }
        protected virtual void Send()
        {
            byte[] datas =  Encoding.UTF8.GetBytes(Datas);
            ClientSocket.Send(datas);
        }
    }
}
