/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:13:39 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// Windows Manager,基于android wm命令
    /// </summary>
    public class WindowManager : DeviceCommander, Data.IReceiveOutputByTo<WindowManager>
    {
        private const string PATTERN_PHY_SIZE = @"Physical.+size\D+(?<w>\d+)x(?<h>\d+)";
        private const string PATTERN_OVR_SIZE = @"Override.+size\D+(?<w>\d+)x(?<h>\d+)";
        private const string PATTERN_PHY_DENSITY = @"Physical.density\D+(?<value>\d+)";
        private const string PATTERN_OVR_DENSITY = @"Override.density\D+(?<value>\d+)";
        /// <summary>
        /// 构造Windows Manager
        /// </summary>
        /// <param name="device"></param>
        /// <exception cref="Exceptions.CommandNotFoundException">设备不支持wm命令时抛出</exception>
        public WindowManager(IDevice device) : base(device)
        {
            ShellCommandHelper.CommandExistsCheck(device, "wm");
        }
        /// <summary>
        /// 获取或设置Size,基于wm size命令
        /// </summary>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public Size Size
        {
            get
            {
                try
                {
                    return OverrideSize;
                }
                catch
                {
                    return PhysicalSize;
                }
                throw new NotImplementedException();
            }
            set
            {
                CmdStation
                    .GetShellCommand(Device, $"wm size {value.Width}{value.Height}")
                    .To(RaiseOutput)
                    .Execute()
                    .ThrowIfShellExitCodeNotEqualsZero();
            }
        }
        /// <summary>
        /// 物理分辨率
        /// </summary>
        public Size OverrideSize
        {
            get
            {
                var exeResult = CmdStation.GetShellCommand(Device, "wm size")
                      .To(RaiseOutput)
                      .Execute().
                      ThrowIfShellExitCodeNotEqualsZero();
                var match = Regex.Match(exeResult.Output.ToString(), PATTERN_OVR_SIZE);
                if (match.Success)
                {
                    return new Size
                    {
                        Height = int.Parse(match.Result("${h}")),
                        Width = int.Parse(match.Result("${w}")),
                    };
                }
                else
                {
                    var sourceException = new AdbShellCommandFailedException(exeResult.ExitCode, exeResult.Output.ToString());
                    throw new Exception("Can't get override size from this device", sourceException);
                }
            }
        }
        /// <summary>
        /// 被修改过的分辨率
        /// </summary>
        public Size PhysicalSize
        {
            get
            {
                var exeResult = CmdStation.GetShellCommand(Device, "wm size")
                  .To(RaiseOutput)
                  .Execute().
                  ThrowIfShellExitCodeNotEqualsZero();
                var match = Regex.Match(exeResult.Output.ToString(), PATTERN_PHY_SIZE);
                if (match.Success)
                {
                    return new Size
                    {
                        Height = int.Parse(match.Result("${h}")),
                        Width = int.Parse(match.Result("${w}")),
                    };
                }
                else
                {
                    var sourceException = new AdbShellCommandFailedException(exeResult.ExitCode, exeResult.Output.ToString());
                    throw new Exception("Can't get physical size from this device", sourceException);
                }
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
                try
                {
                    return OverrideDensity;
                }
                catch
                {
                    return PhysicalDensity;
                }
            }
            set
            {
                CmdStation
                      .GetShellCommand(Device, $"wm density {value}")
                      .To(RaiseOutput)
                      .Execute()
                      .ThrowIfShellExitCodeNotEqualsZero();
            }
        }
        /// <summary>
        /// 物理DPI
        /// </summary>
        public int PhysicalDensity
        {
            get
            {
                var exeResult = CmdStation.GetShellCommand(Device, "wm density")
                  .To(RaiseOutput)
                  .Execute().
                  ThrowIfShellExitCodeNotEqualsZero();
                var match = Regex.Match(exeResult.Output.ToString(), PATTERN_PHY_DENSITY);
                if (match.Success)
                {
                    return int.Parse(match.Result("${value}"));
                }
                else
                {
                    var sourceException = new AdbShellCommandFailedException(exeResult.ExitCode, exeResult.Output.ToString());
                    throw new Exception("Can't get physical density from this device", sourceException);
                }
            }
        }
        /// <summary>
        /// 被修改过的DPI
        /// </summary>
        public int OverrideDensity
        {
            get
            {
                var exeResult = CmdStation.GetShellCommand(Device, "wm density")
                  .To(RaiseOutput)
                  .Execute().
                  ThrowIfShellExitCodeNotEqualsZero();
                var match = Regex.Match(exeResult.Output.ToString(), PATTERN_OVR_DENSITY);
                if (match.Success)
                {
                    return int.Parse(match.Result("${value}"));
                }
                else
                {
                    var sourceException = new AdbShellCommandFailedException(exeResult.ExitCode, exeResult.Output.ToString());
                    throw new Exception("Can't get physical override from this device", sourceException);
                }
            }
        }
        /// <summary>
        /// 重设Size
        /// </summary>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void ResetSize()
        {
            CmdStation
                    .GetShellCommand(Device, $"wm size reset")
                    .To(RaiseOutput)
                    .Execute()
                    .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 重设Density
        /// </summary>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void ResetDensity()
        {
            CmdStation
                .GetShellCommand(Device, $"wm density reset")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 设置显示区域
        /// </summary>
        /// <param name="left">左边的留白像素</param>
        /// <param name="top">顶部的留白像素</param>
        /// <param name="right">右边的留白像素</param>
        /// <param name="bottom">底部的留白像素</param>
        public void SetOverscan(int left, int top, int right, int bottom)
        {
            CmdStation
                .GetShellCommand(Device, $"wm overscan {left} {top} {right} {bottom}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 重设显示区域
        /// </summary>
        public void ResetOverscan()
        {
            CmdStation
                .GetShellCommand(Device, $"wm overscan reset")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
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
