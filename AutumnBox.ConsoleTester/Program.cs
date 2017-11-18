using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
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
            //AndroidShellTest.Run();
            //FunctionModuleProxy fmp =
            //    FunctionModuleProxy.Create<ImgFlasher>(new ImgFlasherArgs(mi4) { ImgPath = @"D:\☆下载暂存\twrp.img" });
            //fmp.OutputReceived += (s, e) => { Console.WriteLine(e.Text); };
            //fmp.Finished += (s, e) => { Console.WriteLine(e.Result.Level); };
            //fmp.AsyncRun();
            //var a = new AndroidShell(mi4);
            //a.Connect();
            //var r = a.SafetyInput("ls fasfasfasfas");
            //string x = DeviceImageHelper.FindById(DevicesHelper.GetDevices().Last(), Images.Recovery);
            //Console.WriteLine("hehe ->" + x);
            Console.WriteLine("start");
            AsyncAwaitTest.DoAsync();
            Console.WriteLine("finish");
            //AndroidShellTest.RootTest();
            //Console.WriteLine("START!?....");
            //DateTime t = DateTime.Now;
            //var path = DeviceImageHelper.FindById(Program.mi4, Images.Boot);
            //Console.WriteLine("path ->" + path ?? "not found!!!!!");
            //var timespan = DateTime.Now - t;
            //Console.WriteLine("have use" + timespan.Seconds + " seconds");
            Console.ReadKey();
        }
        //public static void WriteWColor(this static Console,Action writeAct,ConsoleColor)
        public static void WriteWithColor(Action a, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            a();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
