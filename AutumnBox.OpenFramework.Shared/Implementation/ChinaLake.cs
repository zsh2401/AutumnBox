/*

* ==============================================================================
*
* Filename: LakeImpl
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 16:17:57
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class ChinaLake : ILake
    {
        private readonly Dictionary<Type, Func<object>> factories;
        public ChinaLake()
        {
            factories = new Dictionary<Type, Func<object>>();
            RegisterSingleton<ILake>(this);
            RegisterSingleton<IMethodProxy, MethodProxy>();
        }
        public object Get(Type type)
        {
            return factories[type]();
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public ILake Register(Type type, Func<object> factory)
        {
            factories[type] = factory;
            return this;
        }

        public ILake Register<T>(Func<object> factory)
        {
            return Register(typeof(T), factory);
        }

        public ILake Register<T>(Type impl)
        {
            return Register(typeof(T), Get<IMethodProxy>().GetClassBuilder(impl));
        }

        public ILake Register<T, TImpl>()
        {
            return Register<T>(typeof(TImpl));
        }

        public ILake RegisterSingleton(Type type, Func<object> factory)
        {
            var lazy = new Lazy<object>(factory);
            return Register(type, () => lazy.Value);
        }

        public ILake RegisterSingleton<T>(Func<object> factory)
        {
            return RegisterSingleton(typeof(T), factory);
        }

        public ILake RegisterSingleton<T>(Type impl)
        {
            return RegisterSingleton<T>(() => Get<IMethodProxy>().GetClassBuilder(impl));
        }

        public ILake RegisterSingleton<T, TImpl>()
        {
            return RegisterSingleton<T>(typeof(TImpl));
        }

        public ILake RegisterSingleton<T>(T value)
        {
            return RegisterSingleton<T>(() => value);
        }
    }
}
