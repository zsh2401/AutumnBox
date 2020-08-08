/*

* ==============================================================================
*
* Filename: ConsoleLogger
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 4:01:04
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 异步地向控制台输出内容
    /// </summary>
    public class ConsoleLogger : CoreLoggerBase
    {
        private readonly bool _async = true;

        public ConsoleLogger(bool async = true)
        {
            _async = async;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="log"></param>
        public override void Log(ILog log)
        {
            if (_async)
            {
                Task.Run(() =>
                {
                    Trace.WriteLine(log.ToFormatedString());
                });
            }
            else
            {
                Trace.WriteLine(log.ToFormatedString());
            }
        }
    }
}
