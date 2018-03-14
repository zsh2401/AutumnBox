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

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    [Serializable]
    public class HideAttribute : AccessCheckAttribute
    {
        private static readonly string[] whiteList = new string[] {
            BuildInfo.AUTUMNBOX_OPENFRAMEWORK_ASSEMBLY_NAME,
            BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME,
        };
        public override bool AccessCheck(MethodInterceptionArgs args)
        {
            var callingMethod = new StackFrame(3).GetMethod();
            var decTypeOfCallingMethod = callingMethod.DeclaringType;
            var callingAssemblyName = decTypeOfCallingMethod.Assembly.GetName().Name;
            return whiteList.Contains(callingAssemblyName);
        }
    }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}
