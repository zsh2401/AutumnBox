/*

* ==============================================================================
*
* Filename: Aogger
* Description: 
*
* Version: 1.0
* Created: 2020/8/18 10:38:22
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Logging.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Logging
{
    /// <summary>
    /// 自动化获取调用者信息
    /// </summary>
    public static class Aogger
    {
        public static void WriteToLog(this Exception e, object? additionMessage = null)
        {
            SLogger.Warn(CallerQuerier.Get(2).TypeName, additionMessage ?? String.Empty, e);
        }
        public static void Info(object message)
        {
            SLogger.Info(CallerQuerier.Get(2).TypeName, message);
        }
        public static void Warn(object message)
        {
            SLogger.Warn(CallerQuerier.Get(2).TypeName, message);
        }
        public static void Exception(Exception e)
        {
            SLogger.Exception(CallerQuerier.Get(2).TypeName, e);
        }
    }
}
