
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Management.ExtInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX
{
    [TestClass]
    public class ExtensionInfoTest
    {
        public ExtensionInfoTest()
        {
            FakeOpenFx.Initialize();
        }
        [TestMethod]
        public void AttributeTest()
        {
            var inf = ClassExtensionInfoCache.Acquire<ETest>();

            Assert.IsFalse(inf.Hidden());
            Assert.AreEqual(inf.RequiredDeviceState(), AutumnBox.Basic.Device.DeviceState.Poweron);

            FakeOpenFx.FakeApi.FakeLanguageCode = "zh-CN";
            Assert.AreEqual(inf.Name(), "你好");

            FakeOpenFx.FakeApi.FakeLanguageCode = "en-US";
            Assert.AreEqual(inf.Name(), "Wow");
        }

        [ExtHidden(false)]
        [ExtRequiredDeviceStates(AutumnBox.Basic.Device.DeviceState.Poweron)]
        [ExtName("Wow", "zh-cn:你好")]
        private class ETest : LeafExtensionBase { }
    }
}
