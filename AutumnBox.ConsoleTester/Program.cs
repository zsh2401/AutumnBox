using AutumnBox.Basic.Device;
using AutumnBox.Basic.Executer;
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
            State = DeviceState.Fastboot,
        };
        class EqualsTest : IEquatable<EqualsTest>
        {
            public String Name { get; set; }
            public override bool Equals(object obj)
            {
                Console.WriteLine("default equals");
                return base.Equals(obj);
            }

            public bool Equals(EqualsTest other)
            {
                Console.WriteLine("iequals");
                return this.Name == other.Name;
            }
        }
        unsafe static int Main(string[] cmdargs)
        {
            var info = new DeviceSoftwareInfoGetter(mi4.Serial).IsRootEnable();
            var a = new EqualsTest() { Name = "hehe" };
            var b = new EqualsTest() { Name = "hehe" };
            a.Equals((object)b);
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
