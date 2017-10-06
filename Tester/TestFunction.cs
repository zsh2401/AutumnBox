/* =============================================================================*\
*
* Filename: TestFunction.cs
* Description: 
*
* Version: 1.0
* Created: 9/25/2017 07:07:49(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace Tester
{
    using AutumnBox.Basic.Functions;
    using AutumnBox.Basic.Executer;
    public class TestFunction : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            return Executer.AdbExecute("help");
        }
    }
}
