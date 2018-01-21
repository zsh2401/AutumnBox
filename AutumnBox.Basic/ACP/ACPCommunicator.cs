/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/21 7:22:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ACP
{
    public class ACPCommunicator
    {
        public event EventHandler<int> ReceivedSizeChanged;
        public event EventHandler<ACPResponseData> Finished;
        private readonly String command;
        public ACPCommunicator(String command) {
            this.command = command;
        }
        public async void RunAsync() {
            await Task.Run(()=> {
                var result = Run();
                Finished?.Invoke(this, result);
            });
        }
        public ACPResponseData Run() {
            Logger.D("enter SendRequest method");
            Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), ACP.STD_PORT));
            Logger.D("socket connect successful");
            client.Send(Encoding.UTF8.GetBytes(command));
            Logger.D("sended data");

            Logger.D("receciving data");
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
                ReceivedSizeChanged?.Invoke(this, totalSize);
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

            //设置返回值
            ACPResponseData result = new ACPResponseData
            {
                FirstCode = dynamicBuffer[0],
                Data = _onlyData,
            };
            return result;
        }
        public static ACPResponseData SendRequest(String command) {
            return new ACPCommunicator(command).Run();
        }
    }
}
