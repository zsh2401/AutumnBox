/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 1:02:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Management.Impl;
using System.Reflection;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 各种管理器
    /// </summary>
    public static class Manager
    {

        private static IInternalManager _internalManager;
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
                PermissionCheck(Assembly.GetCallingAssembly());
                if (_internalManager == null)
                {
                    _internalManager = new InternalManagerImpl();
                }
                return _internalManager;
            }
        }
        private static IRunningManager _runningManager;
        /// <summary>
        /// 运行管理器
        /// </summary>
        public static IRunningManager RunningManager
        {
            get
            {
                if (_runningManager == null)
                {
                    _runningManager = new RunningManagerImpl();
                }
                return _runningManager;
            }
        }
        private static bool inited = false;
        /// <summary>
        /// 初始化框架
        /// </summary>
#if !SDK
        public
#else
        internal
#endif
        static void InitFramework(IAutumnBox_GUI autumnbox)
        {
            if (!inited)
            {
                AutumnBoxGuiApi.Main = autumnbox;
                InternalManager.Reload();
                inited = true;
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
    }
}
