using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Lib
{
    public class GodPower : DependOnDeviceObject
    {
        private const string GODPOWER_APK_INNER_RES = "Res.godpower.apk";
        public ShellCommand GetRemoveUserCommand() { }
        public ShellCommand GetRemoveAccountCommnad() { }
        public ShellCommand GetSetIceBoxCommand() { }
        public ShellCommand GetSetStopAppCommand() { }
        public AdbCommand GetPushCommand()
        {

        }
        public void Push()
        {
            GetPushCommand().Execute().ThrowIfExitCodeNotEqualsZero();
        }


        public void RemoveAllUser()
        {

            GetRemoveUserCommand().Execute().ThrowIfExitCodeNotEqualsZero();
        }
        public void RemoveAllAccount()
        {
            GetRemoveAccountCommnad().Execute().ThrowIfExitCodeNotEqualsZero();
        }
        public void SetIceBox()
        {
            GetSetIceBoxCommand().Execute().ThrowIfExitCodeNotEqualsZero();
        }
        public void SetStopApp()
        {
            GetSetStopAppCommand();
        }
        public GodPower(IDevice device) : base(device)
        {
        }
    }
}
