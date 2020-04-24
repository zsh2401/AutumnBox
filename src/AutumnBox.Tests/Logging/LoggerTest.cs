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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
        public void PerformanceTest()
        {
            Stopwatch fisrtCallSW = new Stopwatch();
            fisrtCallSW.Start();
            SLogger<LoggerTest>.Info("First call");
            fisrtCallSW.Stop();
            SLogger<LoggerTest>.Info($"first call used time: {fisrtCallSW.ElapsedMilliseconds}ms");

            var sw = new Stopwatch();
            sw.Start();
            Task.WaitAll(
               Task.Run(WriteLog),
               Task.Run(WriteLog),
               Task.Run(WriteLog));
            sw.Stop();
            Debug.WriteLine("used " + sw.ElapsedMilliseconds + "ms");
        }
        int taskId = 0;
        private const int time = 10000;
        void WriteLog()
        {
            int mytask = taskId++;
            for (int i = 0; i < time; i++)
            {
                SLogger<LoggerTest>.Info($"{mytask}/{i++}");
                SLogger<LoggerTest>.Info($"{mytask}/{i}");
            }
        }
    }
}
