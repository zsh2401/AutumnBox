
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Management.ExtInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX.ExtInf
{
    [TestClass]
    public class ExtInfExtensionTest
    {
        [TestMethod]
        public void AttributeTest()
        {
            var extInf = ClassExtensionInfoCache.Acquire(typeof(ETest));
            Assert.IsFalse(extInf.Hidden());
            Assert.IsTrue(extInf.Name() == "Wow");
        }
        [ExtHidden(false)]
        [ExtName("Wow")]
        private class ETest : LeafExtensionBase
        {

        }
    }

}
