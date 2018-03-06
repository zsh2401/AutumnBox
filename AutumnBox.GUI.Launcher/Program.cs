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
            var startInfo = new ProcessStartInfo()
            {
                FileName = "AutumnBox.exe",
                WorkingDirectory = "file"
            };
            Process.Start(startInfo);
        }
    }
}
