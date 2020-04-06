using AutumnBox.OpenFramework.Leafx;
using AutumnBox.OpenFramework.Leafx.Container;
using AutumnBox.OpenFramework.Leafx.Container.Internal;
using AutumnBox.OpenFramework.Leafx.ObjectManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            lake.RegisterSingleton<string>(TEST_STR);

            MethodProxy maxProxy = new MethodProxy(this, nameof(HashCodeAdd), lake);
            long result = (long)maxProxy.Invoke(new Dictionary<string, object>() { { "x", 3 } });
            long correctResult = HashCodeAdd(SUM, TEST_STR);

            Assert.IsTrue(correctResult == result);
        }
    }
}
