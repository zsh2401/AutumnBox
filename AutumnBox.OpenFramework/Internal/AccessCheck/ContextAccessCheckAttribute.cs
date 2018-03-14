using AutumnBox.OpenFramework.Open.V1;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal.AccessCheck
{
    internal class AccessCheckAttributeCheckLog : Context
    {

    }
    [Serializable]
    internal class ContextAccessCheckAttribute : AccessCheckAttribute
    {
        private readonly ContextPermissionLevel level;
        public ContextAccessCheckAttribute(ContextPermissionLevel level = ContextPermissionLevel.Mid)
        {
            this.level = level;
        }
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            bool firstParaIsRight = method.GetParameters().First().ParameterType == typeof(Context);
            if (!firstParaIsRight)
            {
                throw new ArgumentException("First arg must is Context!");
            }
            base.CompileTimeInitialize(method, aspectInfo);
        }

        public override bool AccessCheck(MethodInterceptionArgs args)
        {
            var context = (Context)args.Arguments[0];
            return context.GetPermissionLevel() >= level;
        }
    }
}
