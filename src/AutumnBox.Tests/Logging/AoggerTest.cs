/*

* ==============================================================================
*
* Filename: AoggerTest
* Description: 
*
* Version: 1.0
* Created: 2020/8/18 10:43:57
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Logging;
using AutumnBox.Logging.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AutumnBox.Tests.Logging
{
    [TestClass]
    public class AoggerTest : ICoreLogger
    {
        public void Dispose()
        {
            LoggingManager.Use(_defaultCoreLogger);
        }

        private ICoreLogger _defaultCoreLogger;

        public AoggerTest()
        {
            _defaultCoreLogger = LoggingManager.CoreLogger;
            LoggingManager.Use(this);
        }

        [TestMethod]
        public void Info()
        {
            Aogger.Info("f");
            var last = logs.Last();
            Assert.AreEqual(nameof(AoggerTest), last.Category);
            Assert.AreEqual(nameof(Aogger.Info), last.Level);
        }

        private readonly List<ILog> logs = new List<ILog>();
        public void Log(ILog log)
        {
            logs.Add(log);
            Debug.WriteLine(log.ToFormatedString());
        }
    }
}
