/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 4:15:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 声明需要依赖的APP,秋之盒将在运行前进行检查
    /// </summary>
    public sealed class ExtAppPropertyAttribute : ExtBeforeCreateAspectAttribute
    {
        /// <summary>
        /// App名称
        /// </summary>
        public string AppLabel { get; set; }
        /// <summary>
        /// 英文App名
        /// </summary>
        public string AppLabel_en { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        /// <param name="label"></param>
        public ExtAppPropertyAttribute(string value) : base(value)
        {
        }

        public override void Before(ExtBeforeCreateArgs args)
        {
            if (!InstallApplication(args.TargetDevice, Value as string))
            {
                args.Context.App.RunOnUIThread(() =>
                {
                    args.Context.App.ShowMessageBox("警告", "未安装相关应用!");
                });
                args.Prevent = true;
            }
        }
        private static bool InstallApplication(DeviceBasicInfo targetDevice, string pkgName)
        {
            return PackageManager.IsInstall(targetDevice, pkgName) == true;
        }
    }
}
