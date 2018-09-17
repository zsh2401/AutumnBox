/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 2:16:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// ADB客户端包装器
    /// </summary>
    public class AdbClientWarpper : IDisposable
    {
        /// <summary>
        /// 包装的ADB客户端
        /// </summary>
        public IAdbClient AdbClient => core;
        private readonly IAdbClient core;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="core"></param>
        public AdbClientWarpper(IAdbClient core)
        {
            this.core = core;
            if (!core.IsConnected)
            {
                core.Connect();
            }
        }
        /// <summary>
        /// 设置设备
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public AdbResponse SetDevice(string serialNumber)
        {
            return SendRequest($"host:transport:{serialNumber}", false);
        }
        /// <summary>
        /// 设置设备
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public AdbResponse SetDevice(IDevice device)
        {
            return SetDevice(device.SerialNumber);
        }
        /// <summary>
        /// 发送ADB请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="readDataWhenOkay"></param>
        /// <returns></returns>
        public AdbResponse SendRequest(string request, bool readDataWhenOkay = true)
        {
            core.SendRequest(request);
            byte[] state = core.ReceiveState();
            AdbResponse response = new AdbResponse()
            {
                State = state.ToAdbResponseState(),
            };
            if (readDataWhenOkay || !response.IsOkay)
            {
                response.Data = core.ReceiveData();
            }
            return response;
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
                    // TODO: 释放托管状态(托管对象)。
                    core.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AdbClientWarpper() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        /// <summary>
        /// Dispose
        /// </summary>
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
