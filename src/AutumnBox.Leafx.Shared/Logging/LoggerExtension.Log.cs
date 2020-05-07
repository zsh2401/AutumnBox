/*

* ==============================================================================
*
* Filename: LoggerExtension
* Description: 
*
* Version: 1.0
* Created: 2020/5/7 17:54:34
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Leafx.Logging
{
    public static partial class LoggerExtension
    {
        [Conditional("DEBUG")]
        public static void Debug(this ILogger logger, object content)
        {
            throw new NotImplementedException();
        }
        [Conditional("DEBUG")]
        public static void Debug(this ILogger logger, object content, Exception e)
        {
            throw new NotImplementedException();
        }

        public static void Info(this ILogger logger, object content)
        {
            throw new NotImplementedException();
        }
        public static void Warn(this ILogger logger, object content)
        {
            throw new NotImplementedException();
        }
        public static void Warn(this ILogger logger, object content, Exception e)
        {
            throw new NotImplementedException();
        }
        public static void Fatal(this ILogger logger, object content, Exception e)
        {
            throw new NotImplementedException();
        }
        public static void Exception(this ILogger logger, object content, Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
