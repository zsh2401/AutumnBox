/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 0:15:54 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutumnBox.Basic.Extension
{
    public static class FileInfoExtension
    {
        private static readonly CommandExecuter executor = new CommandExecuter();
        public static void PushTo(this FileInfo fileInfo, DeviceBasicInfo device, string path)
        {
            ThrowIf.IsNullArg(fileInfo);
            ThrowIf.IsNullArg(path);
            var command = Command.MakeForAdb(
                $"push \"{fileInfo.FullName}\" \"{path}\""
                );
            var output = executor.Execute(command);
            if (output.GetExitCode() != 0)
            {
                throw new AdbCommandFailedException(output);
            }
        }
    }
}
