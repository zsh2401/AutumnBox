/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:47:45 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 截图器
    /// </summary>
    public class ScreenCapture : DeviceCommander , Data.IReceiveOutputByTo<ScreenCapture>
    {
        /// <summary>
        /// 构造截图器
        /// </summary>
        /// <param name="device"></param>
        public ScreenCapture(IDevice device) : base(device)
        {
            ShellCommandHelper.CommandExistsCheck(device, "screencap");
        }
        /// <summary>
        /// 截图临时文件目录
        /// </summary>
        public string TmpPath { get; set; } =
            Path.Combine(ManagedAdb.Adb.AdbTmpPathOnDevice, "screencap.png");
        /// <summary>
        /// 截图
        /// </summary>
        public void Capture()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 将刚截的图保存到PC端
        /// </summary>
        /// <param name="saveFile"></param>
        public void SaveToPC(FileInfo saveFile)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 通过To模式订阅输出事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public ScreenCapture To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
