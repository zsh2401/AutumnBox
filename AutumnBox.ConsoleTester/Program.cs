using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Support;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        //public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        //{
        //    Id = "af0fe186",
        //    Status = DeviceStatus.Poweron,
        //};
        //public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        //{
        //    Id = "9dd1b490",
        //    Status = DeviceStatus.Poweron,
        //};
        unsafe static void Main(string[] args)
        {
            IPAddress.Parse("192.168.0.12");
            var devices = new DevicesGetter().GetDevices();
            devices.ForEach((i) =>
            {
                Console.WriteLine(i.Serial.ToString());
            });
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
