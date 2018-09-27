/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:25:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 视频录制器
    /// </summary>
    public class VideoRecorder : DeviceCommander, Data.IReceiveOutputByTo<VideoRecorder>
    {
        /// <summary>
        /// 创建新的视频录制器实例
        /// </summary>
        /// <param name="device"></param>
        public VideoRecorder(IDevice device) : base(device)
        {
            ShellCommandHelper.SupportCheck(device, "screenrecord");
        }
        /// <summary>
        /// 开始录制
        /// </summary>
        public void Start()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 停止录制
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 将录制完成的文件保存到PC
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
        public VideoRecorder To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
