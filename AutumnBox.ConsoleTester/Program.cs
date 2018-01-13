using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.Basic.Flows.MiFlash;
using AutumnBox.GUI.Cfg;
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
            IConfigOperator configOperator = new ConfigOperator();
            Console.WriteLine(configOperator.Data.IsFirstLaunch);
            configOperator.Data.IsFirstLaunch = false;
            //configOperator.SaveToDisk();
            configOperator.ReloadFromDisk();

            Console.WriteLine(configOperator.Data.IsFirstLaunch);
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
