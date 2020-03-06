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

namespace AutumnBox.OpenFramework.Implementation
{
    internal class ChinaLake : ILake
    {
        delegate object Factory();
        private Dictionary<Type, Factory> factories;
        public object Get(Type type)
        {
            return factories[type]();
        }

        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public ILake Register(Type type, Func<object> factory)
        {
            throw new NotImplementedException();
        }

        public ILake Register<T>(Func<object> factory)
        {
            throw new NotImplementedException();
        }

        public ILake Register<T>(Type impl)
        {
            throw new NotImplementedException();
        }

        public ILake Register<T, TImpl>()
        {
            throw new NotImplementedException();
        }

        public ILake RegisterSingleton(Type type, Func<object> factory)
        {
            throw new NotImplementedException();
        }

        public ILake RegisterSingleton<T>(Func<object> factory)
        {
            throw new NotImplementedException();
        }

        public ILake RegisterSingleton<T>(Type impl)
        {
            throw new NotImplementedException();
        }

        public ILake RegisterSingleton<T, TImpl>()
        {
            throw new NotImplementedException();
        }

        public ILake RegisterSingleton<T>(T value)
        {
            throw new NotImplementedException();
        }
    }
}
