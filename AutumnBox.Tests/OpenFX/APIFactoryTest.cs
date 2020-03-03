/*

* ==============================================================================
*
* Filename: APIFactoryTest
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 14:34:01
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.InnerImpl.Open;
using AutumnBox.OpenFramework.Open;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX
{
    [TestClass]
    public class APIFactoryTest
    {
        [TestMethod]
        public void RegisterTest()
        {
            ILake factory = APIFactory.Instance;
            factory.Register<string>(() => "test");
            Assert.IsTrue(factory.Get<string>() == "test");
        }

        [TestMethod]
        public void GetNotExistTest()
        {
            ILake factory = APIFactory.Instance;
            Exception ex = null;
            try { factory.Get<string>(); }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.IsTrue(ex != null);
        }

        [TestMethod]
        public void RegisterSingletonTest()
        {
            ILake factory = APIFactory.Instance;
            Random ran = new Random();

            factory.RegisterSingleton<int>(() => ran.Next());
            Assert.IsTrue(factory.Get<int>() == factory.Get<int>());

            factory.RegisterSingleton(typeof(int), () => ran.Next());
            Assert.IsTrue(factory.Get<int>() == factory.Get<int>());
        }

        [TestMethod]
        public void RegisterAgainTest()
        {
            ILake factory = APIFactory.Instance;
            factory.Register<string>(() => "test");
            factory.Register<string>(() => "b");
            Assert.IsTrue(factory.Get<string>() == "b");
        }

        [TestMethod]
        public void InjectTest() {
            ILake factory = APIFactory.Instance;
            Random ran = new Random();
            var number = ran.Next();
            factory.RegisterSingleton<RequireValueClass>(typeof(RequireValueClass));
            factory.RegisterSingleton<int>(() => number);
            Debug.WriteLine(number);
            var x = factory.Get<RequireValueClass>();
            Debug.WriteLine(x.X);
            Assert.IsTrue(x.X == number);
        }
        private class RequireValueClass {
            public RequireValueClass(IMethodProxy methodProxy,int x) {
                if (methodProxy is null)
                {
                    throw new ArgumentNullException(nameof(methodProxy));
                }

                X = x;
            }

            public int X { get; }
        }
    }
}
