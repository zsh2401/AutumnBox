using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.Container;

namespace AutumnBox.Tests.OpenFX.Leafx
{
    [TestClass]
    public class ComponentFactoryTest
    {
        private readonly SunsetLake lake;
        public ComponentFactoryTest()
        {
            lake = new SunsetLake();
            var reader = new ComponentFactoryReader(lake, typeof(TestFactory));
            reader.Read();
        }
        [TestMethod]
        public void ByIdSingletonTest()
        {
            Assert.IsTrue((string)lake.Get("clear_dream") == TestFactory.TEST_STR);

        }
        [TestMethod]
        public void ByTypeSingletonTest()
        {
            Assert.IsTrue(lake.Get<StringBuilder>().GetHashCode() == lake.Get<StringBuilder>().GetHashCode());
        }

        [TestMethod]
        public void ByIdMultiTest()
        {
            Assert.IsFalse(lake.Get("new_guid") == lake.Get("new_guid"));
        }

        [TestMethod]
        public void ByTypeMultiTest()
        {
            Assert.IsTrue(lake.Get<int>() - lake.Get<int>() == -1);
        }

        [TestMethod]
        public void PropertyTest()
        {
            Assert.IsTrue(lake.Get("now") == lake.Get("now"));
        }

        private class TestFactory
        {
            public const string TEST_STR = "test str";
            private int randTime = 0;

            [Component(SingletonMode = true, Id = "now")]
            public DateTime Now => DateTime.Now;

            [Component]
            private string StringFactory()
            {
                return TEST_STR;
            }

            [Component(SingletonMode = true, Id = "clear_dream")]
            private string ClearDreamFactory()
            {
                return TEST_STR;
            }

            [Component(SingletonMode = false, Id = "new_guid")]
            private Guid NewGUID()
            {
                return Guid.NewGuid();
            }

            [Component]
            private StringBuilder FSb()
            {
                return new StringBuilder();
            }

            [Component(SingletonMode = false)]
            private int GenerateRandom()
            {
                return randTime++;
            }


        }
    }
}
