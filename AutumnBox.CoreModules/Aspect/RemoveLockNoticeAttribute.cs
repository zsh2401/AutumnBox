/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/10 19:52:47 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.CoreModules.Aspect
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class RemoveLockNoticeAttribute : ExtBeforeCreateAspectAttribute
    {
        public RemoveLockNoticeAttribute() : base(null)
        {
        }

        public override void Before(ExtBeforeCreateArgs args)
        {
            string message = CoreLib.Current.Languages.Get("EDpmSetterRemoveLock");
            string btnLeft = CoreLib.Current.Languages.Get("EDpmSetterRemoveLockBtnLeft");
            string btnRight = CoreLib.Current.Languages.Get("EDpmSetterRemoveLockBtnRight");
            string btnCancel = CoreLib.Current.Languages.Get("EDpmSetterRemoveLockBtnCancel");
            ChoiceResult choiceResult = CoreLib.Context.Ux
                .DoChoice(message, btnLeft, btnRight, btnCancel);
            bool isRemoved = choiceResult == ChoiceResult.Accept;
            args.Prevent = !isRemoved;
        }
    }
}
