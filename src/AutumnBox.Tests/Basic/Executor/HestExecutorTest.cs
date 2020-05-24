using AutumnBox.Basic;
using AutumnBox.Basic.Calling;
using AutumnBox.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace AutumnBox.Tests.Basic.Executor
{
    [TestClass]
    public class HestExecutorTest
    {
        public HestExecutorTest()
        {
            BasicBooter.Use<Win32AdbManager>();
        }
        ~HestExecutorTest()
        {
            BasicBooter.Free();
        }

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
