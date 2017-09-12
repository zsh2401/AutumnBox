#define NEW
using AutumnBox.Basic.Util;
using System;
using System.Management;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static AutumnBox.Basic.AdbEnc.Events;

namespace AutumnBox.Basic.AdbEnc
{
    /// <summary>
    /// 封装Cmd命令行
    /// </summary>
    internal class Cmd : BaseObject,IDisposable
    {
        public Process CmdProcess { get; private set; }
        public event ExecuteStartHandler ExecuteStarted;
        public event DataReceivedEventHandler OutputDataReceived
        {
            add { cmdProcess.OutputDataReceived += value; }
            remove { cmdProcess.OutputDataReceived -= value; }
        }
        public event DataReceivedEventHandler ErrorDataReceived
        {
            add { cmdProcess.ErrorDataReceived += value; }
            remove { cmdProcess.ErrorDataReceived -= value; }
        }
        private OutputData _tmpOut = new OutputData();
        internal Process cmdProcess = new Process();
        public Cmd()
        {
            //初始化Cmd
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
            cmdProcess.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
            cmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入    
            cmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            cmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
            _tmpOut.output = new List<string>();
            this.OutputDataReceived += (s, e) =>
            {
                LogD("Out: " + e.Data);
                _tmpOut.output.Add(e.Data);
                _tmpOut.AllOut += e.Data;
            };
            this.ErrorDataReceived += (s, e) =>
            {
                LogD("Error: " + e.Data + "\n");
                _tmpOut.error += e.Data;
                _tmpOut.AllOut += e.Data;
            };
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command">完整命令</param>
        /// <returns>输出数据</returns>
        public virtual OutputData Execute(string command)
        {
#if DEBUG
            Logger.D(TAG, $"Execute Command {command}");
#endif
            _tmpOut.Clear();
#if NEW
            cmdProcess.StartInfo.Arguments = "/c " + command;
            cmdProcess.Start();
            ExecuteStarted?.Invoke(this, new ExecuteStartEventArgs (){ PID=cmdProcess.Id});
            try
            {
                cmdProcess.BeginOutputReadLine();
                cmdProcess.BeginErrorReadLine();
            }
            catch (Exception e) { LogE("异步读取失败", e); }
            try
            {
                cmdProcess.WaitForExit();
                cmdProcess.CancelOutputRead();
                cmdProcess.CancelErrorRead();
            }
            catch (Exception e) { LogE("等待退出或关闭流失败", e); }
            try { cmdProcess.Close(); } catch (Exception e) { LogE("关闭失败", e); }
            LogD("Finish Execute");
            return _tmpOut;
#else
            cmdProcess.StartInfo.Arguments = "/c " + command;
            cmdProcess.Start();
            //获取执行命令时输出的内容
            StreamReader x = cmdProcess.StandardOutput;
            string str = x.ReadToEnd();
            //将原始输出分行并且存储到一个string列表
            string[] lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //实例化一个OutputData,用于存储执行命令产生的输出
            var o = new OutputData();
            try
            {
                o.output = lines.ToList();//按行存储的正常输出List<string>
                o.error = cmdProcess.StandardError.ReadToEnd();//错误输出
            }
            catch (Exception e)
            {
                Log.d(TAG, e.Message);
            }
            try { cmdProcess.WaitForExit(); } catch (Exception e) { Log.d(TAG, e.Message); }
            try { cmdProcess.Close(); } catch (Exception e) { Log.d(TAG, e.Message); }
            return o;
#endif
        }

        public void Dispose()
        {
            cmdProcess.Dispose();
        }
    }
}
