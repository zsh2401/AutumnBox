namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 隐藏的拓展
    /// </summary>
    public class ExtHideAttribute : SingleInfoAttribute
    {
        /// <summary>
        /// KEY
        /// </summary>
        public override string Key => ExtensionInformationKeys.IS_HIDE;
        /// <summary>
        /// 隐藏的拓展标记
        /// </summary>
        public ExtHideAttribute() : base(true) { }
        /// <summary>
        /// 隐藏的拓展标记
        /// </summary>
        public ExtHideAttribute(bool value) : base(value)
        {
        }
    }
}
