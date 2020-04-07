using AutumnBox.OpenFramework.Leafx;
using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.OpenFramework.Leafx.Container;
using AutumnBox.OpenFramework.Leafx.Container.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutumnBox.Tests.OpenFX.Leafx
{
    [TestClass]
    public class SunsetLakeTest
    {
        [TestMethod]
        public void RegisterSingletonTest()
        {
            IRegisterableLake lake = new SunsetLake();
            lake.RegisterSingleton(typeof(int), () => 1);
            lake.RegisterSingleton<string>("test string");

            Assert.IsTrue(lake.Get<int>() == 1);
            Assert.IsTrue(lake.Get<string>() == "test string");
        }
        [TestMethod]
        public void RegisterTest()
        {
            IRegisterableLake lake = new SunsetLake();
            int time = 0;

            lake.Register<int>(() => time++);

            Assert.IsTrue((lake.Get<int>() - lake.Get<int>()) == -1);
        }
        [TestMethod]
        public void ObjectBuildTest()
        {
            const int sum = 5;
            const string str = "test string";
            IRegisterableLake lake = new SunsetLake();

            lake.RegisterSingleton<int>(sum);
            lake.RegisterSingleton<string>(str);
            lake.RegisterSingleton<ITestInterface, TestClass>();

            ITestInterface testInterfaceImpl = lake.Get<ITestInterface>();

            Assert.IsTrue(testInterfaceImpl.Str == str);
            Assert.IsTrue(testInterfaceImpl.Sum == sum);
        }

        [TestMethod]
        public void InjectingTest()
        {
            IRegisterableLake lake = new SunsetLake();
            const int TEST_SUM = 50;
            const string TEST_STR = "test str";
            lake.RegisterSingleton("sum", () => TEST_SUM);
            lake.RegisterSingleton<string>(TEST_STR);
            lake.RegisterSingleton<InjectTestClass, InjectTestClass>();
            var testClass = lake.Get<InjectTestClass>();

            Assert.IsTrue(testClass.Sum == TEST_SUM);
            Assert.IsTrue(testClass.Str == TEST_STR);
        }
        private interface ITestInterface
        {
            string Str { get; }
            int Sum { get; }
        }
        private class InjectTestClass
        {
            public string Str => _str;

            [AutoInject]
            private string _str { get; set; }

            [AutoInject(Id = "sum")]
            public int Sum { get; set; }
        }
        private class TestClass : ITestInterface
        {
            public TestClass(string str, int sum)
            {
                Str = str;
                Sum = sum;
            }


            public string Str { get; }
            public int Sum { get; }
        }
    }
}
