//#define TEST_LOCAL_API
using AutumnBox.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AutumnBox
{
    internal class Tools
    {

        

        public static void KillProcess(string processName) {
            Process[] process;//创建一个PROCESS类数组
            process = Process.GetProcesses();//获取当前任务管理器所有运行中程序
            foreach (Process proces in process)//遍历
            {
                if (proces.ProcessName == processName)
                {
                    proces.Kill();
                }
            }
        }
    }
}
