using AutumnBox.Basic.ManagedAdb.CommandDriven;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.Basic.CommandDriven
{
    [TestClass]
    public class MyCommandProcedureManagerTest
    {
        [TestMethod]
        public void ExitCodeTest()
        {
            var cpm = new ProcedureManager();
            using var command = cpm.OpenCommand("cmd.exe", "/c", "ping baidu.com");
            var result = command.Execute();
            Assert.IsTrue(result.ExitCode == 0);

            using var command127 = cpm.OpenCommand("cmd.exe", "/c", "fuck.exe");
            var result127 = command127.Execute();
            Assert.IsTrue(result127.ExitCode == 1);
        }

        [TestMethod]
        public void OutputTest()
        {
            bool outputReceived = false;
            var cpm = new ProcedureManager();
            using var command = cpm.OpenCommand("cmd.exe", "/c", "ping baidu.com");
            command.OutputReceived += (s, e) =>
            {
                outputReceived = true;
            };
            var result = command.Execute();
            Assert.IsTrue(outputReceived);
            Debug.WriteLine(result.Output);
            Assert.IsTrue(result.Output.Contains("Pinging"));
        }

        [TestMethod]
        public void CommandExceptionTest()
        {
            using var cpm = new ProcedureManager();
            using var cp = cpm.OpenCommand("asdasdsa");
            cp.Execute();
            Assert.IsTrue(cp.Status == CommandStatus.Failed);
            Assert.IsTrue(cp.Exception.GetType() == typeof(Win32Exception));
        }

        [TestMethod]
        public void EnvVarTest()
        {
            string randomStr = Guid.NewGuid().ToString();
            ushort randomPort = (ushort)new Random().Next(0, ushort.MaxValue);
            using var cpm = new ProcedureManager(new DirectoryInfo(randomStr), randomPort);

            using var command1 = cpm.OpenCommand("cmd.exe", "/c", "echo %ANDROID_ADB_SERVER_PORT%");
            Assert.IsTrue(command1.Execute().Output.Contains(randomPort.ToString()));

            using var command2 = cpm.OpenCommand("cmd.exe", "/c", "echo %PATH%");
            Assert.IsTrue(command2.Execute().Output.Contains(randomStr));
        }
    }
}
