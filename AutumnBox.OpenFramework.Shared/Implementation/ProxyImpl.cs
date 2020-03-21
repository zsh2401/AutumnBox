/*

* ==============================================================================
*
* Filename: ProxyImpl
* Description: 
*
* Version: 1.0
* Created: 2020/3/12 22:19:16
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.ProxyKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class Proxy<T> : IProxy, IProxy<T>
    {
        private readonly Type type;

        public Proxy()
        {
            this.type = typeof(T);
        }
        public Proxy(Type type)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
        }
        public Proxy(object instance)
        {
            this._instance = instance ?? throw new ArgumentNullException(nameof(instance));
            type = _instance.GetType();
        }
        private object _instance;


        public List<ILake> Lakes { get; } = new List<ILake>();



        T IProxy<T>.Instance => (T)_instance;

        object IProxy.Instance => _instance;

        public void CreateInstance(Dictionary<string, object> extraArgs = null)
        {
            var con = type.GetConstructors()[0];
            var args = BuildArgsArray(extraArgs ?? new Dictionary<string, object>(), con.GetParameters());
            _instance = Activator.CreateInstance(type, args);
            InjectProperty();
        }

        public void InjectProperty()
        {
            var injectableProperties = from property in type.GetProperties()
                                       where property.GetCustomAttribute<InjectAttribute>() != null
                                       where property.GetSetMethod() != null
                                       select property;
            foreach (var prop in injectableProperties)
            {
                if (TryGetValueFromLake(prop.PropertyType, out object value))
                {
                    prop.GetSetMethod().Invoke(_instance, new object[] { value });
                }
            }
        }
        public object InvokeMethod(string methodName, Dictionary<string, object> extraArgs = null)
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("Instance has not been created");
            }
            var method = type.GetMethod(methodName);
            if (method == null)
            {
                throw new ArgumentException($"Method '{methodName}' Not Found");
            }
            Debug.WriteLine(method);
            var args = BuildArgsArray(extraArgs ?? new Dictionary<string, object>(), method.GetParameters());
            return method.Invoke(_instance, args);
        }

        private bool TryGetValueFromLake(Type t, out object value)
        {
            for (int i = Lakes.Count() - 1; i >= 0; i--)
            {
                try
                {
                    value = Lakes[i].Get(t);
                    return true;
                }
                catch { }
            }
            value = null;
            return false;
        }

        private object[] BuildArgsArray(Dictionary<string, object> extraArgs, ParameterInfo[] parasInfo)
        {
            List<object> result = new List<object>();
            foreach (var p in parasInfo)
            {
                if (extraArgs.TryGetValue(p.Name, out object vBuffer))
                {
                    result.Add(vBuffer);
                }
                else if (TryGetValueFromLake(p.ParameterType, out vBuffer))
                {
                    result.Add(vBuffer);
                }
                else
                {
                    result.Add(null);
                }
            }
            return result.ToArray();
        }
    }
}
