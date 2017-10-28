using AutumnBox.Shared;
using AutumnBox.Shared.CstmDebug;
using System;
using System.Threading;

namespace AutumnBox.ConsoleTester
{
    [LogSenderPropAttribute(TAG = "Public")]
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            //var full = new TestModuleTest().GetType().Namespace;
            Logger.D(p,"fuck");
            Thread.Sleep(1000);
            Logger.D(p, "fuck");
            Thread.Sleep(1000);
            Logger.D(p, "fuck");
            Thread.Sleep(1000);
            Logger.D(p, "fuck");
            Thread.Sleep(1000);
            Logger.D(p, "fuck");
            //Console.WriteLine(nameof(AutumnBox.Basic));
            Console.ReadKey();
        }
    }
}
