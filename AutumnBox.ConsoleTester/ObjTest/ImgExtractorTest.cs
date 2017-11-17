/* =============================================================================*\
*
* Filename: ImgExtractorTest
* Description: 
*
* Version: 1.0
* Created: 2017/11/12 16:46:48 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutumnBox.ConsoleTester.Program;
namespace AutumnBox.ConsoleTester.ObjTest
{
    class ImgExtractorTest
    {
        public static void Run() {
            WriteWithColor(() => Console.WriteLine("do you like PUBG?"), ConsoleColor.Red);
            var dev = new DevicesGetter().GetDevices()[0].Id;
            WriteWithColor(() => Console.WriteLine("Get Finished...?V35"), ConsoleColor.Green);
            var fmp = FunctionModuleProxy.Create(typeof(ImageExtractor), new Basic.Function.Args.ImgExtractArgs(new DeviceBasicInfo() { Id = dev }, Images.Boot));
            fmp.OutputReceived += (s, e) => { Console.WriteLine("stdo " + e.Text); };
            fmp.Finished += (s, e) => { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Finished? " + e.Result.Level); };
            fmp.AsyncRun();
            WriteWithColor(() => Console.WriteLine("Running..."), ConsoleColor.Red);
            Console.ReadKey();
            new CExecuter().AdbExecute("kill-server");
            Environment.Exit(0);
        }
    }
}
