using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    internal class LeafEntryExecutor
    {
        private readonly LeafExtensionBase ext;
        private readonly ApiAllocator apiAllocator;
        private Dictionary<string, object> data;

        private readonly MethodInfo entry;

        public LeafEntryExecutor(LeafExtensionBase ext, ApiAllocator apiAllocator)
        {
            this.ext = ext ?? throw new ArgumentNullException(nameof(ext));
            this.apiAllocator = apiAllocator ?? throw new ArgumentNullException(nameof(apiAllocator));
            entry = FindEntry();
        }
        /// <summary>
        /// 获取参数列表对应的值列表
        /// </summary>
        /// <param name="pInfos"></param>
        /// <returns></returns>
        private object[] GetPara(ParameterInfo[] pInfos)
        {
            apiAllocator.ExtData = data ?? throw new NullReferenceException("ext data is null!!");
            List<object> ps = new List<object>();
            foreach (var pInfo in pInfos)
            {
                if (pInfo.ParameterType == typeof(Dictionary<string, object>))
                {
                    ps.Add(data);
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
                          where method.GetCustomAttribute<LDoNotScan>() == null
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
        private bool IsClassExtensionMain(MethodInfo method)
        {
            var para = method.GetParameters();
            return para.Length == 1 && para[0].ParameterType == typeof(Dictionary<string, object>);
        }
        /// <summary>
        /// 进行执行
        /// </summary>
        /// <param name="data">键值对数据</param>
        /// <returns>可能的int返回值</returns>
        public int? Execute(Dictionary<string, object> data = null)
        {
            this.data = data;
            //获取其需要的参数列表
            var para = GetPara(entry.GetParameters());
            //执行
            object result = null;
            try
            {
                result = entry.Invoke(ext, para);
                Sound.Play(Sound.Id.Ok);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException is LeafTerminatedException _e)
                {
                    result = _e.ExitCode;
                }
                else
                {
                    throw e.InnerException;
                }
            }

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
