using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Basic.Util;
using AutumnBox.ConsoleTester.MethodTest;
using AutumnBox.ConsoleTester.ObjTest;
using AutumnBox.GUI.Helper;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Id = "af0fe186",
            Status = DeviceStatus.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Id = "9dd1b490",
            Status = DeviceStatus.Poweron,
        };
        unsafe static void Main(string[] args)
        {
            Basic.Flows.FileSender fs = new Basic.Flows.FileSender();
            fs.Init(new Basic.Flows.Args.FileSenderArgs { DevBasicInfo = Program.mi4, PathFrom = @"D:\☆下载暂存\密码1 腾讯steam加速.zip.tdl" });
            fs.Finished += (s, e) =>
            {
                Console.WriteLine(e.Result.FileSendErrorType);
            };
            fs.OutputReceived += (s, e) =>
            {
                Console.WriteLine(e.Text);
            };
            fs.RunAsync();
            Console.ReadKey();
        }
        public static void WriteWithColor(Action a, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            a();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
