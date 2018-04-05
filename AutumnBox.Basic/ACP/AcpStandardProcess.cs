/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/29 8:52:34 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ACP
{
    [Obsolete("Project-ACP is dead")]
    internal static class AcpStandardProcess
    {
        private const int tempBufferSize = 4096;
        private const int receiveInterval = 200;

        public static void Send(Socket client, byte[] request)
        {
            client.Send(request);
        }
        public static void Receive(Socket client,
            out byte fCode, out byte[] datas,
            uint timeoutValue = Acp.TIMEOUT_VALUE)
        {
            byte[] buffer = new byte[tempBufferSize];
            List<byte> dynamicBuffer = new List<byte>();
            int totalSize = 0;
            var stream = new NetworkStream(client);
            var timeoutChecker = new TimeoutChecker();
            Timeout(timeoutChecker, client, (int)timeoutValue);
            try
            {
                do
                {
                    totalSize += stream.Read(buffer, 0, buffer.Length);
                    dynamicBuffer.AddRange(buffer);
                    Thread.Sleep(receiveInterval);
                } while (stream.DataAvailable);

                timeoutChecker.IsFinished = true;
                datas = new byte[totalSize - 1];
                fCode = dynamicBuffer[0];
                dynamicBuffer.CopyTo(1, datas, 0, datas.Length);
            }
            catch (Exception e)
            {
                if (timeoutChecker.IsClosedByTimeoutMethod)
                {
                    throw new AcpTimeOutException();
                }
                else
                {
                    throw e;
                }
            }
        }

        private class TimeoutChecker
        {
            public bool IsFinished { get; set; } = false;
            public bool IsClosedByTimeoutMethod { get; set; } = false;
        }
        private static async void Timeout(TimeoutChecker checker, Socket client, int value)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(value);
            });
            if (!checker.IsFinished)
            {
                checker.IsClosedByTimeoutMethod = true;
                client.Close();
            }
        }
    }
}
