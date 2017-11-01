using AutumnBox.ConsoleTester.MethodTest;
using AutumnBox.ConsoleTester.ObjTest;
using AutumnBox.Shared.CstmDebug;
using System;
using System.Threading;

namespace AutumnBox.ConsoleTester
{
    //[LogProperty(TAG = "Public")]
    class Program
    {
        //[LogProperty(TAG = "DDF")]
        static void Main(string[] args)
        {
            Logger.D("Start test");
            LoggerTest.SpeedTest(1);
            LoggerTest.SpeedTest(1000);
            Console.ReadKey();
        }
    }
}
