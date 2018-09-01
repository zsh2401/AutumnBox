/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 0:39:14 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using System;

namespace AutumnBox.Basic.Util
{
    public static class ThrowIf
    {
        public static void ThrowIfExitCodeNotEqualsZero(this Tuple<Output,int> result)
        {
            if (result.Item2 != 0)
            {
                throw new AdbShellCommandFailedException(result.Item2, result.Item1.ToString());
            }
        }
        public static void ThrowIfShellExitCodeNotEqualsZero(this IProcessBasedCommandResult result)
        {
            if (result.ExitCode != 0)
            {
                throw new AdbShellCommandFailedException(result.ExitCode, result.Output.ToString());
            }
        }
        public static void ThrowIfExitCodeNotEqualsZero(this IProcessBasedCommandResult result)
        {
            if (result.ExitCode != 0)
            {
                throw new AdbCommandFailedException(result.Output);
            }
        }
        public static void ThrowIfNotAlive(this IDevice device)
        {
            if (device.IsAlive == false)
            {
                throw new DeviceNotAliveException();
            }
        }
        public static void ThrowIfNull(this object any)
        {
            if (any == null)
            {
                throw new NullReferenceException();
            }
        }
        public static void ThrowIfNullArg(this object any)
        {
            if (any == null)
            {
                throw new ArgumentNullException();
            }
        }
        public static void IsNullArg(object any)
        {
            if (any == null)
            {
                throw new ArgumentNullException();
            }
        }
        public static void IsNull(object any)
        {
            if (any == null)
            {
                throw new NullReferenceException();
            }
        }
    }
}
