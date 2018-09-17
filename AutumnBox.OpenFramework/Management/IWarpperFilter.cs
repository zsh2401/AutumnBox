using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Warpper;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// Warpper过滤器
    /// </summary>
    public interface IWarpperFilter
    {
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="warpper"></param>
        /// <returns></returns>
        bool Do(IExtensionWarpper warpper);
    }
}
