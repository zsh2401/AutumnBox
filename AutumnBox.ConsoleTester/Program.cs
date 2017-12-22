using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
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
            //var o = new ABProcess().RunToExited(ConstData.FullAdbFileName,"help");
            var o = new CExecuter().Execute(Command.MakeForAdb("devices"));

            Console.WriteLine(o);
            var devList = new DevicesGetter().GetDevices();
            devList.ForEach((dev)=> {
                Console.WriteLine(dev.ToString()); 
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
