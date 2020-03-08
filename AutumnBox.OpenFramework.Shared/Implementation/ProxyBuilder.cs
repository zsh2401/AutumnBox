using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.ProxyKit;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Implementation
{
    class ProxyBuilder : IProxyBuilder
    {
        private class Proxy : IProxy
        {
            private readonly Type type;
            private object instance;
            public Proxy(Type type)
            {
                this.type = type ?? throw new ArgumentNullException(nameof(type));
            }
            public Func<Type, object> ExtraDependencyFactory { set => throw new NotImplementedException(); }

            public object Instance => instance;

            public List<ILake> Lakes { get; } = new List<ILake>();

            public List<IKeyLake> KeyLakes { get; } = new List<IKeyLake>();

            public event RequestingObjectEventHandler RequestingObject;

            public void CreateInstance()
            {
                var x = type.GetConstructors()[0];
                var parameters = x.GetParameters();
                var args = new List<object>();
                foreach (var p in parameters)
                {
                    object v;
                    try
                    {
                        v = factory.Get(p.ParameterType);
                    }
                    catch (Exception e)
                    {
                        SLogger<MethodProxy>.Warn("Can not inject value", e);
                        v = null;
                    }
                    args.Add(v);
                }
                return () => Activator.CreateInstance(classType, args.ToArray());
            }

            public object InvokeMethod(string methodName)
            {
                throw new NotImplementedException();
            }
        }
        public IProxy CreateProxyOf(Type type)
        {
            return new Proxy(type);
        }

        public IProxy CreateProxyOf<T>()
        {
            return new Proxy(typeof(T));
        }
    }
}
