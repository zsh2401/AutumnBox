using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AutumnBox.Tests.OpenFX.Leafx
{
    [TestClass]
    public class MethodProxyTest
    {
        [TestMethod]
        public void CustomArgsTest()
        {
            MethodProxy maxProxy = new MethodProxy(this, nameof(Max));
            var args = new Dictionary<string, object>() { { "x", 1 }, { "y", 2 } };
            int result = (int)maxProxy.Invoke(args);
            Assert.IsTrue(result == 2);
        }

        private int Max(int x, int y)
        {
            return x > y ? x : y;
        }
        private long HashCodeAdd(int x, string text)
        {
            return x.GetHashCode() + text.GetHashCode();
        }

        [TestMethod]
        public void MixArgsTest()
        {
            const string TEST_STR = "test string";
            const int SUM = 3;
            IRegisterableLake lake = new SunsetLake();
            lake.RegisterSingleton<string>("fuck");

            IRegisterableLake lake2 = new SunsetLake();
            lake.RegisterSingleton<string>(TEST_STR);

            MethodProxy maxProxy = new MethodProxy(this, nameof(HashCodeAdd), lake,lake2);
            long result = (long)maxProxy.Invoke(new Dictionary<string, object>() { { "x", 3 } });
            long correctResult = HashCodeAdd(SUM, TEST_STR);

            Assert.IsTrue(correctResult == result);
        }
    }
}
