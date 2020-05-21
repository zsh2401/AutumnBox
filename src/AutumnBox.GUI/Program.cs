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
using System;

namespace AutumnBox.GUI
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            var application = new App();
            application.InitializeComponent();
            return application.Run();
        }
    }
}
