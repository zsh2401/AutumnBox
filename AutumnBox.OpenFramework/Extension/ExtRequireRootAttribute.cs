/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 4:10:56 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 明确标记标明该拓展需要设备ROOT权限
    /// </summary>
    public class ExtRequireRootAttribute : BeforeCreatingAspect
    {
        private readonly bool reqRoot;

        /// <summary>
        /// 构造
        /// </summary>
        public ExtRequireRootAttribute(bool reqRoot) {
            this.reqRoot = reqRoot;
        }
        /// <summary>
        /// 构造
        /// </summary>
        public ExtRequireRootAttribute() : this(true) { }

        private static bool DeviceHaveRoot(IDevice device)
        {
            return device.HaveSU();
        }

        public override void Do(BeforeCreatingAspectArgs args, ref bool canContinue)
        {
            if (!reqRoot || args.TargetDevice == null)
            {
                return;
            }
            if (!DeviceHaveRoot(args.TargetDevice))
            {
                args.Context.App.RunOnUIThread(() =>
                {
                    args.Context.Ux.Warn("OpenFxNoRoot");
                });
                canContinue = true;
            }
        }
    }
}
