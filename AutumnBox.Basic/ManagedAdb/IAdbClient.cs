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
    /// <summary>
    /// ADB客户端
    /// </summary>
    public interface IAdbClient : IDisposable
    {
        /// <summary>
        /// 内部的Socket
        /// </summary>
        Socket InnerSocket { get; }
        /// <summary>
        /// 判断是否已经连接
        /// </summary>
        bool IsConnected { get; }
        /// <summary>
        /// 连接
        /// </summary>
        void Connect();
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="request"></param>
        void SendRequest(string request);
        /// <summary>
        /// 接收状态数据
        /// </summary>
        /// <returns></returns>
        byte[] ReceiveState();
        /// <summary>
        /// 接收Body
        /// </summary>
        /// <returns></returns>
        byte[] ReceiveData();
    }
}
