using AutumnBox.ConsoleTester.ObjTest;
using AutumnBox.Shared;
using AutumnBox.Shared.CstmDebug;
using System;
using System.Threading;

namespace AutumnBox.ConsoleTester
{
    [LogSenderProp(TAG = "Public")]
    class Program
    {
        static void Main(string[] args)
        {
            //new Config
            //var p = new Program();
            ////var full = new TestModuleTest().GetType().Namespace;
            //Logger.D(p,"fuck");
            //Thread.Sleep(1000);
            //Logger.D(p, "fuck");
            //Thread.Sleep(1000);
            //Logger.D(p, "fuck");
            //Thread.Sleep(1000);
            //Logger.D(p, "fuck");
            //Thread.Sleep(1000);
            //Logger.D(p, "fuck");
            ////Console.WriteLine(nameof(AutumnBox.Basic));
            JsonPropTest.Run();
            Console.ReadKey();
        }
    }
}
