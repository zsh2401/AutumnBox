using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// LeafExtension的帮助类
    /// </summary>
    public static class LeafExtensionHelper
    {
        /// <summary>
        /// 获取图标数组
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public static byte[] GetIconBytes(this LeafExtensionBase leaf)
        {
            var filted = from wrapper in Manager.InternalManager.GetLoadedWrappers()
                         where wrapper.Info.ExtType == leaf.GetType()
                         select wrapper;
            var result = filted.First();
            return result.Info.Icon;
        }
    }
}
