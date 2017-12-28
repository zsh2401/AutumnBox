using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Flows;
using AutumnBox.Basic.Util;
using System;
using System.Diagnostics;

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
            State = DeviceState.Poweron,
        };
        unsafe static void Main(string[] args)
        {
            //var result = new CoffeExecuter().Execute(ConstData.FullAdbFileName,"fuck");
            ////p.WaitForExit();
            //Console.WriteLine(result.Output);
            //Console.WriteLine(result.ExitCode);
            //Console.WriteLine(DeviceInfoHelper.GetDpi(mi6.Serial));
            var activator = new BreventServiceActivator();
            activator.Init(new BreventServiceActivatorArgs() { DevBasicInfo = mi6 });
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
