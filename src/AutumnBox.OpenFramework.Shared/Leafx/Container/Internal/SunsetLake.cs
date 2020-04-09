/*

* ==============================================================================
*
* Filename: SunsetLake
* Description: 
*
* Version: 1.0
* Created: 2020/4/3 23:03:17
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.Container.Internal
{
    public class SunsetLake : IRegisterableLake
    {
        private readonly Dictionary<string, Func<object>> factories = new Dictionary<string, Func<object>>();
        public object Get(string id)
        {
            return factories[id]();
        }

        public void Register(string id, Func<object> factory)
        {
            factories[id] = factory;
        }
    }
}
