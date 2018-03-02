/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/26 19:36:23 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AutumnBox.OpenFramework.Internal
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class CallerChecker
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public static bool CallerCheck(Assembly callingAssembly)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            return callingAssembly.GetName().Name == BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME;
        }
    }
}
