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

namespace AutumnBox.Tests.Logging
{
    [TestClass]
    public class AoggerTest
    {
        [TestMethod]
        public void NormalTest()
        {
            LoggingManager.Logs.CollectionChanged += (s, e) =>
            {
                var latest = (ILog)e.NewItems[0]!;
                Assert.AreEqual(nameof(AoggerTest), latest.Category);
                Assert.AreEqual(nameof(Aogger.Info), latest.Level);
            };
            Aogger.Info("f");
        }
    }
}
