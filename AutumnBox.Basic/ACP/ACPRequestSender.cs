/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/21 7:22:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Util;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ACP
{
    public class ReceivedSizeChangedEventArgs
    {
        public int CurrentTotalSize { get; private set; }
        public ReceivedSizeChangedEventArgs(int size)
        {
            this.CurrentTotalSize = size;
        }
    }
    public class ACPRequestSender
    {
        /// <summary>
        /// 超时的时间(超过这个数值时,关闭连接并返回超时错误码)
        /// </summary>
        public int TimeoutValue { get; set; } = ACP.TIMEOUT_VALUE;
        /// <summary>
        /// 当接收到的数据大小变化时发生
        /// </summary>
        public event EventHandler<ReceivedSizeChangedEventArgs> ReceivedSizeChanged;
        /// <summary>
        /// 当任何一条命令处理完成时发生
        /// </summary>
        public event EventHandler<ACPResponseData> Finished;
        private readonly DeviceSerial deviceSerial;
        public ACPRequestSender(DeviceSerial deviceSerial)
        {
            this.deviceSerial = deviceSerial;
        }
        /// <summary>
        /// 异步发送并处理Acp请求
        /// </summary>
        /// <param name="command"></param>
        public async void SendRequestAsync(String command)
        {
            await Task.Run(() =>
            {
                var result = SendRequest(command);
                Finished?.Invoke(this, result);
            });
        }
        /// <summary>
        /// 发送一个命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ACPResponseData SendRequest(String command)
        {
            byte[] datas = null;
            byte fCode = ACP.FCODE_NO_RESPONSE;
            try
            {
                AcpStdFlow(command, out fCode, out datas);
            }
            catch (ACPTimeOutException e)
            {
                fCode = ACP.FCODE_TIMEOUT;
            }
            catch (SocketException e)
            {
                fCode = ACP.FCODE_NO_RESPONSE;
                Logger.T("Communicate failed", e);
            }

            //设置返回值
            ACPResponseData result = new ACPResponseData
            {
                FirstCode = fCode,
                Data = datas,
            };
            return result;
        }
        /// <summary>
        /// 发送一个ACP命令
        /// </summary>
        /// <param name="deviceSerial"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static ACPResponseData SendRequest(DeviceSerial deviceSerial, String command)
        {
            return new ACPRequestSender(deviceSerial).SendRequest(command);
        }
        /// <summary>
        /// 超时检测通讯器,用于在Acp标准处理线程与超时检测线程之间进行通信
        /// </summary>
        private class TimeoutChecker
        {
            public bool ClientIsClosedByTimeout { get; set; } = false;
            public bool ClientIsRuninng { get; set; } = false;
        }
        /// <summary>
        /// 超时检测函数,超时的时候,将会关闭Socket通信
        /// </summary>
        /// <param name="client"></param>
        /// <param name="time"></param>
        /// <param name="checker"></param>
        private async static void TimeOut(Socket client,int time , TimeoutChecker checker)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(time);
            });
            if (checker.ClientIsRuninng)
            {
                Logger.D("request timeout!");
                checker.ClientIsClosedByTimeout = true;
                client.Close();
            }
        }
        /// <summary>
        /// ACP标准流程,其中包括超时检测
        /// </summary>
        /// <param name="command"></param>
        /// <param name="fCode"></param>
        /// <param name="datas"></param>
        private void AcpStdFlow(String command, out byte fCode, out byte[] datas)
        {
            var finishedChecker = new TimeoutChecker();
            Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {
                finishedChecker.ClientIsRuninng = true;
                TimeOut(client, TimeoutValue, finishedChecker);
                client.Connect(GetEndPoint(deviceSerial));
                Send(client, Encoding.UTF8.GetBytes(command));
                Receive(client, out fCode, out datas);
                finishedChecker.ClientIsRuninng = false;
            }
            catch (Exception e)
            {
                Logger.D("checked ex");
                finishedChecker.ClientIsRuninng = false;
                throw e;
            }
            if (finishedChecker.ClientIsClosedByTimeout)
            {
                throw new ACPTimeOutException();
            }
        }
        /// <summary>
        /// 发送ACP标准命令
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        private void Send(Socket client, byte[] request)
        {
            client.Send(request);
        }
        /// <summary>
        /// 接收ACP并简单处理标准返回
        /// </summary>
        /// <param name="client"></param>
        /// <param name="fCode"></param>
        /// <param name="datas"></param>
        private void Receive(Socket client, out byte fCode, out byte[] datas)
        {
            //使用一个List作为动态缓冲区
            List<byte> dynamicBuffer = new List<byte>();
            int totalSize = 0;//数据的总大小
            for (int i = 0; i < int.MaxValue; i++)
            {
                //声明一个4096字节的临时缓冲区
                byte[] buffer = new byte[4096];
                //接收4096字节的数据,并获取实际接收到的数据大小

                int sizeofCrtPkg = client.Receive(buffer);
                //在动态缓冲区中添加刚才接收到的数据
                dynamicBuffer.AddRange(buffer);
                //增加当前已接收的总数据大小
                totalSize += sizeofCrtPkg;
                ReceivedSizeChanged?.Invoke(this, new ReceivedSizeChangedEventArgs(totalSize));
                //如果刚才接收的数据已经不足4096了,说明数据接收完了,退出循环
                //如果数据没接收完,那么继续接收
                if (sizeofCrtPkg < 4096)
                {
                    Logger.D($"data get finish,called {i + 1} Received(),total size{totalSize}");
                    break;
                }
            }
            //接收完毕,开始处理
            Logger.D("rececied data,begin process");
            //先声明足以容纳数据的字节数组,这个字节数组不会包含FirstCode,所以要-1
            byte[] _onlyData = new byte[totalSize - 1];
            //将动态数组里接收的除了FirstCode外的数据完整的拷贝到数据数组
            Array.Copy(dynamicBuffer.ToArray(), 1, _onlyData, 0, _onlyData.Length);
            Logger.D("data processed");
            fCode = dynamicBuffer[0];
            datas = _onlyData;
        }
        /// <summary>
        /// 获取一个合适的接入点,如果设备是USB链接,则使用端口转发,如果是局域网链接,则直接连接
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        private static IPEndPoint GetEndPoint(DeviceSerial serial) {
            if (serial.IsIpAdress)
            {
                return new IPEndPoint(IPAddress.Parse(serial.ToString()), ACP.STD_PORT);
            }
            else {
                return new IPEndPoint(IPAddress.Parse("127.0.0.1"),
                    ForwardManager.GetForwardByRemotePort(serial, ACP.STD_PORT));
            }
        }
    }
}