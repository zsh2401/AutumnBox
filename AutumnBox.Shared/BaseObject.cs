/* =============================================================================*\
*
* Filename: BaseObject
* Description: 
*
* Version: 1.0
* Created: 2017/10/16 14:06:37(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.SharedTools
{
    public class BaseObject:Object
    {
        protected string TAG;
        protected BaseObject() { this.TAG = this.GetType().Name; }
        protected void LogD(string message) { Logger.D(TAG, message); }
        protected void LogT(string message) { Logger.T(TAG, message); }
        protected void LogE(string message, Exception e, bool showInTrace = true)
        {
            Logger.E(TAG, message, e, true);
        }
        protected void 
    }
}
