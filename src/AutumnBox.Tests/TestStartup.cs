/*

* ==============================================================================
*
* Filename: TestStartup
* Description: 
*
* Version: 1.0
* Created: 2020/5/24 16:26:10
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.Basic;
using AutumnBox.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutumnBox.Tests
{
    class TestStartup
    {
        [AssemblyInitialize]
        public static void Startup(TestContext context)
        {
            //BasicBooter.Use<Win32AdbManager>();
        }
        [AssemblyCleanup]
        public static void Cleanup()
        {
            //BasicBooter.Free();
        }
    }
}
