using AutumnBox.SERT.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace AutumnBox.Tests
{
    [TestClass]
    public class ScriptRTTest
    {
        [TestMethod]
        public void VarTest()
        {
            var rt = new ScriptRT("var a = 2401; function main(){a = 2;return 0;}");
            Assert.IsTrue(rt.ValueOf<int>("a") == 2401);
            Assert.IsTrue(rt.Main() == 0);
            Assert.IsTrue(rt.ValueOf<int>("a") == 2);
        }

        [TestMethod]
        public void ATMBNamespaceTest()
        {
            var rt = new ScriptRT("function main(){atmb.logf('atmb namespace exists');return -2401;}");
            Assert.IsTrue(rt.Main() == -2401);
        }

        [TestMethod]
        public void AFXAvailableTest()
        {
            var normalUsing = new ScriptRT("function main(){var ab = new afx.AutumnBox.Basic.Data.OutputBuilder();return -2401;}");
            Assert.IsTrue(normalUsing.Main() == -2401);

            var aliasUsing = new ScriptRT("function main(){new basic.Data.OutputBuilder();return -2402;}");
            Assert.IsTrue(aliasUsing.Main() == -2402);
        }
        [TestMethod]
        public void LoggingFxTest()
        {
            var rt = new ScriptRT("function main(){afx.AutumnBox.Logging.LoggerFactory.Auto('SERTTest').Info('logging test message');return -2401;}");
            Assert.IsTrue(rt.Main() == -2401);
        }
        [TestMethod]
        public void AsyncTest()
        {
            var rt = new ScriptRT("async function main(){return -2401;}");
            Debug.WriteLine(rt.Main());
            //Assert.IsTrue(rt.Main() == -2401);
        }
    }
}
