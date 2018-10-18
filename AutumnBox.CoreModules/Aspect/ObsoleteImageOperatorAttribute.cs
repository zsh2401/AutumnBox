/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/10 19:42:59 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Aspect
{
    internal class ObsoleteImageOperator : BeforeCreatingAspect
    {


        public override void Do(BeforeCreatingAspectArgs args, ref bool canContinue)
        {
            var warnMsg = CoreLib.Current.Languages.Get("EObsoleteAndTryImageHelper");
            CoreLib.Context.Ux.Warn(warnMsg);
            canContinue = false ;
        }
    }
}
