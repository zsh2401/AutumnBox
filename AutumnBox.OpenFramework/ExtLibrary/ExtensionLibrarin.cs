/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 3:34:12 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.ExtLibrary
{
    /// <summary>
    /// 当程序集没有入口类时,将使用的默认入口类
    /// </summary>
    public abstract class ExtensionLibrarin : BaseLibrarian
    {
        /// <summary>
        /// 初始化构造器
        /// </summary>
        public ExtensionLibrarin()
        {
            Init(GetType().Assembly);
        }
    }
}
