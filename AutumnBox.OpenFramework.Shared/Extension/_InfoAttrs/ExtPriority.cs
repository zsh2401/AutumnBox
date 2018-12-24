/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 19:24:44 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块优先级
    /// </summary>
    public class ExtPriority : SingleInfoAttribute
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public const int LOW = -1;
        public const int NORMAL = 0;
        public const int HIGH = 1;
        public const int ROYAL = 2;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        /// <summary>
        /// 使用标准KEY
        /// </summary>
        public override string Key => ExtensionInformationKeys.PRIORITY;
        /// <summary>
        /// 构造拓展模块优先级信息特性
        /// </summary>
        /// <param name="value"></param>
        public ExtPriority(int value) : base(value)
        {
        }
    }
}
