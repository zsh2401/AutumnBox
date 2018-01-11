using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.Basic.Flows.MiFlash;
using System;
using System.IO;
using System.Linq;

namespace AutumnBox.ConsoleTester
{
    class Fuck
    {
        public string fucl;
        public int fs;
        public int sadasas;
    }
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Serial = new Serial("af0fe186"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi6net = new DeviceBasicInfo()
        {
            Serial = new Serial("192.168.0.12:5555"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Serial = new Serial("9dd1b490"),
            State = DeviceState.Fastboot,
        };
        unsafe static int Main(string[] cmdargs)
        {
            var args = new MiFlasherArgs()
            {
                DevBasicInfo = mi4,
                BatFileName = @"D:\☆下载暂存\cancro_images_7.11.2_20171102.0000.00_6.0_cn\flash_all.bat"
            };
            MiFlasher flasher = new MiFlasher();
            flasher.Init(args);
            flasher.OutputReceived += (s, e) =>
            {
                Console.WriteLine("std o/e->" + e.Text);
            };

            flasher.Finished += (s, e) =>
            {
                Console.WriteLine(e.Result.ResultType);
            };
            flasher.RunAsync();
            Console.ReadKey();
            flasher.ForceStop();
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
