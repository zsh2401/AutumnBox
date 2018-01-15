using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.Basic.Executer;
using System;
namespace AutumnBox.ConsoleTester
{
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
            var packages = PackageHelper.GetPackages(mi4.Serial);
            Console.WriteLine(packages.Count);
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
