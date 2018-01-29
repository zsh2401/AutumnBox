using AutumnBox.Basic.ACP;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.Basic.MultipleDevices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Serial = new DeviceSerial("af0fe186"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi6net = new DeviceBasicInfo()
        {
            Serial = new DeviceSerial("192.168.0.12:5555"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Serial = new DeviceSerial("9dd1b490"),
            State = DeviceState.Poweron,
        };
        unsafe static int Main(string[] cmdargs)
        {
            var response = AcpCommunicator.GetAcpCommunicator(mi4.Serial).SendCommand(ACPConstants.CMD_GETICON, "top.atmb.autumnbox");
            File.WriteAllBytes("x.png",response.Data);
            //Console.WriteLine($"{hashCode} {hashCode2}");
            Console.ReadKey();
            return 0;
        }
        static void ParamsTest(params string[] args) {
            Console.WriteLine(args.Length);
        }
        public static void WriteWithColor(Action a, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            a();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
