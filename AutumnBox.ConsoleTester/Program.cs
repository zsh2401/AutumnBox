using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Flows;
using AutumnBox.Basic.Util;
using System;
using System.Diagnostics;
using System.Net;

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
            var opener = new NetDebuggingOpener();
            opener.Init(new NetDebuggingOpenerArgs() { DevBasicInfo = mi6 });
            var openresult = opener.Run();

            var adder = new NetDeviceAdder();
            adder.Init(new NetDeviceAdderArgs() { /*DevBasicInfo = mi6, */IPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.12"), 5555) });
            var adderresult = adder.Run();
            new DevicesGetter().GetDevices().ForEach(dev => Console.WriteLine(dev.Serial));

            //var closer = new NetDebuggingCloser();
            //closer.Init(new Basic.FlowFramework.FlowArgs() { DevBasicInfo = mi6net });
            //var closeResult = closer.Run();

            //var remover = new NetDeviceRemover();
            //remover.Init(new Basic.FlowFramework.FlowArgs() { DevBasicInfo = mi6net });
            //var removerResult = remover.Run();

            Console.WriteLine(openresult.ExitCode + " " + adderresult.ExitCode + " " /*+ removerResult.ExitCode*/);
            new DevicesGetter().GetDevices().ForEach(dev => Console.WriteLine(dev.Serial));
            Console.ReadKey();
            new DevicesGetter().GetDevices().ForEach(dev => Console.WriteLine(dev.Serial));
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
