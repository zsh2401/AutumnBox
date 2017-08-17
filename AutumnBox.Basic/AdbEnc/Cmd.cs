using AutumnBox.Basic.DebugTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.AdbEnc
{
    /// <summary>
    /// 封装Cmd命令行
    /// </summary>
    internal class Cmd:ICommandExecuter
    {
        public const string NOT_FOUND = "NOT_FOUND";
        protected Process cmdProcess = new Process();
        private string TAG = "CMD";
        public Cmd()
        {
            //初始化Cmd
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.CreateNoWindow = true;         // 不创建新窗口    
            cmdProcess.StartInfo.UseShellExecute = false;       //不启用shell启动进程  
            cmdProcess.StartInfo.RedirectStandardInput = true;  // 重定向输入    
            cmdProcess.StartInfo.RedirectStandardOutput = true; // 重定向标准输出    
            cmdProcess.StartInfo.RedirectStandardError = true;  // 重定向错误输出  
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command">完整命令</param>
        /// <returns>输出数据</returns>
        public OutputData Execute(string command)
        {
            List<string> output = new List<string>();
            string error = "";
            Log.d(TAG,$"Execute Command {command}");
            cmdProcess.StartInfo.Arguments = "/c " + command;
            cmdProcess.Start();
            //获取执行命令时输出的内容
            StreamReader x = cmdProcess.StandardOutput;
            string str = x.ReadToEnd();
            //将原始输出分行并且存储到一个string列表
            string[] lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            OutputData o = new OutputData();
            try
            {
                o.output = lines.ToList();
                o.error = cmdProcess.StandardError.ReadToEnd();
            }catch { }

            try{cmdProcess.WaitForExit(); }catch { }
            try { cmdProcess.Close(); } catch { }
            
            return o;
        }
    }
}
