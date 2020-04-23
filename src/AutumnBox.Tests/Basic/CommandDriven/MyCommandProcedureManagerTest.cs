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
            if (result.ExitCode != 0)
            {
                Debug.WriteLine(result.Output);
                Debug.WriteLine(result.ExitCode);
                Debug.WriteLine(command.Exception);
            }
            Assert.IsTrue(result.ExitCode == 0);



            using var command127 = cpm.OpenCommand("cmd.exe", "/c", "fuck.exe");
            var result127 = command127.Execute();
            if (result127.ExitCode != 0)
            {
                Debug.WriteLine(result127.Output);
                Debug.WriteLine(result127.ExitCode);
                Debug.WriteLine(command127.Exception);
            }
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
            (cp as CommandProcedure).DirectExecute = true;
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

            using var command1 = cpm.OpenCommand("echo", "%ANDROID_ADB_SERVER_PORT%");
            var result = command1.Execute();
            Debug.WriteLine(result.Output);
            Debug.WriteLine(result.ExitCode);
            Debug.WriteLine(command1.Exception);
            Assert.IsTrue(result.Output.Contains(randomPort.ToString()));

            using var command2 = cpm.OpenCommand("echo", "%PATH%");
            Assert.IsTrue(command2.Execute().Output.Contains(randomStr));
        }
    }
}
