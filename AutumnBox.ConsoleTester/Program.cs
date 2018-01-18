using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Flows;
using System;
using System.Diagnostics;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Serial = new DeviceSerial("af0fe186"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi6net = new DeviceBasicInfo()
        {
            Serial = new DeviceSerial("192.168.0.12:5555"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Serial = new DeviceSerial("9dd1b490"),
            State = DeviceState.Poweron,
        };
        unsafe static int Main(string[] cmdargs)
        {
            DcimBackuper backuper = new DcimBackuper();
            var args = new DcimBackuperArgs()
            {
                DevBasicInfo = mi4,
                TargetPath = @"C:\Users\zsh24\Desktop\backup"
            };
            backuper.Init(args);
            backuper.OutputReceived += (s, e) =>
            {
                Console.WriteLine(e.Text);
            };
            backuper.Finished += (s, e) =>
            {
                Console.WriteLine(e.Result.ResultType);
            };
            backuper.RunAsync();
            Console.ReadKey();
            return 0;
        }
        public static void WriteWithColor(Action a, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            a();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
