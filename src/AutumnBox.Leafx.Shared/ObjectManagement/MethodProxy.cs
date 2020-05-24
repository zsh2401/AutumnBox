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

        /// <summary>
        /// 用于IoC的组件源
        /// </summary>
        private readonly ILake source;

        /// <summary>
        /// 构建一个方法代理器实例
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="source"></param>
        public MethodProxy(object instance, string methodName, ILake source)
        {
            this.instance = instance;
            this.method = FindMethodByName(methodName);
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// 构建一个方法代理器实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <param name="method">方法</param>
        /// <param name="source">依赖注入源</param>
        public MethodProxy(object instance, MethodInfo method, ILake source)
        {
            this.instance = instance;
            this.method = method ?? throw new ArgumentNullException(nameof(method));
            this.source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// 根据方法名称找到方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="extraArgs"></param>
        /// <exception cref="InvalidOperationException">实例不存在</exception>
        /// <exception cref="TargetInvocationException">方法执行时抛出异常</exception>
        /// <returns></returns>
        public object Invoke(Dictionary<string, object> extraArgs = null)
        {
            if (instance == null) throw new InvalidOperationException("Please create instance before invoke method");

            object[] args = ParameterArrayBuilder.BuildArgs(
                source,
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

        /// <summary>
        /// 获取所有环绕切面
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AroundMethodAttribute> GetAroundAspects()
        {
            return method.GetCustomAttributes<AroundMethodAttribute>();
        }

        /// <summary>
        /// 获取一个执行器
        /// </summary>
        /// <param name="extraArgs"></param>
        /// <returns></returns>
        public Func<object> GetInvoker(Dictionary<string, object> extraArgs = null)
        {
            return () =>
            {
                return Invoke(extraArgs);
            };
        }
    }
}
