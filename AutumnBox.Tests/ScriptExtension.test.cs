using System;
using AutumnBox.SERT.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutumnBox.Tests
{
    [TestClass]
    public class ScriptExtensionTests
    {
        [TestMethod]
        public void ExcutablePropertyTest()
        {
            var executable = new ScriptExtension("function main(args){ return 2401;}");
            Assert.IsTrue(executable.Executable, "It should be executable");

            var unexecutable = new ScriptExtension("function mainX(args){ return 2401;}");
            Assert.IsTrue(!unexecutable.Executable, "It should be unexecutable");
        }

        [TestMethod]
        public void MainReturnValueTest()
        {
            var se = new ScriptExtension("function main(args){ return 2401;}");
            Assert.IsTrue(se.Execute() == 2401);
        }
    }
}
