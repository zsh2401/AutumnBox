/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/10 20:00:27 (UTC +8:00)
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
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal class UserAgreeAttribute : BeforeCreatingAspect
    {
        private readonly string message;

        public UserAgreeAttribute(string message)
        {
            this.message = message ?? throw new ArgumentNullException(nameof(message));
        }



        public override void BeforeCreating(BeforeCreatingAspectArgs args, ref bool canContinue)
        {
            string message = CoreLib.Current.Languages.Get(this.message) ?? this.message;
            bool isAgree = CoreLib.Context.Ux.DoYN(message);
           canContinue= isAgree;
        }
    }
}
