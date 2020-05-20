/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:46:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
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
            return Executor.AdbShell(Device, "cat " + file)
                   .ThrowIfShellExitCodeNotEqualsZero().Output.ToString();
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <param name="isDir"></param>
        public void Copy(string src, string target, bool isDir)
        {
            (isDir ?
                Executor.AdbShell(Device, $"cp -r {src} {target}")
                : Executor.AdbShell(Device, $"cp {src} {target}"))
                .ThrowIfShellExitCodeNotEqualsZero();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public void Delete(string file)
        {
            Executor.AdbShell(Device, $"rm -rf {file}")
                 .ThrowIfShellExitCodeNotEqualsZero();
        }

        /// <summary>
        /// 调用find命令寻找文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Output Find(string name)
        {
            return Executor
                 .AdbShell(Device, $"find {name}")
                 .ThrowIfShellExitCodeNotEqualsZero().Output;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="file"></param>
        public void Mkdir(string file)
        {
            Executor.AdbShell(Device, $"mkdir {file}")
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
            (isDir ?
                Executor.AdbShell(Device, $"mv -r {src} {target}")
                : Executor.AdbShell(Device, $"mv {src} {target}"))
                .ThrowIfShellExitCodeNotEqualsZero();
        }
    }
}
