/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 1:02:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Management.Impl;
using AutumnBox.OpenFramework.Service;
using System;
using System.Reflection;

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
#if !SDK
        public
#else
         internal
#endif
        static IInternalManager InternalManager
        {
            get
            {
                return (IInternalManager)ServicesManager.GetServiceByName(ServicesManager as Context, InternalManagerImpl.SERVICE_NAME);
            }
        }
        private static void PermissionCheck(Assembly assembly)
        {
            var caller = assembly.GetName().Name;
            if (caller != BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME &&
                caller != BuildInfo.AUTUMNBOX_OPENFRAMEWORK_ASSEMBLY_NAME
                )
            {
                throw new AccessDeniedException();
            }
        }

        /// <summary>
        /// 服务管理器
        /// </summary>
        public static IServicesManager ServicesManager
        {
            get
            {
                //PermissionCheck(Assembly.GetCallingAssembly());
                if (_servicesManager == null)
                {
                    _servicesManager = new ServicesManagerImpl();
                }
                return _servicesManager;
            }
        }
        private static IServicesManager _servicesManager;
    }
}
