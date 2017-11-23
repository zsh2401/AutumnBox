using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Basic.Util;
using AutumnBox.ConsoleTester.MethodTest;
using AutumnBox.ConsoleTester.ObjTest;
using AutumnBox.GUI.Helper;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Id = "af0fe186",
            Status = DeviceStatus.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Id = "9dd1b490",
            Status = DeviceStatus.Poweron,
        };
        unsafe static void Main(string[] args)
        {
            //while (true)
            //{
            //    Console.WriteLine(ExecuterInOrder.GetMarkcode());
            //}
            Regex.Match("sadas","sadsadassdaasd").Result($"fuck");
            ExecuterInOrder.Start();
            int count = 0;
            while (true)
            {
                Console.ReadKey();
                count++;
                ExecuterInOrder.AddCommand(Command.MakeForAdb("help"), (o) => { Console.WriteLine($"command {count} finished"); });
            }
        }
        public static void WriteWithColor(Action a, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            a();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
