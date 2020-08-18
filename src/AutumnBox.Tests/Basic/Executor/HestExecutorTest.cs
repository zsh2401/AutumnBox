using AutumnBox.Basic;
using AutumnBox.Basic.Calling;
using AutumnBox.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace AutumnBox.Tests.Basic.Executor
{
    [TestClass]
    public class HestExecutorTest : IDisposable
    {
        public HestExecutorTest()
        {
            BasicBooter.Use<Win32AdbManager>();
        }
        public void Dispose()
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
                Debug.WriteLine(e.Text);
            };
            var result = executor.Cmd("ping baidu.com");
            Assert.IsTrue(outputReceived);
            Assert.IsTrue(result.Output.Contains("Pinging"));
        }
    }
}
