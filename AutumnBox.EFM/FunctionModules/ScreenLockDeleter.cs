/* =============================================================================*\
*
* Filename: ScreenLockDeleter.cs
* Description: 
*
* Version: 1.0
* Created: 9/22/2017 03:13:12(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions.FunctionModulesExp
{
    public sealed class ScreenLockDeleter : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            Ae("root");
            Ae("shell rm /data/system/gesture.key");
            Ae("adb shell rm /data/system/password.key");
            return new OutputData();
        }
    }
}
