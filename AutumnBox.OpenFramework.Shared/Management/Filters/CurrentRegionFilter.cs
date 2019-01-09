/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/17 19:45:45 (UTC +8:00)
** desc： ...
*************************************************/
using System.Linq;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Wrapper;

namespace AutumnBox.OpenFramework.Management.Filters
{
    /// <summary>
    /// 根据目前秋之盒的区域进行筛选的筛选器
    /// </summary>
    public class CurrentRegionFilter :Context, IWrapperFilter
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static CurrentRegionFilter Singleton { get; private set; }
        static CurrentRegionFilter() {
            Singleton = new CurrentRegionFilter();
        }
        private CurrentRegionFilter() { }
        /// <summary>
        /// DO!
        /// </summary>
        /// <param name="wrapper"></param>
        /// <returns></returns>
        public bool DoFilter(IExtensionWrapper wrapper)
        {
            var crtLanCode = App.CurrentLanguageCode.ToLower();
            if (wrapper.Info.Regions == null)
            {
                return true;
            }
            return wrapper.Info.Regions.Where((str) =>
            {
                return str.ToLower() == crtLanCode;
            }).Count() > 0;
        }
    }
}
