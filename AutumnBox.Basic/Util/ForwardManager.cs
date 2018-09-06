/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/27 5:29:47 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;


namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 转发信息结构体
    /// </summary>
    [Obsolete]
    public struct ForwardInfo
    {
        /// <summary>
        /// 设备
        /// </summary>
        public DeviceSerialNumber DeviceSerial { get; private set; }
        /// <summary>
        /// 本地端口
        /// </summary>
        public ushort Local { get; private set; }
        /// <summary>
        ///  本地端口所转发的设备端口
        /// </summary>
        public ushort Remote { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        /// <param name="local"></param>
        /// <param name="remote"></param>
        internal ForwardInfo(DeviceSerialNumber device, ushort local, ushort remote)
        {
            this.DeviceSerial = device;
            this.Local = local;
            this.Remote = remote;
        }
    }
    /// <summary>
    /// 端口转发管理器
    /// </summary>
    [Obsolete]
    public static class ForwardManager
    {
        //    private static CommandExecuter executer = new CommandExecuter();
        //    private static readonly Regex regex = new Regex(
        //        @"(?<serial>.+).tcp:(?<local>\d+).tcp:(?<remote>\d+)",
        //        RegexOptions.Multiline);
        //    private static readonly Command forwardRemoveCommand;
        //     static ForwardManager() {
        //        forwardRemoveCommand = Command.MakeForAdb("forward --remove-all");
        //    }
        //    /// <summary>
        //    /// 移除所有的转发
        //    /// </summary>
        //    public static void RemoveAllForward() {
        //        executer.Execute(forwardRemoveCommand);
        //    }
        //    /// <summary>
        //    /// 根据本地端口移除一个转发
        //    /// </summary>
        //    /// <param name="localPort"></param>
        //    public static void Remove(ushort localPort) {
        //        executer.Execute(Command.MakeForAdb($"--remove {localPort}"));
        //    }
        //    /// <summary>
        //    /// 获取所有的端口转发信息
        //    /// </summary>
        //    /// <param name="device"></param>
        //    /// <returns></returns>
        //    public static List<ForwardInfo> GetAllForward(DeviceSerialNumber device = null)
        //    {
        //        List<ForwardInfo> forwards = new List<ForwardInfo>();
        //        Command queryCommand = device == null ?
        //            Command.MakeForAdb(device, "forward --list") :
        //            Command.MakeForAdb("forward --list");

        //        var queryResult = executer.Execute(queryCommand);
        //        //Logger.D(queryResult.Output.ToString());
        //        if (queryResult.IsSuccessful)
        //        {
        //            var matches = regex.Matches(queryResult.ToString());
        //            foreach (Match match in matches)
        //            {
        //                forwards.Add(new ForwardInfo(
        //                    new DeviceSerialNumber(match.Result("${serial}")),
        //                    ushort.Parse(match.Result("${local}")),
        //                    ushort.Parse(match.Result("${remote}"))
        //                    ));
        //            }
        //        }
        //        return forwards;
        //    }
        //    /// <summary>
        //    /// 进行转发,不建议外部进行调用,建议使用GetForwardByRemotePort() 有效避免端口浪费
        //    /// </summary>
        //    /// <param name="device">设备</param>
        //    /// <param name="localPort">本地端口</param>
        //    /// <param name="remotePort">远程端口</param>
        //    public static void Forward(DeviceSerialNumber device, ushort localPort, ushort remotePort)
        //    {
        //        var exeReuslt = executer.Execute(Command.MakeForAdb(device, $"forward tcp:{localPort} tcp:{remotePort}"));
        //        if (!exeReuslt.IsSuccessful)
        //        {
        //            throw new ExcutionException(exeReuslt);
        //        }
        //    }
        //    /// <summary>
        //    /// 获取一个对远端设备的某个端口的本地转发端口
        //    /// </summary>
        //    /// <param name="device">设备</param>
        //    /// <param name="remotePort">需要被转发的远程`端口</param>
        //    /// <returns></returns>
        //    public static ushort GetForwardByRemotePort(DeviceSerialNumber device, ushort remotePort)
        //    {
        //        //先获取所有的转发信息
        //        var forwards = GetAllForward();
        //        //在所有的转发信息里查找是否有需要被转发的端口
        //        var forwardInfos = forwards.FindAll((info) =>
        //        {
        //            return info.Remote == remotePort;
        //        });
        //        //声明好需要返回的转发后的本地端口号
        //        ushort localPort;
        //        //如果需要被转发的端口已经被转发过了,并且是对当前设备的转发
        //        if (forwardInfos.Count > 0&&forwardInfos.First().DeviceSerial.Equals(device))
        //        {
        //            //就设定返回的本地端口为已转发的值
        //            localPort = forwardInfos.First().Local;
        //        }
        //        else//否则,设置一个新的转发
        //        {
        //            ushort idlePort = GetIdlePort();
        //            Forward(device, idlePort, remotePort);
        //            localPort = idlePort;
        //        }
        //        return localPort;
        //    }
        //    private static readonly Random ran = new Random();
        //    /// <summary>
        //    /// 获取一个空闲的端口
        //    /// </summary>
        //    /// <returns></returns>
        //    private static ushort GetIdlePort()
        //    {
        //        ushort port = (ushort)ran.Next(1024, ushort.MaxValue);
        //        if (port.IsIdlePort())
        //        {
        //            return port;
        //        }
        //        else
        //        {
        //            return GetIdlePort();
        //        }
        //    }
        //    /// <summary>
        //    /// 判断端口是否空闲
        //    /// </summary>
        //    /// <param name="portNum"></param>
        //    /// <returns></returns>
        //    private static bool IsIdlePort(this ushort portNum)
        //    {
        //        bool inUse = false;

        //        IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
        //        IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

        //        foreach (IPEndPoint endPoint in ipEndPoints)
        //        {
        //            if (endPoint.Port == portNum)
        //            {
        //                inUse = true;
        //                break;
        //            }
        //        }

        //        return !inUse;
        //    }
    }
}
