/* =============================================================================*\
*
* Filename: LogFilePropAttribute
* Description: 
*
* Version: 1.0
* Created: 2017/10/28 16:32:44 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Shared.CstmDebug
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class LogFilePropertyAttribute : Attribute
    {
        public string FileName { get; private set; }
        public LogFilePropertyAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}
