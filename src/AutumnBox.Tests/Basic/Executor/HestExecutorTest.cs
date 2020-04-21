using AutumnBox.Basic.Calling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.Basic.Executor
{
    [TestClass]
    public class HestExecutorTest
    {
        [TestMethod]
        public void PingTest()
        {
            bool outputReceived = false;
            var executor = new HestExecutor();
            executor.OutputReceived += (s, e) =>
            {
                outputReceived = true;
            };
            var result = executor.Cmd("ping baidu.com");
            Assert.IsTrue(outputReceived);
            Assert.IsTrue(result.Output.Contains("Pinging"));
            Debug.WriteLine(result.Output);
        }
    }
}
