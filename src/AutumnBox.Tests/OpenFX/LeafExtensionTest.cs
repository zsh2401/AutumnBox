using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Extension.Leaf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AutumnBox.Tests.OpenFX
{
    [TestClass]
    public class LeafExtensionTest
    {
        [TestMethod]
        public void FindEntryPoint()
        {
            IRegisterableLake lake = new SunsetLake();

            {
                var leaf = lake.CreateInstance<EEntryPointFindingTest1>();
                var result = leaf.Main(new Dictionary<string, object>());
                Assert.AreEqual(result, EEntryPointFindingTest1.TEST_RESULT);
            }
            {
                var leaf = lake.CreateInstance<EEntryPointFindingTest2>();
                var result = leaf.Main(new Dictionary<string, object>());
                Assert.AreEqual(result, EEntryPointFindingTest2.TEST_RESULT);
            }
            {
                var leaf = lake.CreateInstance<EEntryPointFindingTest3>();
                var result = leaf.Main(new Dictionary<string, object>());
                Assert.AreEqual(result, EEntryPointFindingTest3.TEST_RESULT);
            }
        }
        private class EEntryPointFindingTest1 : LeafExtensionBase
        {
            public const string TEST_RESULT = "I Love Autumn!";
            [LMain]
            public string EntryPoint()
            {
                return TEST_RESULT;
            }
        }
        private class EEntryPointFindingTest2 : LeafExtensionBase
        {
            public const string TEST_RESULT = "I Love Autumn!";

            public string LMain()
            {
                return TEST_RESULT;
            }
        }

        private class EEntryPointFindingTest3 : LeafExtensionBase
        {
            public const string TEST_RESULT = "I Love Autumn!";

            public string Main()
            {
                return TEST_RESULT;
            }
        }
    }
}
