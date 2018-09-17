using AutumnBox.Basic.Device;
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
        bool Do(IExtensionWrapper Wrapper);
    }
}
