/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:12:14 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Util;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    partial class DeviceExtension
    {
        ///// <summary>
        ///// 在fastboot状态时获取设备的某些变量值
        ///// </summary>
        ///// <param name="device"></param>
        ///// <param name="key"></param>
        ///// <exception cref="CommandErrorException">命令执行失败</exception>
        ///// <exception cref="InvalidOperationException">设备状态有误</exception>
        ///// <returns></returns>
        //public static string FastbootGetVar(this IDevice device, string key, int? maxTimeout = 1000)
        //{
        //    //device.RefreshState();
        //    if (device.State != DeviceState.Fastboot) throw new InvalidOperationException("Device state not right.");
        //    CommandResult? result = null;
        //    if (maxTimeout != null)
        //    {
        //        using var executor = new HestExecutor();
        //        Task.Run(() =>
        //        {
        //            result = executor.Fastboot(device, $"getvar {key}");
        //        });
        //        Thread.Sleep((int)maxTimeout);
        //        if (result == null)
        //        {
        //            executor.Dispose();
        //            throw new CommandErrorException("Get Var Operation'used time reached timeout limit or other error occurs.");
        //        }
        //    }
        //    else
        //    {
        //        result = device.Fastboot($"getvar {key}");
        //    }
        //    //TODO
        //}

        /// <summary>
        /// 在fastboot状态时获取设备的某些变量值 (fastboot getvar指令的封装)
        /// </summary>
        /// <param name="device"></param>
        /// <param name="key"></param>
        /// <exception cref="CommandErrorException">命令执行失败</exception>
        /// <exception cref="InvalidOperationException">设备状态有误</exception>
        /// <exception cref="KeyNotFoundException">未找到对应键的值</exception>
        /// <returns></returns>
        public static string GetVar(this IDevice device, string key)
        {
            if (device.State != DeviceState.Fastboot)
            {
                throw new InvalidOperationException("Device's state not correct: should be bootloader mode");
            }
            string command = $"getvar {key}";
            using var hestExecutor = new HestExecutor(BasicBooter.CommandProcedureManager);

            /*
             * 由于某些原因,部分设备上的fastboot getvar指令会有玄学问题
             *因此此处实现了一个重试机制,尽可能获取到正确的值
             */
            const int MAX_TIMEOUT = 100;
            const int MAX_TIMES = 10;
            const int INTERVAL = 20;
            CommandResult? result = null;
            for (int crtTime = 1; crtTime <= MAX_TIMES; crtTime++)
            {
                Task.Run(() =>
                {
                    result = hestExecutor.Fastboot(device, command);
                });

                Thread.Sleep(MAX_TIMEOUT);

                if (result != null && !result.Output.Contains("GetVar Variable Not found"))
                {
                    break;
                }
                else
                {
                    hestExecutor.CancelCurrent();
                    Thread.Sleep(INTERVAL);
                }
            }

            //达到尝试上线或已经获取到正确结果

            if (result == null)
            {
                throw new CommandErrorException("Operation: 'fastboot getvar' reached limit of times of retry.");
            }
            else if (result.Output.Contains("GetVar Variable Not found", true))
            {
                throw new KeyNotFoundException();
            }
            else if (result.ExitCode != 0)
            {
                throw new CommandErrorException(result.Output, result.ExitCode);
            }
            else
            {
                var match = Regex.Match(result.Output, key + @":\s(?<value>[\w|\d]+)");
                if (match.Success)
                {
                    return match.Result("${value}");
                }
                else
                {
                    throw new CommandErrorException("Fail to parse output: " + result.Output, result.ExitCode);
                }
            }
        }

        /// <summary>
        /// 获取AB槽位信息
        /// </summary>
        /// <returns>
        /// A: true
        /// B: false
        /// null:  Not support A/B Slot or failed to get.
        /// </returns>
        public static bool? GetSlot(this IDevice device)
        {
            try
            {
                return device.GetVar("current-slot") == "a";
            }
            catch (Exception e)
            {
                e.WriteToLog();
                return null;
            }
        }

        /// <summary>
        /// 在fastboot状态时获取设备的某些变量值
        /// </summary>
        /// <param name="device"></param>
        /// <param name="key"></param>
        /// <exception cref="CommandErrorException">命令执行失败</exception>
        /// <exception cref="InvalidOperationException">设备状态有误</exception>
        /// <exception cref="KeyNotFoundException">未找到对应键的值</exception>
        /// <returns></returns>
        public static Task<string> GetVarAsync(this IDevice device, string key)
        {
            return Task.Run(() =>
            {
                return GetVar(device, key);
            });
        }

        /// <summary>
        /// 获取build.prop中的值
        /// </summary>
        /// <param name="device"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetProp(this IDevice device, string key)
        {
            var result = device.Shell($"getprop {key}").ThrowIfExitCodeNotEqualsZero();
            return result.Output.ToString().Trim();
        }

        /// <summary>
        /// Pull文件
        /// </summary>
        /// <param name="device"></param>
        /// <param name="fileOnDevice"></param>
        /// <param name="savePath"></param>
        public static void Pull(this IDevice device, string fileOnDevice, string savePath)
        {
            device.Adb($"pull {fileOnDevice} {savePath}")
                .ThrowIfExitCodeNotEqualsZero();
        }

        static readonly Regex ipRegex = new Regex(@"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.Compiled);
        /// <summary>
        /// 获取该设备在局域网中的IP
        /// </summary>
        /// <param name="device"></param>
        /// <exception cref="System.Exception">无法正确获取数据</exception>
        /// <returns></returns>
        public static System.Net.IPAddress GetLanIP(this IDevice device)
        {
            var result = device.Shell("ifconfig wlan0");
            AutumnBox.Logging.SLogger.Info(nameof(DeviceExtension), result.Output);
            var match = ipRegex.Match(result.Output.ToString());
            if (match.Success)
            {
                return System.Net.IPAddress.Parse(match.Result("${ip}"));
            }
            else
            {
                result = device.Shell("ifconfig netcfg");
                match = ipRegex.Match(result.Output.ToString());
                if (match.Success)
                {
                    return System.Net.IPAddress.Parse(match.Result("${ip}"));
                }
            }
            throw new System.Exception("can not get lan ip address");
        }
        /// <summary>
        /// 检查是否有SU权限
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool HaveSU(this IDevice device)
        {
            ///话说是哪位鬼才想到的使用不验证 su 存在直接用 su -c 检查有没有 root 的？
            ///var command = new SuCommand(device, "ls");


            /// 这玩意太长，我是不满意
            /// 顺便在控制台执行的时候遇到了一些神奇的问题
            /// 话说啥时候 ShellCommand 负责解释的是 sh 而不是 adb -s <device> shell <cmd> 呢？
            return device.Shell("echo id | su | grep uid=0 >/dev/null").ExitCode == 0;
        }
    }
}
