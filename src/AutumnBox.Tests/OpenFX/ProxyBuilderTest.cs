///*

//* ==============================================================================
//*
//* Filename: ProxyBuilderTest
//* Description: 
//*
//* Version: 1.0
//* Created: 2020/3/12 22:12:39
//* Compiler: Visual Studio 2019
//*
//* Author: zsh2401
//*
//* ==============================================================================
//*/
//using AutumnBox.OpenFramework.Implementation;
//using AutumnBox.OpenFramework.Leafx;
//using AutumnBox.OpenFramework.Open;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutumnBox.Tests.OpenFX
//{
//    [TestClass]
//    public class ProxyBuilderTest
//    {
//        [TestMethod]
//        public void CreateInstanceTest()
//        {
//            var pb = new ProxyBuilder();
//            var proxy = pb.CreateProxyOf<Fuck>();
//            ILake lake = new ChinaLake();
//            lake.RegisterSingleton<ILake>(lake);
//            proxy.Lakes.Add(lake);
//            proxy.CreateInstance(new Dictionary<string, object>() { { "test", 1 }, { "test2", "c" } });
//            Assert.IsTrue(proxy.Instance != null);
//            Assert.IsTrue(proxy.Instance.Test == 1);
//            Assert.IsTrue(proxy.Instance.Test2 == "c");
//            Assert.IsTrue(proxy.Instance.Lake == lake);
//        }
//        [TestMethod]
//        public void InvokeMethodTest()
//        {
//            var pb = new ProxyBuilder();
//            var proxy = pb.CreateProxyOf(typeof(InvokeClass));
//            ILake lake = new ChinaLake();
//            lake.RegisterSingleton<ILake>(lake);
//            proxy.Lakes.Add(lake);
//            proxy.CreateInstance();
//            var invokeSuccess = false;
//            var callback = new Action<string, ILake>((a, _lake) =>
//            {
//                invokeSuccess = a != null && _lake != null;
//            });

//            proxy.InvokeMethod("Fuck", new Dictionary<string, object>() { { "a", "b" }, { "callback", callback } });
//            Assert.IsTrue(invokeSuccess);
//        }
//        private class InvokeClass
//        {
//            public void Fuck(string a, ILake lake, Action<string, ILake> callback)
//            {
//                Debug.WriteLine($"{a}/{lake}/{callback}");
//                callback(a, lake);

//            }
//        }
//        private class Fuck
//        {
//            public int Test { get; private set; }
//            public string Test2 { get; private set; }
//            public ILake Lake { get; }

//            public Fuck(int test, string test2, ILake lake)
//            {
//                if (lake is null)
//                {
//                    throw new ArgumentNullException(nameof(lake));
//                }

//                this.Test = test;
//                this.Test2 = test2;
//                Lake = lake;
//            }
//        }
//    }
//}
