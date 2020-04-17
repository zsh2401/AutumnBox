namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 隐藏的拓展
    /// </summary>
    public class ExtHiddenAttribute : ExtensionInfoAttribute
    {
        /// <summary>
        /// 隐藏的拓展标记
        /// </summary>
        public ExtHiddenAttribute(bool value = false) :
            base(ExtensionMetadataKeys.IS_HIDDEN, value)
        {
        }
    }
}
