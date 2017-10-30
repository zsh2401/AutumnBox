/* =============================================================================*\
*
* Filename: StackTest
* Description: 
*
* Version: 1.0
* Created: 2017/10/31 2:11:36 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Shared.CstmDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.MethodTest
{
    public static class StackTest
    {
        [LogProperty(TAG = "fuck")]
        public static void A()
        {
            AB();
        }
        public static void B()
        {
            var attrs = new StackTrace().GetFrames()[2].GetMethod().GetCustomAttributes(true);
            var a = from attr in attrs
                    where attr is LogPropertyAttribute
                    select attr;
            if (a != null)
            {
                Console.WriteLine(((LogPropertyAttribute)a.First()).TAG);
            }
        }
        public static void AB() {
            B();
        }
    }
}
