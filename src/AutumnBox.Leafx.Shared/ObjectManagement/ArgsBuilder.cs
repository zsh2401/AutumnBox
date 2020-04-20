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
                try
                {
                    args.Add(GetFromExtraArgs(p, extraArgs));
                }
                catch
                {
                    try
                    {
                        args.Add(GetFromSources(p, sources));
                    }
                    catch
                    {
                        args.Add(default);
                    }
                }

            }
            return args.ToArray();
        }

        private static object? GetFromExtraArgs(ParameterInfo pInfo, Dictionary<string, object> args)
        {
            return args[pInfo.Name];
        }

        private static object? GetFromSources(ParameterInfo pInfo, IEnumerable<ILake> sources)
        {
            try
            {
                return sources.Get(pInfo.Name);
            }
            catch
            {
                return sources.Get(pInfo.ParameterType);
            }
        }
    }
}
