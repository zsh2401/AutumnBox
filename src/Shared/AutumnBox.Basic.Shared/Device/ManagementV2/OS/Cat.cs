using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// Android shell中cat命令的简单封装
    /// </summary>
    public sealed class Cat
    {
        private readonly IDevice device;
        private readonly ICommandExecutor executor;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        public Cat(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="CommandErrorException">命令执行失败</exception>
        /// <returns>文件的文本内容</returns>
        public string Read(string fileName)
        {
            return executor.AdbShell(device, $"cat {fileName}")
                 .ThrowIfError()
                 .Output;
        }
    }
}
