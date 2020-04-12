using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX.Leafx
{
    [TestClass]
    public class NamespaceComponentLoaderTest
    {
        [TestMethod]
        public void MainTest()
        {
            IRegisterableLake lake = new SunsetLake();
            new ClassComponentsLoader("AutumnBox.Tests.OpenFX.Leafx", lake).Do();
            Assert.IsTrue(lake.Get<Fuck>().Yes());
        }
    }
    [Component(Type = typeof(Fuck))]
    public class Fuck
    {
        public bool Yes() => true;
    }
}
