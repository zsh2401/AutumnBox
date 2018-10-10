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
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class UserAgreeAttribute : ExtBeforeCreateAspectAttribute
    {
        public UserAgreeAttribute(string value) : base(value)
        {
        }

        public override void Before(ExtBeforeCreateArgs args)
        {
            string message = CoreLib.Current.Languages.Get(Value.ToString()) ?? Value.ToString();
            bool isAgree = CoreLib.Context.Ux.Agree(message);
            args.Prevent = !isAgree;
        }
    }
}
