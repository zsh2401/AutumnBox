using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
using System;

namespace AutumnBox.ConsoleTester
{
    class Fuck {
        public string fucl;
        public int fs;
        public int sadasas;
    }
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
            DeviceBuildPropGetter getter = new DeviceBuildPropGetter(mi6.Serial);
            var dict = getter.GetFull();
            Console.WriteLine(dict["ro.build.version.release"]);
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
