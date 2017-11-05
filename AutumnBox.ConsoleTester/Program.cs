using AutumnBox.ConsoleTester.MethodTest;
using AutumnBox.ConsoleTester.ObjTest;
using System;
using System.Threading;

namespace AutumnBox.ConsoleTester
{
    //[LogProperty(TAG = "Public")]
    class Program
    {
        //[LogProperty(TAG = "DDF")]
        unsafe static void Main(string[] args)
        {
            FMPTest.CreateTest();
            Console.ReadKey();
        }
    }
}
