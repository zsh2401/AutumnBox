using AutumnBox.Basic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Tests.Basic.MADB
{
    [TestClass]
    public class Win32ManagerTest
    {
        [TestMethod]
        public void DisposeTest()
        {
            bool cpmDisposed = false;
            bool cmdDisposed = false;
            //BasicBooter.Use<Win32AdbManager>();
            BasicBooter.CommandProcedureManager.Disposed += (s, e) => cpmDisposed = true;
            using var cmd = BasicBooter.CommandProcedureManager.OpenCommand("cmd.exe");
            cmd.Disposed += (s, e) => cmdDisposed = true;
            var task = cmd.ExecuteAsync();

            Thread.Sleep(1000);
            BasicBooter.Free();
            Thread.Sleep(1000);

            Assert.IsTrue(cpmDisposed);
            Assert.IsTrue(cmdDisposed);
            Assert.IsFalse(task.Status == TaskStatus.Running);
        }
    }
}
