/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 5:26:24 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 运行模块所需的最低安卓版本...
    /// </summary>
    public class ExtMinAndroidVersionAttribute : ExtBeforeCreateAspectAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        public ExtMinAndroidVersionAttribute(int major, int minor, int build) :
            base(new Version(major, minor, build))
        {
        }

        /// <summary>
        /// 版本检查
        /// </summary>
        /// <param name="device"></param>
        /// <param name="minVersion"></param>
        /// <returns></returns>
        private static bool VersionCheck(DeviceBasicInfo device, Version minVersion)
        {
            var propGetter = new DeviceBuildPropGetter(device);
            return (propGetter.GetAndroidVersion() >= minVersion);
        }
        /// <summary>
        /// 在创建前
        /// </summary>
        /// <param name="args"></param>
        public override void Before(ExtBeforeCreateArgs args)
        {
            if (!VersionCheck(args.TargetDevice, Value as Version))
            {
                args.Context.App.RunOnUIThread(() =>
                {
                    args.Context.Ux.ShowMessageDialog("Warning", "您的安卓设备版本过低....");
                });
                args.Prevent = true;
            }
        }
    }
}
