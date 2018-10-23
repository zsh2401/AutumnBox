/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:46:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result = new AdbCommand("shell cat " + file).Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
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
            var result = isDir ?
                new AdbCommand($"cp -r {src} {target}").Execute()
                : new AdbCommand($"cp {src} {target}").Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public void Delete(string file)
        {
            new AdbCommand($"rm -rf {file}").Execute()
                 .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 调用find命令寻找文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Output Find(string name)
        {
            var result = new AdbCommand($"find {name}").Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
            return result.Output;
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="file"></param>
        public void Mkdir(string file)
        {
            var result = new AdbCommand($"mkdir {file}").Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <param name="isDir"></param>
        public void Move(string src, string target, bool isDir)
        {
            var result = isDir ?
                new AdbCommand($"mv -r {src} {target}").Execute()
                : new AdbCommand($"mv {src} {target}").Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
        }
    }
}
