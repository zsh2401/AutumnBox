/*

* ==============================================================================
*
* Filename: MethodProxy
* Description: 
*
* Version: 1.0
* Created: 2020/4/5 22:23:56
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
    public sealed class MethodProxy
    {
        private readonly object instance;
        private readonly MethodInfo method;

        public List<ILake> Sources
        {
            get => _srcs; set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                _srcs = value;
            }
        }
        private List<ILake> _srcs;

        public MethodProxy(object instance, string methodName, params ILake[] sources)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("message", nameof(methodName));
            }

            this.instance = instance ?? throw new ArgumentNullException(nameof(instance));
            method = FindMethodByName(methodName);
            this.Sources = sources.ToList();
        }

        public MethodProxy(object instance, MethodInfo method, params ILake[] sources)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (sources is null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            this.instance = instance;
            this.method = method;
            this.Sources = sources.ToList();
        }

        private MethodInfo FindMethodByName(string methodName)
        {
            return instance.GetType().GetMethod(methodName);
        }

        public object Invoke(Dictionary<string, object> extraArgs = null, bool inject = true)
        {
            var args = ArgsBuilder.BuildArgs(
                inject ? Sources : null,
                extraArgs ?? new Dictionary<string, object>(),
                method.GetParameters());
            return method.Invoke(instance, args);
        }
    }
}
