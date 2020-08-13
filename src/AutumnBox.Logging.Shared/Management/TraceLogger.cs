/*

* ==============================================================================
*
* Filename: TraceLogger
* Description: 
*
* Version: 1.0
* Created: 2020/8/14 1:44:55
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System.Diagnostics;

namespace AutumnBox.Logging.Management
{
    public class TraceLogger : CoreLoggerBase
    {
        public void Dispose()
        {
            //pass
        }

        public override void Log(ILog log)
        {
            Trace.WriteLine(log.ToFormatedString());
        }
    }
}
