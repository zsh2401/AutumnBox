using AutumnBox.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using AutumnBox.Logging;
using AutumnBox.Tests.Util;
using System;

namespace AutumnBox.Tests.Basic.MADB
{
    [TestClass]
    public class BooterTest : IDisposable
    {
        public BooterTest()
        {
            BasicBooter.Use<Win32AdbManager>();
        }
        public void Dispose()
        {
            BasicBooter.Free();
        }

        [TestMethod]
        public void LoadManager()
        {
            using var cpm = BasicBooter.CommandProcedureManager;
            using var cmd = cpm.OpenCommand("adb", "devices");
            cmd.Execute();
            if (cmd.Status == AutumnBox.Basic.ManagedAdb.CommandDriven.CommandStatus.InnerException)
            {
                Debug.WriteLine(cmd.Exception);
            }
            SLogger<BooterTest>.CDebug(cmd.Result.Output);

            Assert.AreEqual(0,cmd.Result.ExitCode);
            Assert.IsFalse(cmd.Result.Output.Contains("daemon not running"));
            Assert.IsTrue(cmd.Result.Output.Contains("List of devices attached"));
        }
    }
}
