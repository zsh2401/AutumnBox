using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Launcher
{
    class Program
    {

        static void Main(string[] args)
        {
            new Program().Run();
        }
        private ConsoleColor defaultConsoleForeColor;
        private void Run()
        {
            Print("Init...");
            defaultConsoleForeColor = Console.ForegroundColor;
            var startInfo = new ProcessStartInfo()
            {
                FileName = "AutumnBox.GUI.exe",
                WorkingDirectory = "file",
                UseShellExecute = true,
                Verb = "runas"
            };
            Print("AutumnBox.GUI starts at now!");
            Process.Start(startInfo);
        }

        private void Print(object content, ConsoleColor? fore = null)
        {
            Console.ForegroundColor = fore ?? defaultConsoleForeColor;
            Console.WriteLine(content);
            Console.ForegroundColor = defaultConsoleForeColor;
        }
    }
}
