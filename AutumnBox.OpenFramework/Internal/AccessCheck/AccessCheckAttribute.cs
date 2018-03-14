using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Internal.AccessCheck
{
    [Serializable]
    public abstract class AccessCheckAttribute : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            if (AccessCheck(args))
            {

            }
            else
            {
                throw new AccessDeniedException();
            }
            base.OnInvoke(args);
        }
        public abstract bool AccessCheck(MethodInterceptionArgs args);
    }
}
