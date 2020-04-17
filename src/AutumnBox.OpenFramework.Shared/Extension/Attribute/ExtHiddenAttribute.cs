namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 隐藏的拓展
    /// </summary>
    public class ExtHiddenAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// KEY
        /// </summary>
        public override string Key => ExtensionMetadataKeys.IS_HIDDEN;
        /// <summary>
        /// 隐藏的拓展标记
        /// </summary>
        public ExtHiddenAttribute() : base(true) { }
        /// <summary>
        /// 隐藏的拓展标记
        /// </summary>
        public ExtHiddenAttribute(bool value) : base(value)
        {
        }
    }
}
