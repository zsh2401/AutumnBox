using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutumnBox.Tests.OpenFX.Leafx
{
    [TestClass]
    public class ObjectInjectorTest
    {

        [TestMethod]
        public void InjectById()
        {
            SunsetLake lake = new SunsetLake();
            lake.Register("time_now", () => DateTime.Now);

            var instance = new InjectTarget();
            var originalDateTime = instance.CreateDateTime;
            new DependenciesInjector(instance, lake).Inject();
            Assert.IsTrue(instance.CreateDateTime != originalDateTime);
        }

        [TestMethod]
        public void IntelliInject()
        {
            SunsetLake lake = new SunsetLake();
            lake.Register<DateTime>(() => DateTime.Now);

            var instance = new InjectTarget();
            var originalDateTime = instance.CreateDateTime;
            new DependenciesInjector(instance, lake).Inject();
            Assert.IsTrue(instance.CreateDateTime != originalDateTime);
        }

        [TestMethod]
        public void ByTypeInject()
        {
            const string TEST_STR = "test string";
            SunsetLake lake = new SunsetLake();
            lake.RegisterSingleton<string>(TEST_STR);

            var instance = new InjectTarget();
            new DependenciesInjector(instance, lake).Inject();
            Assert.IsTrue(instance.Str == TEST_STR);
        }

        [TestMethod]
        public void FieldTest()
        {
            SunsetLake lake = new SunsetLake();
            lake.RegisterSingleton(FieldInjectTarget.TEST_STR_ID, () => FieldInjectTarget.TEST_STR);
            lake.RegisterSingleton<FieldInjectTarget, FieldInjectTarget>();
            var target = lake.Get<FieldInjectTarget>();
            Assert.IsTrue(target.IsAllRight());
        }

        private class FieldInjectTarget
        {
            public const string TEST_STR = "test str..";
            public const string TEST_STR_ID = "test_str";

            [AutoInject(Id = TEST_STR_ID)]
            private readonly string privateReadonlyTestStr;

            [AutoInject(Id = TEST_STR_ID)]
            private string privateTestStr;

            [AutoInject(Id = TEST_STR_ID)]
            public string publicTestStr;

            [AutoInject(Id = TEST_STR_ID)]
            public readonly string publicReadonlyTestStr;

            public bool IsAllRight()
            {
                return (privateReadonlyTestStr == TEST_STR) &&
                    (privateTestStr == TEST_STR) &&
                    (publicTestStr == TEST_STR) &&
                    (publicReadonlyTestStr == TEST_STR);
            }
        }

        private class InjectTarget
        {
            [AutoInject(Id = "time_now")]
            public DateTime CreateDateTime { get; set; }

            [AutoInject]
            public string Str { get; set; }
        }
    }
}
