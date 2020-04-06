using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Extension.Leaf.Internal;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using AutumnBox.OpenFramework.Leafx.Container;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.Extension.Leaf.Internal
{
    internal class LeafEntryExecutor
    {
        private readonly LeafExtensionBase ext;
        private readonly ApiAllocator apiAllocator;
        private MethodInfo entry;

        public LeafEntryExecutor(LeafExtensionBase ext, ApiAllocator apiAllocator)
        {
            this.ext = ext ?? throw new ArgumentNullException(nameof(ext));
            this.apiAllocator = apiAllocator ?? throw new ArgumentNullException(nameof(apiAllocator));

        }
        /// <summary>
        /// 获取参数列表对应的值列表
        /// </summary>
        /// <param name="pInfos"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private object[] GetPara(ParameterInfo[] pInfos, Dictionary<string, object> args)
        {
            apiAllocator.ExtData = args ?? throw new NullReferenceException("ext data is null!!");
            List<object> ps = new List<object>();
            foreach (var pInfo in pInfos)
            {
                if (pInfo.ParameterType == typeof(Dictionary<string, object>))
                {
                    ps.Add(args);
                }
                else
                {
                    try
                    {
                        var result = apiAllocator.GetParamterValue(pInfo);
                        ps.Add(result);
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e);
                        ps.Add(null);
                    }
                }
            }
            return ps.ToArray();
        }
        /// <summary>
        /// 寻找入口点函数
        /// </summary>
        /// <returns></returns>
        private MethodInfo FindEntry()
        {
            var type = ext.GetType();
            var methods = from method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                          where method.GetCustomAttribute<LDoNotScanAttribute>() == null
                          select method;
            var result = FindExplicitMain(methods);
            if (result == null) return FindImplicitMain(methods) ?? throw new Exception("Entry not found");
            else return result;
        }
        /// <summary>
        /// 判断是否是显式入口点
        /// </summary>
        /// <returns></returns>
        private MethodInfo FindExplicitMain(IEnumerable<MethodInfo> methods)
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
        private MethodInfo FindImplicitMain(IEnumerable<MethodInfo> methods)
        {
            var filt = from method in methods
                       where method.Name == "Main" && !IsClassExtensionMain(method)
                       select method;
            return filt.Any() ? filt.First() : null;
        }
        /// <summary>
        /// 判断是否为IExtension.Main
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private bool IsClassExtensionMain(MethodInfo method)
        {
            var tMethod = typeof(IExtension).GetMethod(nameof(IExtension.Main));
            return method == tMethod;
        }
        /// <summary>
        /// 进行执行
        /// </summary>
        /// <param name="argsDictionary"></param>
        /// <returns>可能的int返回值</returns>
        public object Execute(Dictionary<string, object> argsDictionary)
        {
            entry = FindEntry();
            //获取其需要的参数列表
            var para = GetPara(entry.GetParameters(), argsDictionary);
            //执行
            object result = null;
            result = entry.Invoke(ext, para);
            LakeProvider.Lake.Get<ISoundService>().OK();
            //处理可能的返回值
            if (result is int exitCode)
            {
                return exitCode;
            }
            else
            {
                return null;
            }
        }
    }
}
