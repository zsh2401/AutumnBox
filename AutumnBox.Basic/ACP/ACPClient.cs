/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/29 8:54:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ACP
{
    public class ACPClient
    {
        public enum ACPClientStatus
        {
            Ready,
            Sending,
            Sended,
            Receiving,
            Rececived,
            Finished,
        }
        private ACPClient()
        {

        }
        internal Socket Socket { get;private set; }
        public ACPClientStatus Status { get; internal set; }
        internal static ACPClient GetClient(DeviceSerial device)
        {
            Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client.Connect(GetEndPoint(device));
            return new ACPClient() { Socket = client, Status = ACPClientStatus.Ready };
        }
        /// <summary>
        /// 获取一个合适的接入点,如果设备是USB链接,则使用端口转发,如果是局域网链接,则直接连接
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        private static IPEndPoint GetEndPoint(DeviceSerial serial)
        {
            if (serial.IsIpAdress)
            {
                return new IPEndPoint(IPAddress.Parse(serial.ToString()), ACPConstants.STD_PORT);
            }
            else
            {
                return new IPEndPoint(IPAddress.Parse("127.0.0.1"),
                    ForwardManager.GetForwardByRemotePort(serial, ACPConstants.STD_PORT));
            }
        }
    }
}
