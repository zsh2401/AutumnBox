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
                shell.BlockLastCommand = false;
                shell.BlockEmptyOutput = false;
                shell.BlockNullOutput = false;
                shell.Switch2Superuser();
                while (!shell.HasExited)
                {
                    shell.Input(Console.ReadLine());
                }
                Thread.Sleep(3000);
                //shell.InputLine("help");
            }
        }
        public static void RootTest()
        {
            using (var shell = new AndroidShell(Program.mi4))
            {
                shell.Connect();
                shell.OutputReceived += (s, e) => { Console.WriteLine("stdo: " + e.Text); };
                shell.BlockLastCommand = false;
                shell.InputReceived += (s, e) => { Console.WriteLine("stdi: " + e.Command); };
                Thread.Sleep(3000);
                Console.WriteLine(shell.IsRuningAsSuperuser);
                shell.Switch2Superuser();
                Thread.Sleep(3000);
                Console.WriteLine(shell.IsRuningAsSuperuser);
                shell.Switch2Normaluser();
                Thread.Sleep(3000);
                Console.WriteLine(shell.IsRuningAsSuperuser);
                Thread.Sleep(3000);
                //shell.InputLine("help");
            }
        }
    }
}
