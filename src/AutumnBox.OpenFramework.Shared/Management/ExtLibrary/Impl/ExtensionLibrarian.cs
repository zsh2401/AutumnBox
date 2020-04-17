/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 3:34:12 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Management.ExtLibrary.Impl
{
    /// <summary>
    /// 可供拓展的库管理器
    /// </summary>
    public abstract class ExtensionLibrarian : AssemblyLibrarian
    {
        /// <summary>
        /// 初始化构造器
        /// </summary>
        public ExtensionLibrarian()
        {
            ManagedAssembly = this.GetType().Assembly;
        }
    }
}
