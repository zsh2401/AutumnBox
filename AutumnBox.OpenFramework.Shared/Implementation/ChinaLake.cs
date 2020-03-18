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
        private readonly Dictionary<string, Func<object>> factories;

        public ChinaLake()
        {
            factories = new Dictionary<string, Func<object>>();
        }

        public object Get(string id)
        {
            return factories[id]();
        }

        public ILake Register(string id, Func<object> factory)
        {
            factories[id] = factory;
            return this;
        }
    }
}
