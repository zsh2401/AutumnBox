using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.ConsoleTester.MethodTest;
using AutumnBox.ConsoleTester.ObjTest;
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
            //var p = new ABProcess();
            //p.StartInfo.FileName = "adb/adb.exe";
            //p.StartInfo.Arguments = "shell";
            //Console.WriteLine(p.StartInfo.FileName + " " + p.StartInfo.Arguments);
            //p.Start();
            //p.BeginRead();
            //p.OutputReceived += (s, e) =>
            //{
            //    Console.WriteLine(e.Text);
            //};
            //var sw = p.StandardInput;
            //while (!p.HasExited)
            //{
            //    sw.WriteLine(Console.ReadLine());
            //    sw.Flush();
            //}
            var fmp = FunctionModuleProxy.Create(typeof(ImageExtractor), new Basic.Function.Args.ImgExtractArgs(new DeviceBasicInfo() { Id = Program.Mi6ID },Basic.Function.Args.Image.Boot));
            fmp.OutputReceived += (s, e) => { Console.WriteLine(e.Text); };
            fmp.Finished += (s, e) => { Console.WriteLine("Finished? " + e.Result.Level); };
            fmp.AsyncRun();
            Console.ReadKey();
        }
    }
}
