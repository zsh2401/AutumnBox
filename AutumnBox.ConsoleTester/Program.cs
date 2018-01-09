using AutumnBox.Basic.Device;
using System;

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
        unsafe static int Main(string[] args)
        {
            DeviceImageFinder finder = new DeviceImageFinder(mi6.Serial);
            var path = finder.PathOf(DeviceImage.Boot);
            Console.WriteLine(path);
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
