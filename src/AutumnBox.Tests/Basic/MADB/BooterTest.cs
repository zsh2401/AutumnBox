using AutumnBox.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using AutumnBox.Logging;
using AutumnBox.Tests.Util;

namespace AutumnBox.Tests.Basic.MADB
{
    [TestClass]
    public class BooterTest
    {
        [TestMethod]
        public void LoadManager()
        {
            BasicBooter.Use<Win32AdbManager>();

            using var cpm = BasicBooter.CommandProcedureManager;
            using var cmd = cpm.OpenCommand("adb", "devices");
            cmd.Execute();
            if (cmd.Status == AutumnBox.Basic.ManagedAdb.CommandDriven.CommandStatus.Failed)
            {
                Debug.WriteLine(cmd.Exception);
            }
            SLogger<BooterTest>.CDebug(cmd.Result.Output);

            Assert.IsTrue(cmd.Result.ExitCode == 0);
            Assert.IsFalse(cmd.Result.Output.Contains("daemon not running"));
            Assert.IsTrue(cmd.Result.Output.Contains("List of devices attached"));

            BasicBooter.Free();
        }
    }
}
