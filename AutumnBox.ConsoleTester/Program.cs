using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.FlowFramework.Container;
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
            Basic.Flows.AirForzenActivator fl = new Basic.Flows.AirForzenActivator();
            fl.Init(new FlowArgs() { DevBasicInfo = mi4 });
            fl.Finished += (s, e) =>
            {
                Console.WriteLine(e.Result.ResultType);
                Console.WriteLine(e.Result.ErrorType);
            };
            fl.OutputReceived += (s, e) =>
            {
                Console.WriteLine(e.Text);
            };
            FunctionFlowBase.AnyFinished += (s, e) =>
            {
                Console.WriteLine(s.GetType().Name + " ->finished...");
                Console.WriteLine(e.Result.OutputData.ToString());
            };
            fl.RunAsync();
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
