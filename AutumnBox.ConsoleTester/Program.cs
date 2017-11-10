using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.ConsoleTester.MethodTest;
using AutumnBox.ConsoleTester.ObjTest;
using AutumnBox.GUI.Helper;
using System;
using System.Threading;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        public readonly static string Mi6ID = "af0fe186";
        public readonly static string Mi4ID = "9dd1b490";
        unsafe static void Main(string[] args)
        {
            var fmp = FunctionModuleProxy.Create(typeof(ImageExtractor), new Basic.Function.Args.ImgExtractArgs(new DeviceBasicInfo() { Id = Program.Mi4ID }, Basic.Function.Args.Image.Boot));
            fmp.OutputReceived += (s, e) => { Console.WriteLine("stdo " + e.Text); };
            fmp.Finished += (s, e) => { Console.WriteLine("Finished? " + e.Result.Level); };
            fmp.AsyncRun();
            //AndroidShellTest.RootTest();
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
            new CExecuter().AdbExecute("kill-server");
            Environment.Exit(0);

        }
    }
}
