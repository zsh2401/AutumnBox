using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Extension.Leaf
{
    /// <summary>
    /// LeafExtension的拓展函数
    /// </summary>
    public static partial class LeafExtension
    {
        /// <summary>
        /// 获取当前LeafExtension实例的信息
        /// </summary>
        /// <param name="classExtension"></param>
        /// <param name="leafExtension"></param>
        /// <returns></returns>
        public static IExtensionInfo GetExtensionInfo(this IClassExtension classExtension)
        {
            var libsManager = LakeProvider.Lake.Get<ILibsManager>();
            return (from extInfo in libsManager.GetAllExtensions()
                    where (extInfo as ClassExtensionInfo)?.ClassExtensionType == classExtension.GetType()
                    select extInfo).FirstOrDefault();
        }



        /// <summary>
        /// 寻找一个LeafExtension的入口点函数
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
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
