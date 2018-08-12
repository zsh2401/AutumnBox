/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 5:26:24 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    public class MinAndroidVersionAttribute : ExtBeforeCreateAspectAttribute
    {
        public MinAndroidVersionAttribute(int major, int minor, int build) :
            base(new Version(major, minor, build))
        {
        }

        private static bool VersionCheck(DeviceBasicInfo device, Version minVersion)
        {
            var propGetter = new DeviceBuildPropGetter(device);
            return (propGetter.GetAndroidVersion() >= minVersion);
        }

        public override void Before(ExtBeforeCreateArgs args)
        {
            if (!VersionCheck(args.TargetDevice, Value as Version))
            {
                args.Context.App.RunOnUIThread(() =>
                {
                    args.Context.App.ShowMessageBox("Warning", "您的安卓设备版本过低....");
                });
                args.Prevent = true;
            }
        }
    }
}
