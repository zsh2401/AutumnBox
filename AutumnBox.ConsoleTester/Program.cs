using AutumnBox.Basic.Device;
using AutumnBox.Basic.Executer;
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
            //ScriptsManager.Reload(this);
            //var scripts = ScriptsManager.GetScripts(this);
            //Console.WriteLine(scripts.Length);
        }
        unsafe static int Main(string[] cmdargs)
        {
            var builder = new AdvanceOutputBuilder();
            var builder2 = new AdvanceOutputBuilder();
            builder.ExitCode = 0;
            builder2.ExitCode = 0;
            builder.AppendOut("xx");
            builder2.AppendOut("xx");
            Console.WriteLine(builder.Result.GetHashCode() == builder2.Result.GetHashCode());
            Console.ReadKey();
            return 0;
        }
    }
}
