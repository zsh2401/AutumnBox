/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/6 2:23:33 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 如果添加此标记,则秋之盒会保证该模块以管理员权限运行
    /// </summary>
    public class ExtRunAsAdminAttribute : BeforeCreatingAspect
    {
        private readonly bool reqAdmin;
        /// <summary>
        /// 设定值
        /// </summary>
        /// <param name="reqAdmin"></param>
        public ExtRunAsAdminAttribute(bool reqAdmin = true)
        {
            this.reqAdmin = reqAdmin;
        }
        /// <summary>
        /// 实现
        /// </summary>
        /// <param name="args"></param>
        /// <param name="canContinue"></param>
        public override void Do(BeforeCreatingAspectArgs args, ref bool canContinue)
        {
            if (reqAdmin && !args.Context.App.IsRunAsAdmin)
            {
                args.Context.App.RunOnUIThread(() =>
                {
                    var choice = args.Context.Ux
                    .DoChoice("OpenFxNeedAdminPermission");
                    if (choice == Open.ChoiceResult.Accept)
                    {
                        args.Context.App.RestartAppAsAdmin();
                    }
                });
            }
        }
    }
}
