/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:46:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 简单的文件系统管理实现
    /// </summary>
    public class FileSystem : DeviceCommander
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public FileSystem(IDevice device) : base(device)
        {
        }

        /// <summary>
        /// CAT命令查看文件内容
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string Cat(string file)
        {
            var result = CmdStation.GetShellCommand(Device, "cat " + file)
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
            return result.Output.ToString();
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <param name="isDir"></param>

        public void Copy(string src, string target, bool isDir)
        {
            var command = isDir ?
                CmdStation.GetShellCommand(Device, $"cp -r {src} {target}")
                : CmdStation.GetShellCommand(Device, $"cp {src} {target}");
            command.To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public void Delete(string file)
        {
            var result = CmdStation
                .GetShellCommand(Device, $"rm -rf {file}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 调用find命令寻找文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Output Find(string name)
        {
            var result = CmdStation
                .GetShellCommand(Device, $"find {name}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
            return result.Output;
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="file"></param>
        public void Mkdir(string file)
        {
            var result = CmdStation
                .GetShellCommand(Device, $"mkdir {file}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <param name="isDir"></param>
        public void Move(string src, string target, bool isDir)
        {
            var command = isDir ?
                CmdStation.GetShellCommand(Device, $"mv -r {src} {target}")
                : CmdStation.GetShellCommand(Device, $"mv {src} {target}");
            command.To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
    }
}
