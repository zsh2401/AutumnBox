using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Executer
{
#if DEBUG
    public sealed class CommandExecuter:BaseObject,IDisposable
#else
    internal sealed class CommandExecuter : BaseObject, IDisposable
#endif
    {
        enum ExeType
        {
            Adb,
            Fastboot
        }
        //事件
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

        private OutErrorData tempOut;
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
            OutErrorData.Get(out tempOut);
            MainProcess.OutputDataReceived += (s, e) =>
            {
                LogD("Out: " + e.Data);
                tempOut.LineOut.Add(e.Data);
                tempOut.Out.AppendLine(e.Data);
            };
            MainProcess.ErrorDataReceived += (s, e) =>
            {
                LogD("Error: " + e.Data);
                tempOut.LineError.Add(e.Data);
                tempOut.Error.AppendLine(e.Data);
            };
        }
        public void GetDevices(ref DevicesList devices)
        {
            if (Process.GetProcessesByName("adb").Length == 0) Execute("start-server");
            devices.Clear();
            //Adb devices
            List<string> l;
            l = Execute("devices").LineOut;
            for (int i = 1; i < l.Count - 2; i++)
            {
                devices.Add(
                    new DeviceSimpleInfo
                    {
                        Id = l[i].Split('\t')[0],
                        Status = DevicesHelper.StringStatusToEnumStatus(l[i].Split('\t')[1])
                    });
                //devices.Add(l[i].Split('\t')[0], l[i].Split('\t')[1]);
            }
            //Fastboot devices
            l = FBExecute("devices").LineOut;
            for (int i = 0; i < l.Count - 1; i++)
            {
                try
                {
                    devices.Add(
                    new DeviceSimpleInfo
                    {
                        Id = l[i].Split('\t')[0],
                        Status = DevicesHelper.StringStatusToEnumStatus(l[i].Split('\t')[1])
                    });
                }
                catch { }
            }
        }
        public static void Kill() {
            new CommandExecuter().Execute("kill-server");
        }
        public static void Restart() {
            Kill();
            new CommandExecuter().Execute("start-server");
        }
        public void Dispose()
        {
            MainProcess.Dispose();
            //Tools.KillProcessAndChildrens(MainProcess.Id);
        }
#region Execute Command
        private OutErrorData CExecute(string command, ExeType type = ExeType.Adb)
        {
            tempOut.Clear();
            if (type == ExeType.Adb)
            {
                this.MainProcess.StartInfo.FileName = ADB_PATH;
            }
            else
            {
                this.MainProcess.StartInfo.FileName = FB_PATH;
            }
            LogD($"Execute Command {command}");
            MainProcess.StartInfo.Arguments = command;
            MainProcess.Start();
            try
            {
                MainProcess.BeginOutputReadLine();
                MainProcess.BeginErrorReadLine();
            }
            catch (Exception e) { LogE("Begin Out failed", e); }
            ExecuteStarted?.Invoke(this, new ExecuteStartEventArgs() { PID = MainProcess.Id });
            try
            {
                MainProcess.WaitForExit();
                MainProcess.CancelOutputRead();
                MainProcess.CancelErrorRead();
                MainProcess.Close();
            }
            catch (Exception e) { LogE("等待退出或关闭流失败", e); }

            return tempOut;
        }
        public OutErrorData Execute(string command)
        {
            return CExecute(command, ExeType.Adb);
        }
        public OutErrorData Execute(string id, string command)
        {
            return CExecute($"-s {id} {command}", ExeType.Adb);
        }
        public OutErrorData FBExecute(string command)
        {
            return CExecute(command, ExeType.Fastboot);
        }
        public OutErrorData FBExecute(string id, string command)
        {
            return CExecute($"-s {id} {command}", ExeType.Fastboot);
        }
#endregion

    }
}
