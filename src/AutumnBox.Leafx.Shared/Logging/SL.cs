/*

* ==============================================================================
*
* Filename: SL
* Description: 
*
* Version: 1.0
* Created: 2020/5/7 20:12:58
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutumnBox.Leafx.Logging
{
    public static class SL<TSender>
    {
        [Conditional("DEBUG")]
        public static void Debug(object content)
        {
            throw new NotImplementedException();
        }

        [Conditional("DEBUG")]
        public static void Debug(object content, Exception e)
        {
            throw new NotImplementedException();
        }

        public static void Info(object content)
        {
            throw new NotImplementedException();
        }
        public static void Warn(object content)
        {
            throw new NotImplementedException();
        }
        public static void Warn(object content, Exception e)
        {
            throw new NotImplementedException();
        }
        public static void Fatal(object content, Exception e)
        {
            throw new NotImplementedException();
        }
        public static void Exception(object content, Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
