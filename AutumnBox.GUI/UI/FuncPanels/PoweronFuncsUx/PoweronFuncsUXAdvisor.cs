/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 18:27:37 (UTC +8:00)
** desc： ...
*************************************************/
using AopAlliance.Intercept;
using AutumnBox.Basic.Device;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{
    public class PoweronFuncsUXAdvisor : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            var devInfo = (DeviceBasicInfo)invocation.Arguments[0];
            foreach (var prechecker in GetPrechecker(invocation.Method))
            {
                if (!prechecker.Check(devInfo))
                {
                    return null;
                }
            }
            return invocation.Proceed();
        }

        private static FuncsPrecheckAttribute[] GetPrechecker(MethodInfo method)
        {
            var attrs = method.GetCustomAttributes(true);
            List<FuncsPrecheckAttribute> precheckers = new List<FuncsPrecheckAttribute>();
            foreach (object attr in attrs)
            {
                if (attr is FuncsPrecheckAttribute result)
                {
                    precheckers.Add(result);
                }
            }
            return precheckers.ToArray();
        }
    }
}
