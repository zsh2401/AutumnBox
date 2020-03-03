/*

* ==============================================================================
*
* Filename: MethodProxy
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 14:31:15
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class MethodProxy : IMethodProxy
    {
        private readonly ILake factory;

        public MethodProxy(ILake factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public object CallMethod(object _this, string name)
        {
            var method = _this.GetType().GetMethod(name);
            var parameters = method.GetParameters();
            var parameterValueList = new List<object>();
            foreach (var p in parameters)
            {
                object v;
                try
                {
                    v = factory.Get(p.ParameterType);
                }
                catch
                {
                    v = null;
                }
                parameterValueList.Add(v);
            }
            return method.Invoke(_this, parameterValueList.ToArray());
        }

        public Func<TClass> GetClassBuilder<TClass>()
        {
            return () => (TClass)GetClassBuilder(typeof(TClass))();
        }

        public Func<object> GetClassBuilder(Type classType)
        {
            var x = classType.GetConstructors()[0];
            var parameters = x.GetParameters();
            var args = new List<object>();
            foreach (var p in parameters)
            {
                object v;
                try
                {
                    v = factory.Get(p.ParameterType);
                }
                catch(Exception e)
                {
                    SLogger<MethodProxy>.Warn("Can not inject value",e);
                    v = null;
                }
                args.Add(v);
            }
            return () => Activator.CreateInstance(classType, args.ToArray());
        }

        public Func<object> GetMethodCaller(object owner, string methodName)
        {
            return () =>
            {
                return CallMethod(owner, methodName);
            };
        }
    }
}
