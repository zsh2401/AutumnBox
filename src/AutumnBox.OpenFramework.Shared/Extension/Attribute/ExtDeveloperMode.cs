namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块是否是开发人员模式
    /// </summary>
    public class ExtDeveloperMode : ExtensionInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ExtDeveloperMode(bool value = false) :
            base(ExtensionMetadataKeys.IS_DEVELOPING, value)
        { }
    }
}
