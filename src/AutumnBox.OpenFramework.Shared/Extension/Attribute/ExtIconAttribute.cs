#nullable enable
/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 23:24:20 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块的图标
    /// </summary>
    public class ExtIconAttribute : ExtensionInfoAttribute
    {
        /// <summary>
        /// 指定参数的类型
        /// </summary>
        public enum IconSourceType
        {
            /// <summary>
            /// 嵌入资源路径
            /// </summary>
            EmbeddedResource,
            /// <summary>
            /// Base64字符串
            /// </summary>
            Base64Image
        }
        /// <summary>
        /// 指定源的类型
        /// </summary>
        public IconSourceType SourceType { get; set; } = IconSourceType.EmbeddedResource;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override object? Value
        {
            get
            {
                return SourceType switch
                {
                    IconSourceType.Base64Image => $"__atmb_ext_base64_icon{base.Value}",
                    _ => base.Value,
                };
            }
        }

        /// <summary>
        /// 构造拓展模块图标标记器
        /// </summary>
        /// <param name="iconSource">
        /// 一个base64字符串或内部嵌入资源的图标地址
        /// 如内嵌资源文件:  /Resources/Icons/icon.png
        /// 则应传入值: Resources.Icons.icon.png
        /// </param>
        public ExtIconAttribute(string iconSource) : base(ExtensionMetadataKeys.ICON, iconSource)
        {
        }
    }
}
