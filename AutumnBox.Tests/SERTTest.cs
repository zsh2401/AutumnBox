using AutumnBox.SERT.Shared.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace AutumnBox.Tests
{
    [TestClass]
    public class SERTTest
    {
        [TestMethod]
        public void EvaluateTest()
        {
            var rt = new ScriptRT("function main(){atmb.log('Well');}");

            rt.Main();
        }
    }
}
