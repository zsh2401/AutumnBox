using System;

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
            Console.ReadKey();
            return 0;
        }
    }
}
