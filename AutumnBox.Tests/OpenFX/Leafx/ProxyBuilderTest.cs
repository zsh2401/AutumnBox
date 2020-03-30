/*

* ==============================================================================
*
* Filename: ProxyBuilderTest
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:52:15
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Leafx.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX.Leafx
{
    [TestClass]
    public class ProxyBuilderTest
    {
        private class TestClass
        {
            public bool TestMethod()
            {
                Trace.WriteLine("wow");
                return true;
            }
        }
        [TestMethod]
        public void BuildProxy()
        {
            IAOPProxyBuilder aopProxyBuilder = new AOPProxyBuilder();
            IDisposable instance = (IDisposable)aopProxyBuilder.Build(typeof(IDisposable));
            instance.Dispose();
        }
    }
}
