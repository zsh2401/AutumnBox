/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:18:26 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 特性实现此接口,将会对拓展模块进行信息标记
    /// </summary>
    public interface IInformationAttribute
    {
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }
        /// <summary>
        /// Value
        /// </summary>
        object Value { get; }
    }
}
