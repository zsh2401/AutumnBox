/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:18:26 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Wrapper;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// Wrapper过滤器
    /// </summary>
    public interface IWrapperFilter
    {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="Wrapper"></param>
        /// <returns></returns>
        bool DoFilter(IExtensionWrapper Wrapper);
    }
}
