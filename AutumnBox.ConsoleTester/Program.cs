using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Support;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Serial = new Serial("af0fe186"),
            Status = DeviceStatus.Poweron,
        };
        public readonly static DeviceBasicInfo mi6net = new DeviceBasicInfo()
        {
            Serial = new Serial("192.168.0.12:5555"),
            Status = DeviceStatus.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Serial = new Serial("9dd1b490"),
            Status = DeviceStatus.Poweron,
        };
        unsafe static void Main(string[] args)
        {
            //Process.Start("steam://open/console");
            string[] lines  = File.ReadAllLines(@"C:\Windows\System32\drivers\etc\hosts");
            string fullText = File.ReadAllText(@"C:\Windows\System32\drivers\etc\hosts");
            StringBuilder sb = new StringBuilder(fullText);
            sb.Append("23.52.74.146 steamcommunity.com");
            File.WriteAllText(@"C:\Windows\System32\drivers\etc\hosts",sb.ToString());
            //var devices = new DevicesGetter().GetDevices();
            //devices.ForEach((i) =>
            //{
            //    Console.WriteLine(i.Serial.ToString());
            //});
            //var buildInfo = DeviceInfoHelper.GetBuildInfoWithSu(new Serial("192.168.0.12:5555"));
            //foreach(var en in buildInfo)
            //{
            //    Console.WriteLine(en.Key,en.Value);
            //}
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
