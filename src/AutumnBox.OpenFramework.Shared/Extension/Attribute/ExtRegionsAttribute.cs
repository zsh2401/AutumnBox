namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 模块可用的区域
    /// </summary>
    public class ExtRegionsAttribute : ExtensionInfoAttribute
    {
        /// <summary>
        /// KEY
        /// </summary>
        public override string Key => ExtensionMetadataKeys.REGIONS;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="regions"></param>
        public ExtRegionsAttribute(params string[] regions) : base(regions)
        {
        }
    }
}
