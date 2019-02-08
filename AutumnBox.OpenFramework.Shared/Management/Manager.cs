/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 1:02:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Service;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 各种管理器
    /// </summary>

    public static class Manager
    {
        /// <summary>
        /// 内部管理器
        /// </summary>
        public static IInternalManager InternalManager
        {
            get; internal set;
        }

        /// <summary>
        /// 服务管理器
        /// </summary>
        public static IServicesManager ServicesManager
        {
            get; internal set;
        }
    }
}
