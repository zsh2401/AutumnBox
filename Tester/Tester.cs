/* =============================================================================*\
*
* Filename: Tester.cs
* Description: 
*
* Version: 1.0
* Created: 9/25/2017 06:29:28(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using System;
using System.Diagnostics;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Event;
using AutumnBox.Helper;
using AutumnBox.NetUtil;

namespace Tester
{
    public class Tester : IOutReceiver
    {
        public DeviceSimpleInfo defaultInfo = new DeviceSimpleInfo() { Id = "xxxxx", Status = DeviceStatus.RUNNING };
        public void Run()
        {
            CommandExecuter.Restart();
            _Run();
            Pause();
        }
        public void _Run()
        {
            //var o = MOTDGetter.t.init
        }
        public void Print(object message)
        {
            Console.WriteLine(message);
        }
        public void Pause()
        {
            Console.ReadKey();
        }

        public void OutReceived(object sender, DataReceivedEventArgs e)
        {
            Print("RealTime out  : " + e.Data);
        }

        public void ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            Print("RealTime error : " + e.Data);
        }

        public void FuncFinished(object sender, FinishEventArgs e)
        {
            //Print(e.Result.IsHandled.ToString());
        }
    }
}
