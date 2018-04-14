using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Internal;
using CSScriptLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
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
            ScriptLoader.Test();
            Console.ReadKey();
            return 0;
        }
    }
    public interface IT
    {
        void Test();
    }
}
