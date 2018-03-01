/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/30 15:03:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ActivityManager;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ACP
{
    internal static class AndroidClientController
    {
        private const string PACKAGE_NAME = "top.atmb.autumnbox";
        private const string MAIN_ACTIVITY_NAME = "activities.MainActivity";
        private const string SERVICE_NAME = "service.ACPService";
        private const string BROADCAST_START_ACP_SERVICE =
            "top.atmb.autumnbox.COMMAND_START_ACP_SERVICE";
        private const string BROADCAST_STOP_ACP_SERVICE =
            "top.atmb.autumnbox.COMMAND_STOP_ACP_SERVICE";
        public static bool IsInstallAutumnBoxApp(DeviceSerial serial) {
            return PackageManager.IsInstall(serial, PACKAGE_NAME) == true;
        }
        public static AdvanceOutput StartMainActivity(DeviceSerial device)
        {
            return Activity.Start(device, PACKAGE_NAME, MAIN_ACTIVITY_NAME);
        }
        public static AdvanceOutput AwakeAcpService(DeviceSerial device) {
            CheckInstallApp(device);
            StartMainActivity(device);
            return Service.StartService(device, PACKAGE_NAME,SERVICE_NAME);
        }
        public static AdvanceOutput StopAcpService(DeviceSerial device) {
            return Broadcast.Send(device,BROADCAST_STOP_ACP_SERVICE);
        }
        public static bool AcpServiceIsRunning(DeviceSerial device) {
            CheckInstallApp(device);
            var result =  AcpCommunicator.GetAcpCommunicator(device).SendCommand(Acp.CMD_TEST);
            return result.IsSuccessful;
        }
        private static void CheckInstallApp(DeviceSerial device) {
            if (!IsInstallAutumnBoxApp(device)) { throw new AndroidAppIsNotInstallException(); }
        }
    }
}
