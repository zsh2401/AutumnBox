using AutumnBox.OpenFramework.Open.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Open.Management
{
    public static class OpenApiFactory
    {
        private const string IMPLS_NAMESPACE = "AutumnBox.OpenFramework.Open.Impl";
        static Dictionary<Type, Type> ApiAndImplsInformation = new Dictionary<Type, Type>();
        static OpenApiFactory()
        {
            ScanAndLoad();
        }
        static void ScanAndLoad()
        {
            var types = from type in Assembly.GetCallingAssembly().GetTypes()
                        where type.Namespace == IMPLS_NAMESPACE
                        select type;
            foreach (var type in types)
            {
                foreach (var _interface in type.GetInterfaces())
                {
                    ApiAndImplsInformation.Add(_interface, type);
                }
            }
        }
        public static object Get(Type interfaceType, object requester = null, object arg = null)
        {
            if (ApiAndImplsInformation.TryGetValue(interfaceType, out Type implType))
            {
                try
                {
                    return Activator.CreateInstance(implType, new InitSettings(requester, arg));
                }
                catch
                {
                    return Activator.CreateInstance(implType);
                }
            }
            else
            {
                return null;
            }
        }
        public static TApi Get<TApi>(object requester=null, object arg=null)
        {
            return (TApi)Get(typeof(TApi), requester, arg);
        }
    }
}
