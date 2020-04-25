/*

* ==============================================================================
*
* Filename: LoggerTest
* Description: 
*
* Version: 1.0
* Created: 2020/4/24 18:50:14
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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.Logging
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void BufferedFsCoreLoggerSpeedTest()
        {
            LoggingManager.Use<BufferedFSCoreLogger>(false);
            const int times = 1_000_000;
            Task.WaitAll(Writing(1, times), Writing(1, times), Writing(1, times));
        }

        [TestMethod]
        public void FsCoreLoggerSpeedTest()
        {
            //LoggingManager.Use<FSCoreLogger>(false);
            const int times = 1_000_000;
            Task.WaitAll(Writing(1, times), Writing(2, times), Writing(3, times));
        }

        public static async Task Writing(int taskId, int maxTime)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < maxTime; i++)
                {
                    SLogger<LoggerTest>.Info($"{taskId}/{i}");
                }
            });
        }
    }
}
