using AutumnBox.OpenFramework.Open;
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

        private Dictionary<string, object> data;

        private readonly MethodInfo entry;

        public LeafEntryExecutor(LeafExtensionBase ext)
        {
            this.ext = ext ?? throw new ArgumentNullException(nameof(ext));
            entry = FindEntry();
        }
        /// <summary>
        /// 获取参数列表对应的值列表
        /// </summary>
        /// <param name="pInfos"></param>
        /// <returns></returns>
        private object[] GetPara(ParameterInfo[] pInfos)
        {
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
                        var result = ApiAllocator.GetParamterValue(data, ext.GetType(), ext.Context, pInfo);
                        ps.Add(result);
                    }
                    catch
                    {
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
            var explicitMain = from method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                               where IsExplicitMain(method)
                               select method;
            if (explicitMain.Any())
            {
                return explicitMain.First();
            }
            else
            {
                var implicitMain = from method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                   where IsImplicitMain(method)
                                   select method;
                if (!implicitMain.Any())
                {
                    throw new Exception("Entry not found!");
                }
                else
                {
                    return implicitMain.First();
                }
            }
        }
        /// <summary>
        /// 判断是否是显式入口点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool IsExplicitMain(MethodInfo info)
        {
            return info.GetCustomAttribute<LMainAttribute>() != null;
        }
        /// <summary>
        /// 判断是否是隐式入口点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool IsImplicitMain(MethodInfo info)
        {
            return info.Name == "Main" && info.GetCustomAttribute<LDoNotScan>() == null;
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
            var result = entry.Invoke(ext, para);
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
