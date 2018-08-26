/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 1:51:17 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.MapleLeaf.Adb
{
    public class AdbService : IAdbService
    {
        public static IAdbService Instance { get; private set; }

        public ushort Port { get; private set; } = 5037;

        private const string ADB_EXECUTABLE_FILE = @"google\platform-tools\adb.exe";

        public bool IsAlive
        {
            get
            {
                try
                {
                    using (IAdbClient client = CreateClient(false)) { }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        static AdbService()
        {
            Instance = new AdbService();
        }

        public IAdbClient CreateClient(bool connectAfterCreated=true)
        {
            var client = new AdbClient(false)
            {
                Port = this.Port
            };
            if (connectAfterCreated)
                client.Connect();
            return client;
        }

        public void Start(ushort port)
        {
            Port = port;
            var pStartInfo = new ProcessStartInfo()
            {
                FileName = ADB_EXECUTABLE_FILE,
                Arguments = "-P " + this.Port + " start-server",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var p = Process.Start(pStartInfo);
            p.WaitForExit();
            if (p.ExitCode != 0)
            {
                throw new Exception(p.StandardOutput.ReadToEnd());
            }
        }

        public void Kill()
        {
            var pStartInfo = new ProcessStartInfo()
            {
                FileName = ADB_EXECUTABLE_FILE,
                Arguments = "-P " + this.Port + " kill-server",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var p = Process.Start(pStartInfo);
            p.WaitForExit();
            if (p.ExitCode != 0)
            {
                throw new Exception(p.StandardOutput.ReadToEnd());
            }
        }


        ~AdbService()
        {
            Dispose();
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
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                Kill();
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AdbService() {
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
