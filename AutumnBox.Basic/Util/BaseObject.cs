/* =============================================================================*\
*
* Filename: BaseObject.cs
* Description: 
*
* Version: 1.0
* Created: 9/9/2017 16:39:28(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Util
{
    using System;
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseObject : Object
    {
        protected string TAG;
        protected BaseObject() { this.TAG = this.GetType().Name; }
        protected void LogD(string message) { Logger.D(TAG, message); }
        protected void LogT(string message) { Logger.T(TAG, message); }
        protected void LogE(string message, Exception e, bool showInTrace = true)
        {
            Logger.E(TAG, message, e, true);
        }
    }
}
