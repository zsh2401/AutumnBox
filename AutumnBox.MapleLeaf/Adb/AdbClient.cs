/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 1:40:42 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AutumnBox.MapleLeaf.Adb
{
    public class AdbClient : IAdbClient
    {
        public Socket InnerSocket { get; private set; }

        public IPAddress IP { get; set; } = IPAddress.Parse("127.0.0.1");

        public ushort Port { get; set; } = 5037;

        public bool IsConnected { get; private set; }

        public AdbClient(bool connectAfterCreated = true)
        {
            InnerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (connectAfterCreated)
            {
                Connect();
            }
        }

        public void Connect()
        {
            if (IsConnected) return;
            InnerSocket.Connect(new IPEndPoint(IP, Port));
            IsConnected = true;
        }

        public byte[] ReceiveData()
        {
            byte[] lenBytes = new byte[4];
            InnerSocket.Receive(lenBytes);
            string lenString = Encoding.UTF8.GetString(lenBytes);
            int len = int.Parse(lenString, System.Globalization.NumberStyles.HexNumber);
            byte[] dataBuffer = new byte[len];
            InnerSocket.Receive(dataBuffer);
            return dataBuffer;
        }

        public string ReceiveState()
        {
            byte[] buffer = new byte[4];
            InnerSocket.Receive(buffer);
            return Encoding.UTF8.GetString(buffer);
        }

        public void SendRequest(string request)
        {
            InnerSocket.Send(request.ToAdbRequest());
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    InnerSocket.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AdbClient() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            IsConnected = false;
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
