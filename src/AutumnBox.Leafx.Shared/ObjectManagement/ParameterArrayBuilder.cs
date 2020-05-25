/*

* ==============================================================================
*
* Filename: ArgsBuilder
* Description: 
*
* Version: 1.0
* Created: 2020/4/5 22:54:18
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
#nullable enable
using AutumnBox.Leafx.Container;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.Leafx.ObjectManagement
{
    /// <summary>
    /// 参数列表构造器
    /// </summary>
    public static class ParameterArrayBuilder
    {
        /// <summary>
        /// 根据参数请求列表,获取参数
        /// </summary>
        /// <param name="lake">用作依赖属性的湖</param>
        /// <param name="extraArgs">额外的,指定参数名的参数</param>
        /// <param name="parameterInfos">参数列表</param>
        /// <exception cref="System.ArgumentNullException">参数为空</exception>
        /// <returns>应得的参数</returns>
        public static object?[] BuildArgs(
            ILake lake,
            Dictionary<string, object> extraArgs,
            ParameterInfo[] parameterInfos)
        {
            if (lake is null)
            {
                throw new System.ArgumentNullException(nameof(lake));
            }

            if (extraArgs is null)
            {
                throw new System.ArgumentNullException(nameof(extraArgs));
            }

            if (parameterInfos is null)
            {
                throw new System.ArgumentNullException(nameof(parameterInfos));
            }

            List<object?> args = new List<object?>();
            foreach (var p in parameterInfos)
            {
                if (extraArgs.TryGetValue(p.Name, out object value))
                {
                    args.Add(value);
                }
                else if (lake.TryGet(p.Name, out object? byNameValue))
                {
                    args.Add(byNameValue);
                }
                else if (lake.TryGet(p.ParameterType, out object? byTypeValue))
                {
                    args.Add(byTypeValue);
                }
                else
                {
                    args.Add(p.DefaultValue);
                }
            }
            return args.ToArray();
        }
    }
}
