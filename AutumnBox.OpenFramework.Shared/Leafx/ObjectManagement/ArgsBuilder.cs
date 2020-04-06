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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.ObjectManagement
{
    internal static class ArgsBuilder
    {
        public static object[] BuildArgs(
            IEnumerable<ILake> sources,
            Dictionary<string, object> extraArgs,
            ParameterInfo[] parameterInfos)
        {
            List<object> args = new List<object>();
            foreach (var p in parameterInfos)
            {
                args.Add(GetFromExtraArgs(p, extraArgs) ?? GetFromSources(p, sources) ?? null);
            }
            return args.ToArray();
        }

        private static object GetFromExtraArgs(ParameterInfo pInfo, Dictionary<string, object> args)
        {
            if (args.TryGetValue(pInfo.Name, out object value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }
        private static object GetFromSources(ParameterInfo pInfo, IEnumerable<ILake> sources)
        {
            if (sources == null) return null;
            for (int i = sources.Count(); i >= 0; i--)
            {
                try
                {
                    return sources.ElementAt(i).Get(pInfo.ParameterType);
                }
                catch { }
            }
            return null;
        }
    }
}
