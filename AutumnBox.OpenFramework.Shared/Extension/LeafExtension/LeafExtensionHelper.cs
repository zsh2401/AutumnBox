using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Wrapper;
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
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public static IExtInfoGetter GetInformations(this LeafExtensionBase leaf)
        {
            var filted = from wrapper in Manager.InternalManager.GetLoadedWrappers()
                         where wrapper.Info.ExtType == leaf.GetType()
                         select wrapper;
            return filted.First().Info;
        }
        /// <summary>
        /// 获取图标数组
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public static string GetName(this LeafExtensionBase leaf)
        {
            var filted = from wrapper in Manager.InternalManager.GetLoadedWrappers()
                         where wrapper.Info.ExtType == leaf.GetType()
                         select wrapper;
            var result = filted.First();
            return result.Info.Name;
        }
        /// <summary>
        /// 通过抛出指定的异常中断模块主要流程
        /// </summary>
        /// <exception cref="LeafTerminatedException"></exception>
        /// <param name="leaf"></param>
        /// <param name="exitCode"></param>
        public static void End(this LeafExtensionBase leaf,int exitCode=0)
        {
            throw new LeafTerminatedException(exitCode);
        }
    }
}
