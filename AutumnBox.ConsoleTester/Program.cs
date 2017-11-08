using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.ConsoleTester.MethodTest;
using AutumnBox.ConsoleTester.ObjTest;
using System;
using System.Threading;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static string Mi6ID = "af0fe186";
        unsafe static void Main(string[] args)
        {
            var o = new ABProcess().RunToExited("cmd.exe", "/c Adb\\adb.exe help");
            Console.WriteLine(o.ToString());
            Console.ReadKey();
        }
    }
}
