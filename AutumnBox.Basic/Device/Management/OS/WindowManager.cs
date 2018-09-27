/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:13:39 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// Windows Manager,基于android wm命令
    /// </summary>
    public class WindowManager : DeviceCommander,Data.IReceiveOutputByTo<WindowManager>
    {
        /// <summary>
        /// 构造Windows Manager
        /// </summary>
        /// <param name="device"></param>
        /// <exception cref="Exceptions.CommandNotFoundException">设备不支持wm命令时抛出</exception>
        public WindowManager(IDevice device) : base(device)
        {
            ShellCommandHelper.SupportCheck(device,"wm");
        }
        /// <summary>
        /// 获取或设置Size,基于wm size命令
        /// </summary>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public Size Size
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 获取或设置Density,基于wm density命令
        /// </summary>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public int Density
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 重设Size
        /// </summary>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void ResetSize() {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 重设Density
        /// </summary>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void ResetDensity() {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 通过To模式订阅输出事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        WindowManager IReceiveOutputByTo<WindowManager>.To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
