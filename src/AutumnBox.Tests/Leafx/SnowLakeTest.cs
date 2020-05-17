/*

* ==============================================================================
*
* Filename: SnowLakeTest
* Description: 
*
* Version: 1.0
* Created: 2020/5/17 10:35:17
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.Leafx
{
    [TestClass]
    public class SnowLakeTest
    {
        [TestMethod]
        public void ExtensionMethodTest()
        {
            IRegisterableLake lake = new SnowLake();
            lake.RegisterComponent("t", () => -1);
            lake.RegisterComponent("t", () => 1);
            lake.RegisterComponent("t", () => 2);

            Assert.IsTrue(lake.GetComponents("t").Count() == 3);
            Assert.IsTrue((lake.GetComponent("t") as int?) == -1);
        }

        [TestMethod]
        public void CheckTest()
        {
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                ILake lake = new SunsetLake();
                lake.GetComponents("t");
            });

            ILake lake = new TestLake();
            Assert.IsTrue(lake.GetComponents("t").Count() == 0);
        }

        private class TestLake : ILake
        {
            public int Count => 0;

            public object GetComponent(string id)
            {
                throw new IdNotFoundException(id);
            }

            public IEnumerable<object> GetComponents(string id)
            {
                return new object[0];
            }
        }
        [TestMethod]
        public void RegisteringTest()
        {
            Random random = new Random();
            IRegisterableLake lake = new SnowLake();
            lake.RegisterSingleton("id", () => random.Next());
            lake.RegisterSingleton("id", () => random.Next());
            lake.RegisterSingleton("id", () => random.Next());

            Assert.IsTrue(lake.GetComponents("id").Count() == 3);
            Assert.IsTrue(lake.GetComponents("id").ElementAt(0).Equals(lake.GetComponents("id").ElementAt(0)));
            Assert.IsTrue(lake.GetComponents("id").ElementAt(1).Equals(lake.GetComponents("id").ElementAt(1)));
            Assert.IsTrue(lake.GetComponents("id").ElementAt(2).Equals(lake.GetComponents("id").ElementAt(2)));
        }
        [TestMethod]
        public void GetComponentsTest()
        {
            SnowLake lake = new SnowLake();
            lake.RegisterComponent("t", () => 0);
            lake.RegisterComponent("t", () => 1);
            lake.RegisterComponent("t", () => 2);
            Assert.IsTrue(lake.GetComponents("t").Count() == 3);

            var components = lake.GetComponents("t");

            Assert.IsTrue((components.ElementAt(0) as int?) == 0);
            Assert.IsTrue((components.ElementAt(1) as int?) == 1);
            Assert.IsTrue((components.ElementAt(2) as int?) == 2);
        }

        [TestMethod]
        public void DefaultComponentTest()
        {
            SnowLake lake = new SnowLake();
            lake.RegisterComponent("t", () => 10);
            lake.RegisterComponent("t", () => 1);
            lake.RegisterComponent("t", () => 2);
            Assert.IsTrue((lake.GetComponent("t") as int?) == 10);
        }
    }
}
