using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System.Reflection;

namespace AutumnBox.OpenFramework.Management.Filters
{
    /// <summary>
    /// 开发模式筛选器
    /// </summary>
    public class DevelopingFilter : IWrapperFilter
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly DevelopingFilter Singleton = new DevelopingFilter();
        private DevelopingFilter() { }
        /// <summary>
        /// 执行过滤
        /// </summary>
        /// <param name="Wrapper"></param>
        /// <returns></returns>
        public bool DoFilter(IExtensionWrapper Wrapper)
        {
            if (Wrapper.Info.ExtType.GetCustomAttribute(typeof(ExtDeveloperMode)) != null)
            {
                return (Wrapper as Context).App.IsDeveloperMode;
            }
            else
            {
                return true;
            }
        }
    }
}
