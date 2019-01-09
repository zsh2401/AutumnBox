using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System.Reflection;

namespace AutumnBox.OpenFramework.Management.Filters
{
    /// <summary>
    /// 隐藏过滤器
    /// </summary>
    public class HideFilter : IWrapperFilter
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly HideFilter Singleton = new HideFilter();
        private HideFilter() { }
        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="Wrapper"></param>
        /// <returns></returns>
        public bool DoFilter(IExtensionWrapper Wrapper)
        {
            if (Wrapper.Info.TryGet(ExtensionInformationKeys.IS_HIDE, out object result))
            {
                return result == null || !(bool)result;
            }
            else
            {
                return true;
            }
        }
    }
}
