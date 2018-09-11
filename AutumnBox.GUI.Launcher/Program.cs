using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Linq;
using System.ComponentModel;
using System.IO;

namespace AutumnBox.GUI.Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run(args);
        }

        private ConsoleColor defaultConsoleForeColor;
        private void Run(string[] args)
        {
            defaultConsoleForeColor = Console.ForegroundColor;
            Print("Init...");
            var startInfo = new ProcessStartInfo()
            {
                FileName = "AutumnBox.GUI.exe",
                WorkingDirectory = "file",
            };
            if (args.Contains("-tryadmin"))
            {
                startInfo.Verb = "runas";
            }
            try
            {
                if (args.Contains("-waitfor"))
                {
                    var pidIndex = args.ToList().IndexOf("-waitfor") + 1;
                    var pid = int.Parse(args[pidIndex]);
                    Print($"Waiting {pid}...");
                    Process.GetProcessById(pid).WaitForExit();
                }
                else if (args.Contains("-waitatmb"))
                {
                    var proc = Process.GetProcessesByName("AutumnBox.GUI")[0];
                    Print($"Waiting {proc.Id}...");
                    proc.WaitForExit();
                }
            }
            catch { }
            
            try {
                Print("AutumnBox.GUI starts at now!");
                Process.Start(startInfo);
            }
            catch (Win32Exception)
            {
                Print("找不到文件!请将整个秋之盒解压后再使用!");
                Print("File not found!Please uncompresse all files!");
                Console.ReadKey();
            }

        }
        private void Print(object content, ConsoleColor? fore = null)
        {
            Console.ForegroundColor = fore ?? defaultConsoleForeColor;
            Console.WriteLine(content);
            Console.ForegroundColor = defaultConsoleForeColor;
        }
        private bool IsRunAsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
