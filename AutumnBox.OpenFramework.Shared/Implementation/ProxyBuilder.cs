using AutumnBox.Logging;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.ProxyKit;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class ProxyBuilder : IProxyBuilder
    {
        public IProxy<T> CreateProxyOf<T>()
        {
            var proxy = new Proxy<T>();
            proxy.Lakes.Add(LakeProvider.Lake);
            return proxy;
        }

        public IProxy CreateProxyOf(Type type)
        {
            var proxy = new Proxy<object>(type);
            proxy.Lakes.Add(LakeProvider.Lake);
            return proxy;
        }

        public IProxy CreateProxyOf(object instance)
        {
            var proxy = new Proxy<object>(instance);
            proxy.Lakes.Add(LakeProvider.Lake);
            return proxy;
        }
    }
}
