/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 5:49:54 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.AppFx.Impl
{
    public class ActivityManager : DependOnDeviceObject, IActivityManager
    {
        
        public ActivityManager(IDevice device) : base(device)
        {
        }

        public void StartActivity(string pkgName, string activityClassName)
        {
            var cmd = new AdbCommandBuilder().Device(Device)
                 .Shell()
                 .Arg("am")
                 .Arg("start")
                 .Arg($"{pkgName}/.{activityClassName}").ToCommand();
           var result =  cmd.Execute();
            result.ThrowIfExitCodeNotEqualsZero();
        }
    }
}
