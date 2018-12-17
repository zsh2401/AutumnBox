using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;

namespace AutumnBox.OpenFramework.Management.Filters
{
    /// <summary>
    /// 开发模式筛选器
    /// </summary>
    public class DevelopingFilter : IWrapperFilter
    {
        public static DevelopingFilter Singleton { get; }
        static DevelopingFilter()
        {
            Singleton = new DevelopingFilter();
        }
        private DevelopingFilter() { }
        public bool Do(IExtensionWrapper Wrapper)
        {
            try
            {
                if (Wrapper.Info[ExtensionInformationKeys.IS_DEVELOPING] as bool? == true)
                {
                    return ((Context)Wrapper).BaseApi.IsDeveloperMode;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
