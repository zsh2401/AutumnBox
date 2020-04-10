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
using AutumnBox.Leafx.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutumnBox.Leafx.ObjectManagement
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
            this.method = FindMethodByName(methodName);
            this.Sources = sources?.ToList() ?? new List<ILake>();
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
            this.Sources = sources?.ToList() ?? new List<ILake>();
        }

        private MethodInfo FindMethodByName(string methodName)
        {
            var result = instance.GetType().GetMethod(methodName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Default | BindingFlags.Static);
            if (result == null)
            {
                throw new InvalidOperationException($"Method {methodName} not found");
            }
            return result;
        }

        public object Invoke(Dictionary<string, object> extraArgs = null, bool inject = true)
        {
            if (instance == null) throw new InvalidOperationException("test");
            object[] args = ArgsBuilder.BuildArgs(
                inject ? Sources : null,
                extraArgs ?? new Dictionary<string, object>(),
                method.GetParameters());

            return method.Invoke(instance, args);
        }

        public Func<object> GetInvoker(Dictionary<string, object> extraArgs = null, bool inject = true)
        {
            return () =>
            {
                return Invoke(extraArgs, inject);
            };
        }
    }
}
