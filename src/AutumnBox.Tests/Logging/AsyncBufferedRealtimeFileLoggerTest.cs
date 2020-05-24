/*

* ==============================================================================
*
* Filename: AsyncBufferedRealtimeFileLoggerTest
* Description: 
*
* Version: 1.0
* Created: 2020/5/24 17:16:28
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Logging.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.Logging
{
    /// <summary>
    /// 暂时不参与测试
    /// </summary>
    [TestClass]
    class AsyncBufferedRealtimeFileLoggerTest
    {
        [TestMethod]
        public void HighConcurrentlyTest()
        {
            using var fs = new FileStream(new Guid().ToString(), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using var logger = new AsyncBufferedRealtimeFileLogger(fs);
            const int times = 1_000_000;
            Task.WaitAll(logger.Writing(1, times), logger.Writing(2, times), logger.Writing(3, times));

            using var sr = new StreamReader(fs);
            Assert.AreEqual(sr.ReadToEnd().Split(Environment.NewLine).Count(), times * 3);
        }
    }
}
