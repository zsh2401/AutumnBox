using AutumnBox.Basic.Devices;
using AutumnBox.Support;
using System;
using System.Text;

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
            //Basic.Flows.AirForzenActivator fl = new Basic.Flows.AirForzenActivator();
            //fl.Init(new FlowArgs() { DevBasicInfo = mi4 });
            //fl.Finished += (s, e) =>
            //{
            //    Console.WriteLine(e.Result.ResultType);
            //    Console.WriteLine(e.Result.ErrorType);
            //};
            //fl.OutputReceived += (s, e) =>
            //{
            //    Console.WriteLine(e.Text);
            //};
            //FunctionFlowBase.AnyFinished += (s, e) =>
            //{
            //    Console.WriteLine(s.GetType().Name + " ->finished...");
            //    Console.WriteLine(e.Result.OutputData.ToString());
            //};
            ////fl.RunAsync();
            //Console.WriteLine(DeviceInfoHelper.IsInstalled(mi6, "me.piebridge.brevent"));
            //string fuck = "鈽嗕笅杞芥殏瀛榎";
            //new IdentifyEncoding().GetEncodingName(Convert.ToSByte(fuck));
            //Console.WriteLine(Encoding.UTF8.GetString(bytes));
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
