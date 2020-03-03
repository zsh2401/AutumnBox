/*

* ==============================================================================
*
* Filename: APIFactory
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 14:06:00
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.InnerImpl.Open
{
    public class APIFactory : ILake
    {
        public static APIFactory Instance { get; private set; }
        static APIFactory()
        {
            Instance = new APIFactory();
        }
        private readonly IMethodProxy methodProxy;
        private readonly Dictionary<Type, Func<object>> providers;
        private APIFactory()
        {
            methodProxy = new MethodProxy(this);
            providers = new Dictionary<Type, Func<object>>();

            RegisterSingleton(() => methodProxy);
        }

        public ILake Register<TTarget>(Func<TTarget> provider)
        {
            providers[typeof(TTarget)] = provider as Func<object>;
            return this;
        }
        public ILake RegisterSingleton<TTarget>(Func<TTarget> provider)
        {
            var lazyLoader = new Lazy<TTarget>(provider);
            providers[typeof(TTarget)] = () => lazyLoader.Value;
            return this;
        }
        public ILake Register<TTarget, TImpl>()
        {
            return Register<TTarget>(typeof(TImpl));
        }

        public ILake RegisterSingleton<TTarget, TImpl>()
        {
            return RegisterSingleton<TTarget>(typeof(TImpl));
        }

        public ILake Register<TTarget>(Type implType)
        {
            Register(typeof(TTarget), implType);
            return this;
        }

        public ILake RegisterSingleton<TTarget>(Type implType)
        {
            RegisterSingleton(typeof(TTarget), implType);
            return this;
        }

        public ILake Register(Type type, Func<object> provider)
        {
            providers[type] = provider as Func<object>;
            return this;
        }
        public ILake RegisterSingleton(Type type, Func<object> provider)
        {
            var lazyLoader = new Lazy<object>(provider);
            providers[type] = () => lazyLoader.Value;
            return this;
        }

        public ILake Register(Type beingImpl, Type impl)
        {
            var builder = methodProxy.GetClassBuilder(impl);
            providers[beingImpl] = builder;
            return this;
        }
        public ILake RegisterSingleton(Type beingImpl, Type impl)
        {
            var builder = methodProxy.GetClassBuilder(impl);
            var lazyLoader = new Lazy<object>(() => (object)builder());
            providers[beingImpl] = () => lazyLoader.Value;
            return this;
        }

        public TTarget Get<TTarget>()
        {
            return (TTarget)Get(typeof(TTarget));
        }
        public object Get(Type type)
        {
            return providers[type]();
        }
    }
}
