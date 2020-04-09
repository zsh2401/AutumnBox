using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.OpenFX.LeafExtension
{
    [TestClass]
    public class LeafExtensionTest
    {
        [TestMethod]
        public void EntryPointFindingTest()
        {
            LeafExtensionBase leaf1 = new EEntryPointFindingTest1();
            var result1 = leaf1.Main(new Dictionary<string, object>());
            Assert.IsTrue((string)result1 == EEntryPointFindingTest1.TEST_RESULT);

            LeafExtensionBase leaf2 = new EEntryPointFindingTest2();
            var result2 = leaf2.Main(new Dictionary<string, object>());
            Assert.IsTrue((string)result2 == EEntryPointFindingTest2.TEST_RESULT);

            LeafExtensionBase leaf3 = new EEntryPointFindingTest3();
            var result3 = leaf3.Main(new Dictionary<string, object>());
            Assert.IsTrue((string)result3 == EEntryPointFindingTest3.TEST_RESULT);
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
