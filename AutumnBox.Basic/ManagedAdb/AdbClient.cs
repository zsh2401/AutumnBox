/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 1:40:42 (UTC +8:00)
** desc： ...
*************************************************/
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// ADB客户端实现
    /// </summary>
    public class AdbClient : IAdbClient
    {
        /// <summary>
        /// 内部的Socket
        /// </summary>
        public Socket InnerSocket { get; private set; }
        /// <summary>
        /// 要连接的IP
        /// </summary>
        public IPAddress IP { get; set; } = Adb.Server.IP;
        /// <summary>
        /// 要连接的端口
        /// </summary>
        public ushort Port { get; set; } = Adb.Server.Port;
        /// <summary>
        /// 判断是否已经连接
        /// </summary>
        public bool IsConnected { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="connectAfterCreated">是否在构造后直接连接</param>
        public AdbClient(bool connectAfterCreated = true)
        {
            InnerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (connectAfterCreated)
            {
                Connect();
            }
        }
        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            if (IsConnected) return;
            InnerSocket.Connect(new IPEndPoint(IP, Port));
            IsConnected = true;
        }
        /// <summary>
        /// 接收Body
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 接收状态数据
        /// </summary>
        /// <returns></returns>
        public byte[] ReceiveState()
        {
            byte[] buffer = new byte[4];
            InnerSocket.Receive(buffer);
            return buffer;
            //return Encoding.UTF8.GetString(buffer);
        }
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="request"></param>
        public void SendRequest(string request)
        {
            InnerSocket.Send(request.ToAdbRequest());
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
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
        /// <summary>
        /// Dispose
        /// </summary>
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
