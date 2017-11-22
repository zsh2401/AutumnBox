/* =============================================================================*\
*
* Filename: ExecuterInOrder
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 0:56:16 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public static class ExecuterInOrder
    {
        private struct ShellCommand
        {
            public string Id { get; set; }
            public string Command { get; set; }
            public Action<ShellOutput> Handler { get; set; }
        }
        private struct NormalCommand
        {
            public Command Command { get; set; }
            public Action<OutputData> Handler { get; set; }
        }
        private static readonly CExecuter executer;
        private static Dictionary<string, NormalCommand> commands;
        private static Dictionary<string, ShellCommand> shellCommands;
        static ExecuterInOrder()
        {
            executer = new CExecuter();
            commands = new Dictionary<string, NormalCommand>();
            shellCommands = new Dictionary<string, ShellCommand>();
            Start();
        }
        public static void ClearCommands()
        {
            commands.Clear();
            shellCommands.Clear();
        }
        public static void AddCommand(Command command, Action<OutputData> finishedHandler)
        {
            commands.Add(GetMarkcode(), new NormalCommand() { Command = command, Handler = finishedHandler });
        }
        public static void AddShellCommand(string id, string shellCommand, Action<ShellOutput> finishedHandler)
        {
            shellCommands.Add(GetMarkcode(), new ShellCommand() { Id = id, Command = shellCommand, Handler = finishedHandler });
        }
        public static void Stop()
        {
            _continue = false;
            IsRunning = false;
        }
        public static bool IsRunning { get; private set; } = false;
        public static void Start()
        {
            if (IsRunning) return;
            _continue = true;
            Task.Run(() =>
            {
                IsRunning = true;
                MainMethod();
            });
        }
        private static bool _continue = true;
        private static Random ran = new Random();
        private static void MainMethod()
        {
            while (_continue)
            {
                Thread.Sleep(1000);
                try
                {
                    if (ran.Next() % 2 == 0)
                    {
                        var cmd = commands.First();
                        var o = executer.Execute(cmd.Value.Command);
                        cmd.Value.Handler?.Invoke(o);
                        commands.Remove(cmd.Key);
                    }
                    else
                    {
                        var cmd = shellCommands.First();
                        var so = executer.QuicklyShell(cmd.Value.Id, cmd.Value.Command);
                        cmd.Value.Handler?.Invoke(so);
                        shellCommands.Remove(cmd.Key);
                    }
                }
                catch (InvalidOperationException e)
                {
                }
            }
        }
        private static char[] allcheckRandom ={'0','1','2','3','4','5','6','7','8','9',
                                       'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W',
                                       'X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q',
                                       'r','s','t','u','v','w','x','y','z'};
        private static int maxExtLength = 15;
        public static string GetMarkcode()
        {
            string result = DateTime.Now.ToString("yyyyMMddHHmmss");
            for (int length = ran.Next(0, maxExtLength); length >= 0; length--)
            {
                result += allcheckRandom[ran.Next(0, 61)];
            }
            return result;
            //for (int i = 0; i < < span style = "color: #FF0000;" > 4 </ span >; i++)
            //{
            //    Randomcode += allcheckRandom[rom.Next(allcheckRandom.Length)];
            //    Session["verty"] = Randomcode;
            //}
            //Session["CheckCode"] = Randomcode;

        }
    }
}
