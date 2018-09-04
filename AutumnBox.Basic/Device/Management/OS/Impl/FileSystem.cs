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
    public class FileSystem : DependOnDeviceObject, IFileSystem
    {
        public FileSystem(IDevice device) : base(device)
        {
        }

        public string Cat(string file)
        {
            var result = new AdbCommand("shell cat " + file).Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
            return result.Output.ToString();
        }

        public void Copy(string src, string target, bool isDir)
        {
            var result = isDir ?
                new AdbCommand($"cp -r {src} {target}").Execute()
                : new AdbCommand($"cp {src} {target}").Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
        }

        public void Delete(string file)
        {
            new AdbCommand($"rm -rf {file}").Execute()
                 .ThrowIfShellExitCodeNotEqualsZero();
        }

        public Output Find(string name)
        {
            var result = new AdbCommand($"find {name}").Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
            return result.Output;
        }

        public void Mkdir(string file)
        {
            var result = new AdbCommand($"mkdir {file}").Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
        }

        public void Move(string src, string target, bool isDir)
        {
            var result = isDir ?
                new AdbCommand($"mv -r {src} {target}").Execute()
                : new AdbCommand($"mv {src} {target}").Execute();
            result.ThrowIfShellExitCodeNotEqualsZero();
        }
    }
}
