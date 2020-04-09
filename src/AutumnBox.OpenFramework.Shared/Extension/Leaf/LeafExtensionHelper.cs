using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Leafx;
using AutumnBox.OpenFramework.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.Wrapper;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Leafx.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.Extension.Leaf
{
    /// <summary>
    /// LeafExtension的拓展函数
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
            var filted = from wrapper in OpenFx.Lake.Get<ILibsManager>().Wrappers()
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
        public static IExtensionInfoDictionary GetInformations(this LeafExtensionBase leaf)
        {
            var filted = from wrapper in OpenFx.Lake.Get<ILibsManager>().Wrappers()
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
            var filted = from wrapper in OpenFx.Lake.Get<ILibsManager>().Wrappers()
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
        public static void EndCurrentLeafThread(this LeafExtensionBase leaf, int exitCode = 0)
        {
            throw new LeafTerminatedException(exitCode);
        }
        internal static void InjectProperties(this LeafExtensionBase leaf, params ILake[] sources)
        {
            new PropertyInjector(leaf, sources).Inject();
        }
        internal static MethodInfo FindEntryPoint(this LeafExtensionBase leaf)
        {
            if (leaf == null)
            {
                throw new System.ArgumentNullException(nameof(leaf));
            }
            return FindEntry(leaf.GetType());
        }

        /// <summary>
        /// 寻找入口点函数
        /// </summary>
        /// <returns></returns>
        private static MethodInfo FindEntry(Type type)
        {
            var methods = from method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                          where method.GetCustomAttribute<LDoNotScanAttribute>() == null
                          select method;
            var result = FindExplicitMain(methods);
            if (result == null) return FindImplicitMain(methods) ?? throw new Exception($"Entry not found in {type.FullName}");
            else return result;
        }
        /// <summary>
        /// 判断是否是显式入口点
        /// </summary>
        /// <returns></returns>
        private static MethodInfo FindExplicitMain(IEnumerable<MethodInfo> methods)
        {
            var filt = from method in methods
                       where method.GetCustomAttribute<LMainAttribute>() != null
                       select method;
            return filt.Any() ? filt.First() : null;
        }
        /// <summary>
        /// 判断是否是隐式入口点
        /// </summary>
        /// <returns></returns>
        private static MethodInfo FindImplicitMain(IEnumerable<MethodInfo> methods)
        {
            var filt = from method in methods
                       where (method.Name == "Main" || method.Name == "LMain") && !IsClassExtensionMain(method)
                       select method;
            return filt.Any() ? filt.First() : null;
        }
        /// <summary>
        /// 判断是否为IExtension.Main
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool IsClassExtensionMain(MethodInfo method)
        {
            var tMethod = typeof(IExtension).GetMethod(nameof(IExtension.Main));
            return method == tMethod;
        }
    }
}
