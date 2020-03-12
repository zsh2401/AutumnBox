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
using System.Diagnostics;
using System.Threading;

namespace AutumnBox.OpenFramework.Implementation
{
    public class ChinaLake : ILake
    {
        private readonly Dictionary<Type, Func<object>> factories;

        public ChinaLake()
        {
            factories = new Dictionary<Type, Func<object>>();
        }

        public object Get(Type type)
        {
            return factories[type]();
        }

        public ILake Register(Type type, Func<object> factory)
        {
            factories[type] = factory;
            return this;
        }
    }
}
