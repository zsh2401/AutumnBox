using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.ConsoleTester.MethodTest;
using AutumnBox.ConsoleTester.ObjTest;
using AutumnBox.GUI.Helper;
using System;
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
            //d.GetDevices().ForEach((s) =>
            //{
            //    Console.WriteLine(s.Id + "   " + s.Status);
            //});
            //OutputData o = new OutputData();
            //o.Clear();
            //o.OutAdd("");
            //o.OutAdd("List of devices attached");
            //o.OutAdd("9dd1b490        device");
            //o.OutAdd("9dd1b490        device");
            //o.OutAdd("");
            //o.OutAdd("");
            //var reg = new Regex(@"(?i)^(?<id>[\w\d]+)[\t\u0020]{8}(?<status>\w+)[^?!.]$", RegexOptions.Multiline);
            //var matches = reg.Matches(o.ToString());
            //foreach (Match match in matches)
            //{
            //    Console.WriteLine(match.Result("${id}--${status}"));
            //}
            //var ap = new ABProcess();
            //OutputData no = new OutputData();
            //no.OutSender = ap;
            //var o = ap.RunToExited("cmd.exe","/c ping www.baidu.com");
            //Console.Write(no.ToString());
            //var d = new DevicesGetter();
            //d.GetDevices().ForEach((s) =>
            //{
            //    Console.WriteLine(s.Id + "   " + s.Status);
            //});
            Console.WriteLine(DeviceInfoHelper.GetStorageTotal(mi6));
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
