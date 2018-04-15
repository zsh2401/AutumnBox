using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Script;
using CSScriptLibrary;
using System;
using System.IO;
using System.Reflection;

namespace AutumnBox.ConsoleTester
{
    class Program : Context
    {
        private void Run()
        {
            ScriptsManager.Reload(this);
            var scripts = ScriptsManager.GetScripts(this);
            Console.WriteLine(scripts.Length);
        }
        unsafe static int Main(string[] cmdargs)
        {
            new Program().Run();
            Console.ReadKey();
            return 0;
        }
    }
}
