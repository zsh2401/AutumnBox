/* =============================================================================*\
*
* Filename: AsyncAwaitTest
* Description: 
*
* Version: 1.0
* Created: 2017/11/18 18:29:19 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.MethodTest
{
    public static class AsyncAwaitTest
    {
        public static async void DoAsync()
        {
            Console.WriteLine("start");
            int fuck = await GetResult();
            Console.WriteLine("maybe finish?");
        }
        public static Task<int> GetResult()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(3000);
                return 1;
            });
        }
    }
}
