/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 0:53:59 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.OpenFramework
{
    /// <summary>
    /// 关于SDK的信息
    /// </summary>
    public static class BuildInfo
    {
        /// <summary>
        /// SDK版本,不设计为Const是为了防止编译器优化
        /// </summary>
        public static readonly int SDK_VERSION = 5;
        /// <summary>
        /// AutumnBox.GUI的程序集名称
        /// </summary>
        internal const string AUTUMNBOX_GUI_ASSEMBLY_NAME = "AutumnBox.GUI";
        /// <summary>
        /// AutumnBox.Basic的程序集名称
        /// </summary>
        internal const string AUTUMNBOX_BASIC_ASSEMBLY_NAME = "AutumnBox.Basic";
        /// <summary>
        /// AutumnBox.OpenFramework的程序集名称
        /// </summary>
        internal const string AUTUMNBOX_OPENFRAMEWORK_ASSEMBLY_NAME = "AutumnBox.OpenFramework";
    }
}
