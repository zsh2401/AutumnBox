using AutumnBox.Basic.ACP;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Flows;
using System;
using System.Diagnostics;
using System.IO;

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
            var result = new ACPCommunicator("testxxxsadasd asdas asdas").Run();
            //using (FileStream fs = new FileStream("x.txt", FileMode.OpenOrCreate,FileAccess.ReadWrite)) {
            //    fs.Write(result.Data,0,result.Data.Length);
            //    fs.Flush();
            //}
            Console.WriteLine("fCode->" + result.FirstCode);
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
