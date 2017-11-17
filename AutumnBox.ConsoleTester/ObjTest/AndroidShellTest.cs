/* =============================================================================*\
*
* Filename: AndroidShellTest
* Description: 
*
* Version: 1.0
* Created: 2017/11/10 19:52:36 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.ObjTest
{
    public class AndroidShellTest
    {
        public static void Run()
        {
            using (var shell = new AndroidShell(Program.mi4))
            {
                shell.Connect();
                shell.OutputReceived += (s, e) => { if (e.Text != "") Console.WriteLine("stdo: " + e.Text); };
                shell.InputReceived += (s, e) => { Console.WriteLine("stdi: " + e.Command); };
                //shell.Switch2Su();
                while (!shell.HasExited)
                {
                    var x = shell.SafetyInput(Console.ReadLine());
                    Program.WriteWithColor(() =>
                    {
                        Console.WriteLine($"line {x.LineAll.Count}");
                        Console.WriteLine("return code: " + x.ReturnCode);
                        Console.WriteLine(x.LineAll.Last());
                    }, ConsoleColor.Red);
                }
                Thread.Sleep(3000);
            }
        }
        public static void RootTest()
        {
            using (var shell = new AndroidShell(Program.mi4))
            {
                shell.Connect();
                shell.OutputReceived += (s, e) => { Console.WriteLine("stdo: " + e.Text); };
                shell.InputReceived += (s, e) => { Console.WriteLine("stdi: " + e.Command); };
                Thread.Sleep(3000);
                Console.WriteLine(shell.Switch2Su());
                shell.Switch2Su();
                Thread.Sleep(3000);
                Console.WriteLine(shell.IsRunningAsSuperuser);
                shell.Switch2Normal();
                Thread.Sleep(3000);
                Console.WriteLine(shell.IsRunningAsSuperuser);
                Thread.Sleep(3000);
                //shell.InputLine("help");
            }
        }
    }
}
