/*

* ==============================================================================
*
* Filename: Program
* Description: 
*
* Version: 1.0
* Created: 2020/5/21 20:00:50
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.Services.Impl.OS;
using System;
using System.Linq;

namespace AutumnBox.GUI
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            ResolveArguments(args);
            var app = new App();
            app.InitializeComponent();
            return app.Run();
        }
        private static void ResolveArguments(string[] args)
        {
            var process = OtherProcessChecker.ThereIsOtherAutumnBoxProcess();
            if (process != null && (!args.Contains("--wait")))
            {
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                Environment.Exit(0);
            }
            process?.WaitForExit();
        }
    }
}
