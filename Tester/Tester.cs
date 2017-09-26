using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Functions.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.RunningManager;
using AutumnBox.Basic.Functions.FunctionModules;

namespace Tester
{
    public class Tester : IOutReceiver
    {
        public DeviceSimpleInfo defaultInfo = new DeviceSimpleInfo() { Id = "xxxxx", Status = DeviceStatus.RUNNING };
        public void Run()
        {
            _Run();
            Pause();
        }
        public void _Run()
        {
            new DevicesGetter().GetDevices().ForEach((i)=> { Print(i.Id); });
        }
        public void Print(string message)
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
            Print(e.Result.IsHandled.ToString());
        }
    }
}
