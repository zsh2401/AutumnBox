/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 2:55:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.MapleLeaf.Adb;
using AutumnBox.MapleLeaf.Data;
using AutumnBox.MapleLeaf.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.MapleLeaf.Android.Shell
{
    public class AndroidShell : IAndroidShell
    {
        private NetworkStream stream;
        private AdbClientWarpper clientWarpper;
        private CancellationTokenSource readingTokenSource;

        public AndroidShell(string serialNumber)
        {
            clientWarpper = new AdbClientWarpper(AdbService.Instance.CreateClient());
            clientWarpper.SetDevice(serialNumber);
            clientWarpper.SendRequest("shell:",false);
            stream = new NetworkStream(clientWarpper.AdbClient.InnerSocket);
            readingTokenSource = new CancellationTokenSource();
            Task.Run(() =>
            {
                ReadingLoop();
            });
        }

        public event OutputReceivedEventHandler OutputReceived;

        public void InputLine(string inputContent)
        {
            var stdinBytes = inputContent.ToStdInput();
            stream.Write(stdinBytes, 0, stdinBytes.Length);
        }

        private void ReadingLoop()
        {
            byte[] buffer = new byte[1024 * 1024];
            int readCount = 0;
            while (stream.CanRead && !readingTokenSource.IsCancellationRequested)
            {
                readCount = stream.Read(buffer, 0, buffer.Length);
                if (readCount == 0) continue;
                else
                {
                    var str = Encoding.UTF8.GetString(buffer, 0, readCount);
                    OutputReceived?.Invoke(this, new OutputReceivedEventArgs(str));
                }
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    stream.Dispose();
                    clientWarpper.Dispose();
                    readingTokenSource.Cancel();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                stream = null;
                clientWarpper = null;
                readingTokenSource = null;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AndroidShell() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
