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
    public static class ArgsBuilder
    {
        public static object?[] BuildArgs(
            IEnumerable<ILake> sources,
            Dictionary<string, object> extraArgs,
            ParameterInfo[] parameterInfos)
        {

            List<object?> args = new List<object?>();

            foreach (var p in parameterInfos)
            {
                args.Add(GetArgs(p, extraArgs, sources));
            }
            return args.ToArray();
        }

        private static object? GetArgs(ParameterInfo pInfo, IDictionary<string, object> args, IEnumerable<ILake> sources)
        {
            if (args.TryGetValue(pInfo.Name, out object? value))
            {
                return value;
            }
            else
            {
                return sources.Get(pInfo.ParameterType);
            }
        }
    }
}
