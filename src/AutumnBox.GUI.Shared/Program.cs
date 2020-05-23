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
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using System;
using System.Linq;

namespace AutumnBox.GUI
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            if (!HandleArguments(args)) return 0;
            var application = new App()
            {
                Lake = BuildLake()
            };
            application.InitializeComponent();
            return application.Run();
        }
        private static bool HandleArguments(string[] args)
        {
            var process = OtherProcessChecker.ThereIsOtherAutumnBoxProcess();
            if (process != null && (!args.Contains("--wait")))
            {
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                App.Current.Shutdown(0);
                return false;
            }
            process?.WaitForExit();
            return true;
        }
        private static IRegisterableLake BuildLake()
        {
            SunsetLake lake = new SunsetLake();
            new ClassComponentsLoader(
            "AutumnBox.GUI.Services.Impl",
            lake)
            .Do();
            return lake;
        }
    }
}
