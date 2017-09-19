//#define SHOW_OUTPUT
//#define SHOW_COMMAND
using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Executer
{
    public enum ExeType
    {
        Adb,
        Fastboot
    }
    public sealed partial class CommandExecuter : BaseObject, IDisposable, IDevicesGetter,IAdbCommandExecuter
    {
        //事件
        public event ProcessStartEventHandler ProcessStared;
        public event ExecuteStartHandler ExecuteStarted;
        public event DataReceivedEventHandler OutputDataReceived
        {
            add { MainProcess.OutputDataReceived += value; }
            remove { MainProcess.OutputDataReceived -= value; }
        }
        public event DataReceivedEventHandler ErrorDataReceived
        {
            add { MainProcess.ErrorDataReceived += value; }
            remove { MainProcess.ErrorDataReceived -= value; }
        }
        /// <summary>
        /// 当前的执行类型
        /// </summary>
        private ExeType NowExeType
        {
            get { return exeType; }
            set
            {
                MainProcess.StartInfo.FileName = (value == ExeType.Adb) ? ADB_PATH : FB_PATH;
                exeType = value;
            }
        }
        private ExeType exeType;

        private OutputData tempOut;
        /// <summary>
        /// 执行器的底层进程
        /// </summary>
        private Process MainProcess = new Process();
        private static readonly string ADB_PATH = Paths.ADB_TOOLS;
        private static readonly string FB_PATH = Paths.FASTBOOT_TOOLS;
        public CommandExecuter()
        {
            //初始化Cmd
            MainProcess.StartInfo.FileName = "unknow";
            MainProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
            MainProcess.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
            MainProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入    
            MainProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            MainProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
            OutputData.Get(out tempOut);
            MainProcess.OutputDataReceived += (s, e) =>
            {
#if SHOW_OUTPUT
                LogD("Out: " + e.Data);
#endif
                tempOut.LineOut.Add(e.Data);
                tempOut.Out.AppendLine(e.Data);
            };
            MainProcess.ErrorDataReceived += (s, e) =>
            {
#if SHOW_OUTPUT
                LogD("Error: " + e.Data);
#endif
                tempOut.LineError.Add(e.Data);
                tempOut.Error.AppendLine(e.Data);
            };
        }
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="devList"></param>
        public void GetDevices(out DevicesList devList)
        {
            if (Process.GetProcessesByName("adb").Length == 0) ExecuteWithoutDevice("start-server");
            devList = new DevicesList();
            //Adb devices
            List<string> l;
            ExecuteWithoutDevice("devices", out OutputData o);
            l = o.LineOut;
            for (int i = 1; i < l.Count - 2; i++)
            {
                devList.Add(
                    new DeviceSimpleInfo
                    {
                        Id = l[i].Split('\t')[0],
                        Status = DevicesHelper.StringStatusToEnumStatus(l[i].Split('\t')[1])
                    });
            }
            //Fastboot devices
            ExecuteWithoutDevice("devices", out OutputData ofb,ExeType.Fastboot);
            l = ofb.LineOut;
            for (int i = 0; i < l.Count - 1; i++)
            {
                try
                {
                    devList.Add(
                    new DeviceSimpleInfo
                    {
                        Id = l[i].Split('\t')[0],
                        Status = DevicesHelper.StringStatusToEnumStatus(l[i].Split('\t')[1])
                    });
                }
                catch { }
            }
        }
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="devList">设备信息列表</param>
        public DevicesList GetDevices() {
            GetDevices(out DevicesList devList);
            return devList;
        }
        /// <summary>
        /// 启动adb服务
        /// </summary>
        public static void Start()
        {
            new CommandExecuter().ExecuteWithoutDevice("start-server");
        }
        /// <summary>
        /// 关闭adb服务
        /// </summary>
        public static void Kill()
        {
            new CommandExecuter().ExecuteWithoutDevice("kill-server");
        }
        /// <summary>
        /// 重启adb服务
        /// </summary>
        public static void Restart()
        {
            Kill();
            Start();
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            MainProcess.Dispose();
            //Tools.KillProcessAndChildrens(MainProcess.Id);
        }
        #region Execute Command

        //public OutputData Execute(string command)
        //{
        //    return CExecute(command, ExeType.Adb);
        //}
        //public OutputData Execute(string id, string command)
        //{
        //    OutputData o;
        //    Execute(new CommandData { DeviceID = id, Command = command, ExeType = ExeType.Adb }, out o);
        //    return o;
        //}
        //public OutputData FBExecute(string command)
        //{
        //    return CExecute(command, ExeType.Fastboot);
        //}
        //public OutputData FBExecute(string id, string command)
        //{
        //    return CExecute($"-s {id} {command}", ExeType.Fastboot);
        //}
        #endregion

    }
}
