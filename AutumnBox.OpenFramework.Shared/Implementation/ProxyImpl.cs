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
        private object _instance;


        public List<ILake> Lakes { get; } = new List<ILake>();



        T IProxy<T>.Instance => (T)_instance;

        object IProxy.Instance => _instance;

        public void CreateInstance(Dictionary<string, object> extraArgs = null)
        {
            var con = type.GetConstructors()[0];
            var args = BuildArgsArray(extraArgs ?? new Dictionary<string, object>(), con.GetParameters());
            _instance = Activator.CreateInstance(type, args);
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

            foreach (var lake in Lakes)
            {
                try
                {
                    value = lake.Get(t);
                    return true;
                }
                catch
                {
                }
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
