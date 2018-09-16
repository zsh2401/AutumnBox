/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 18:06:00 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System.Net;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.MultipleDevices
{
    internal static class DeviceObjectFacotry
    {
        private const string DEVICES_PATTERN = @"(?i)^(?<sn>[^\u0020|^\t]+)[^\w]+(?<state>\w+)\u0020?.+?$";
        private static readonly Regex _deviceRegex = new Regex(DEVICES_PATTERN);

        private const string DEVICES_L_PATTERN = @"^(?<sn>[^\u0020|^\t]+)[^\w]+(?<state>\w+).+:(?<product>\w+).+:(?<model>\w+).+:(?<device>\w+).+:(?<transport_id>\w+)$";
        private static readonly Regex _deviceLRegex = new Regex(DEVICES_L_PATTERN);

        public static bool TryParse(string serialNumber, string state, out IDevice device)
        {
            DeviceBase dev = null;
            if (TryParseSerialAsIPEnd(serialNumber, out IPEndPoint end))
            {
                device = new NetDevice()
                {
                    IPEndPoint = end,
                };
            }
            else
            {
                device = new UsbDevice();
            }
            dev.SerialNumber = serialNumber;
            dev.State = state.ToDeviceState();
            return device != null;
        }

        public static bool AdbTryParse(string input, out IDevice device)
        {
            if (AdbLParse(input, out device))
            {
                return true;
            }
            if (SimpleParse(input, out device))
            {
                return true;
            }
            return false;
        }

        private static bool TryParseSerialAsIPEnd(string serialNumber, out IPEndPoint iPEndPoint)
        {
            try
            {
                var splited = serialNumber.Split(':');
                var ip = IPAddress.Parse(splited[0]);
                var port = ushort.Parse(splited[1]);
                iPEndPoint = new IPEndPoint(ip, port);
                return true;
            }
            catch
            {
                iPEndPoint = null;
                return false;
            }
        }
        private static bool AdbLParse(string input, out IDevice device)
        {
            var match = _deviceLRegex.Match(input);
            if (!match.Success)
            {
                device = null;
                return false;
            }
            DeviceBase dev = null;
            if (TryParseSerialAsIPEnd(match.Result("${sn}"), out IPEndPoint endPoint))
            {
                dev = new NetDevice()
                {
                    IPEndPoint = endPoint
                };
            }
            else
            {
                dev = new UsbDevice();
            }
            dev.SerialNumber = match.Result("${sn}");
            dev.State = match.Result("${state}").ToDeviceState();
            dev.Model = match.Result("${model}");
            dev.Product = match.Result("${product}");
            dev.TransportId = match.Result("${transport_id}");
            device = dev;
            return true;
        }
        private static bool SimpleParse(string input, out IDevice device)
        {
            var match = _deviceRegex.Match(input);
            if (!match.Success)
            {
                device = null;
                return false;
            }
            DeviceBase dev = null;
            if (TryParseSerialAsIPEnd(match.Result("${sn}"), out IPEndPoint endPoint))
            {
                dev = new NetDevice()
                {
                    IPEndPoint = endPoint
                };
            }
            else
            {
                dev = new UsbDevice();
            }
            dev.SerialNumber = match.Result("${sn}");
            dev.State = match.Result("${state}").ToDeviceState();
            device = dev;
            return true;
        }
        public static bool FastbootTryParse(string input, out IDevice device)
        {
            if (SimpleParse(input, out device))
            {
                return true;
            }
            return false;
        }
    }
}
