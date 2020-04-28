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
    /// <summary>
    /// 方法代理器
    /// </summary>
    public sealed class MethodProxy
    {
        /// <summary>
        /// 执行方法的实例
        /// </summary>
        private readonly object instance;

        /// <summary>
        /// 被代理的方法
        /// </summary>
        private readonly MethodInfo method;
        private readonly ILake source;

        public MethodProxy(object instance, string methodName, ILake source)
        {

            this.method = FindMethodByName(methodName);
            this.instance = instance;
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public MethodProxy(object instance, MethodInfo method, ILake source)
        {
            this.instance = instance;
            this.method = method ?? throw new ArgumentNullException(nameof(method));
            this.source = source ?? throw new ArgumentNullException(nameof(source));
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
            if (instance == null) throw new InvalidOperationException("Please create instance before invoke method");

            object[] args = ParameterArrayBuilder.BuildArgs(
                inject ? source : null,
                extraArgs ?? new Dictionary<string, object>(),
                method.GetParameters());

            object rawInvoker() => method.Invoke(instance, args);

            var aspects = GetAroundAspects();
            if (aspects.Any())
            {
                var aspectArgs = new AroundMethodArgs()
                {
                    MethodInfo = method,
                    Instance = instance,
                    Args = args,
                    ExtraArgs = extraArgs,
                    Invoker = rawInvoker
                };
                return aspects.ElementAt(0).Around(aspectArgs);
            }
            else
            {
                return rawInvoker();
            }
        }

        public IEnumerable<AroundMethodAttribute> GetAroundAspects()
        {
            return method.GetCustomAttributes<AroundMethodAttribute>();
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
