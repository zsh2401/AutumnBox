using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using System;
using System.Diagnostics;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Serial = new DeviceSerialNumber("af0fe186"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi6net = new DeviceBasicInfo()
        {
            Serial = new DeviceSerialNumber("192.168.0.12:5555"),
            State = DeviceState.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Serial = new DeviceSerialNumber("9dd1b490"),
            State = DeviceState.Poweron,
        };
        unsafe static int Main(string[] cmdargs)
        {
            GreenifyAggressiveDozeActivator activator = new GreenifyAggressiveDozeActivator();
            activator.Init(new Basic.FlowFramework.FlowArgs()
            {
                DevBasicInfo = mi6
            });
            Console.ReadKey();
            return 0;
        }
    }
}
