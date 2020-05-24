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
using AutumnBox.Logging.Internal;
using AutumnBox.Logging.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Tests.Logging
{
    public static class LoggerTestUnit
    {
        public static async Task Writing(this ICoreLogger logger, int taskId, int maxTime)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < maxTime; i++)
                {
                    logger.Log(new Log("Test", "Test Task", "Test Message"));
                }
            });
        }
    }
}
