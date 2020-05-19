/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:13:39 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Text.RegularExpressions;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Exceptions;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{


    /// <summary>
    /// Windows Manager,基于android wm命令
    /// </summary>
    public class WindowManager
    {
        private const string PATTERN_PHY_SIZE = @"Physical.+size\D+(?<w>\d+)x(?<h>\d+)";
        private const string PATTERN_OVR_SIZE = @"Override.+size\D+(?<w>\d+)x(?<h>\d+)";
        private const string PATTERN_PHY_DENSITY = @"Physical.density\D+(?<value>\d+)";
        private const string PATTERN_OVR_DENSITY = @"Override.density\D+(?<value>\d+)";
        private readonly IDevice device;
        private readonly ICommandExecutor executor;

        /// <summary>
        /// 构造Windows Manager
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        public WindowManager(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
        /// <summary>
        /// 获取或设置Size,基于wm size命令
        /// </summary>
        /// <exception cref="CommandErrorException"></exception>
        public Data.Size Size
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
            }
            set
            {
                executor.AdbShell(device, $"wm size {value.Width}{value.Height}").ThrowIfError();
            }
        }
        /// <summary>
        /// 物理分辨率
        /// </summary>
        /// <exception cref="CommandErrorException">无法正确执行命令</exception>
        /// <exception cref="Exception">无法读取</exception>
        public Data.Size OverrideSize
        {
            get
            {
                var exeResult = executor.AdbShell(device, $"wm size").ThrowIfError();
                var match = Regex.Match(exeResult.Output.ToString(), PATTERN_OVR_SIZE);
                if (match.Success)
                {
                    return new Data.Size
                    {
                        Height = int.Parse(match.Result("${h}")),
                        Width = int.Parse(match.Result("${w}")),
                    };
                }
                else
                {
                    var sourceException = new AdbShellCommandFailedException(exeResult.Output, exeResult.ExitCode);
                    throw new Exception("Can't read the overrided size from target device", sourceException);
                }
            }
        }
        /// <summary>
        /// 被修改过的分辨率
        /// </summary>
        public Data.Size PhysicalSize
        {
            get
            {
                var exeResult = executor.AdbShell(device, $"wm size").ThrowIfError();
                var match = Regex.Match(exeResult.Output.ToString(), PATTERN_PHY_SIZE);
                if (match.Success)
                {
                    return new Data.Size
                    {
                        Height = int.Parse(match.Result("${h}")),
                        Width = int.Parse(match.Result("${w}")),
                    };
                }
                else
                {
                    var sourceException = new AdbShellCommandFailedException(exeResult.Output, exeResult.ExitCode);
                    throw new Exception("Can't read physical size from target device", sourceException);
                }
            }
        }
        /// <summary>
        /// 获取或设置Density,基于wm density命令
        /// </summary>
        /// <exception cref="CommandErrorException">命令无法执行</exception>
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
                executor.AdbShell(device, $"wm density {value}").ThrowIfError();
            }
        }
        /// <summary>
        /// 物理DPI
        /// </summary>
        /// <exception cref="CommandErrorException">命令无法执行</exception>
        public int PhysicalDensity
        {
            get
            {
                var exeResult = executor.AdbShell(device, "wm density").ThrowIfError();
                var match = Regex.Match(exeResult.Output.ToString(), PATTERN_PHY_DENSITY);
                if (match.Success)
                {
                    return int.Parse(match.Result("${value}"));
                }
                else
                {
                    var sourceException = new AdbShellCommandFailedException(exeResult.Output, exeResult.ExitCode);
                    throw new Exception("Can't get physical density from this device", sourceException);
                }
            }
        }
        /// <summary>
        /// 被修改过的DPI
        /// </summary>
        /// <exception cref="CommandErrorException">命令无法执行</exception>
        /// <exception cref="Exception">输出不匹配</exception>
        public int OverrideDensity
        {
            get
            {
                var exeResult = executor.AdbShell(device, "wm density").ThrowIfError();
                var match = Regex.Match(exeResult.Output.ToString(), PATTERN_OVR_DENSITY);
                if (match.Success)
                {
                    return int.Parse(match.Result("${value}"));
                }
                else
                {
                    var sourceException = new AdbShellCommandFailedException(exeResult.Output, exeResult.ExitCode);
                    throw new Exception("Can't get physical override from this device", sourceException);
                }
            }
        }
        /// <summary>
        /// 重设Size
        /// </summary>
        /// <exception cref="CommandErrorException"></exception>
        public void ResetSize()
        {
            executor.AdbShell(device, "wm size reset").ThrowIfError();
        }
        /// <summary>
        /// 重设Density
        /// </summary>
        /// <exception cref="CommandErrorException"></exception>
        public void ResetDensity()
        {
            executor.AdbShell(device, "wm density reset").ThrowIfError();
        }
        /// <summary>
        /// 设置显示区域
        /// </summary>
        /// <param name="left">左边的留白像素</param>
        /// <param name="top">顶部的留白像素</param>
        /// <param name="right">右边的留白像素</param>
        /// <param name="bottom">底部的留白像素</param>
        /// <exception cref="CommandErrorException">命令无法执行</exception>
        public void SetOverscan(int left, int top, int right, int bottom)
        {
            executor.AdbShell(device, $"wm overscan {left} {top} {right} {bottom}").ThrowIfError();
        }
        /// <summary>
        /// 重设显示区域
        /// </summary>
        /// <exception cref="CommandErrorException">命令无法执行</exception>
        public void ResetOverscan()
        {
            executor.AdbShell(device, $"wm overscan reset").ThrowIfError();
        }
    }
}
