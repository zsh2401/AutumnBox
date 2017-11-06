/* =============================================================================*\
*
* Filename: AndroidFullBackupTest
* Description: 
*
* Version: 1.0
* Created: 2017/11/6 22:23:31 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using System;

namespace AutumnBox.ConsoleTester.ObjTest
{
    class AndroidFullBackupTest
    {
        public static void RunTest()
        {
            var fmp = FunctionModuleProxy.Create(typeof(AndroidFullBackup), new ModuleArgs(new DeviceBasicInfo() { Id = Program.Mi6ID }));
            fmp.Finished += (s, e) =>
            {
                Console.WriteLine($"launched level : {e.Result.Level.ToString()} ");
            };
            fmp.OutReceived += (s, e) =>
            {
                Console.WriteLine(e.Data);
                try
                {
                    if (e.Data.ToLower().Contains("now unlock your device"))
                    {
                        fmp.ForceStop();
                    }
                }
                catch { }
            };
            fmp.AsyncRun();
        }
    }
}
