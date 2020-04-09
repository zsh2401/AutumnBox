using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.OpenFramework.Leafx.Container.Internal;
using AutumnBox.OpenFramework.Leafx.ObjectManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutumnBox.OpenFramework.Leafx.Container;
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
            new PropertyInjector(instance, lake).Inject();
            Assert.IsTrue(instance.CreateDateTime != originalDateTime);
        }

        [TestMethod]
        public void IntelliInject()
        {

            SunsetLake lake = new SunsetLake();
            lake.Register<DateTime>(() => DateTime.Now);

            var instance = new InjectTarget();
            var originalDateTime = instance.CreateDateTime;
            new PropertyInjector(instance, lake).Inject();
            Assert.IsTrue(instance.CreateDateTime != originalDateTime);
        }

        [TestMethod]
        public void ByTypeInject() {
            const string TEST_STR = "test string";
            SunsetLake lake = new SunsetLake();
            lake.RegisterSingleton<string>(TEST_STR);

            var instance = new InjectTarget();
            new PropertyInjector(instance, lake).Inject();
            Assert.IsTrue(instance.Str == TEST_STR);
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
