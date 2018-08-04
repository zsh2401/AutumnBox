using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 接近操作系统的API
    /// </summary>
    public interface IOSApi
    {
        /// <summary>
        /// 当前是否以管理员模式运行
        /// </summary>
        bool IsRunAsAdmin { get; }
        /// <summary>
        /// 当前系统是否为WIN10
        /// </summary>
        bool IsWindows10 { get; }
        /// <summary>
        /// 安装驱动
        /// </summary>
        /// <param name="infPath">驱动的inf路径</param>
        /// <returns>执行结果</returns>
        bool InstallDriver(string infPath);
    }
}
