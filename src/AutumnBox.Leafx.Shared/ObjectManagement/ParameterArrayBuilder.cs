#nullable enable
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
using AutumnBox.Leafx.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutumnBox.Leafx.ObjectManagement
{
    public static class ParameterArrayBuilder
    {
        public static object?[] BuildArgs(
            ILake lake,
            Dictionary<string, object> extraArgs,
            ParameterInfo[] parameterInfos)
        {
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
                    args.Add(default);
                }
            }
            return args.ToArray();
        }
    }
}
