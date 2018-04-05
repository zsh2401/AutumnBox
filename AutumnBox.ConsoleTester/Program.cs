using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using System;
using System.Diagnostics;
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
        class ExtensionInfo
        {

        }
        unsafe static int Main(string[] cmdargs)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\zsh24\Desktop\TEST.xml");
            var root = doc.SelectSingleNode("ExtensionInfo");
            var minSdk = ((XmlElement)root.SelectSingleNode("MinSdk")).GetAttribute("Value");
            Console.WriteLine(minSdk);
            return 0;
        }
    }
}
