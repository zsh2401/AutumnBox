/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/14 13:24:18 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
namespace AutumnBox.Support.Aop
{
    [Serializable]
    public class AccessCheckAttribute : MethodInterceptionAspect
    {
        private List<string> assemblies = new List<string>();
        public AccessCheckAttribute(params string[] assemblies)
        {
            this.assemblies.AddRange(assemblies);
            if (assemblies.Length < 0)
            {
                this.assemblies.Add("AutumnBox.GUI");
                this.assemblies.Add("AutumnBox.Basic");
                this.assemblies.Add("AutumnBox.OpenFramework");
            }
        }
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var callerName = Assembly.GetCallingAssembly().GetName().Name;
            if (assemblies.Contains(callerName))
                base.OnInvoke(args);
            else {
                throw new AccessDeniedException();
            }
        }
    }
}
