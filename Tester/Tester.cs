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
using Newtonsoft.Json.Linq;
using AutumnBox.Util;
using AutumnBox.NetUtil;
using AutumnBox.Basic.Functions.FunctionArgs;

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
            double x = 1E7;
            var rm = RunningManager.Create(
                new DeviceSimpleInfo()
                { Id = Program.mi4ID},new MiFlash(
                    new MiFlasherArgs()
                    { batFileName = @"D:\☆下载暂存\cancro_images_7.9.21_20170921.0000.00_6.0_cn\flash_all.bat" }));
            Print("CreateFinished");
            rm.FuncEvents.OutReceiver = this;
            rm.FuncEvents.Finished += (s,e) => { Print(e.Result.IsSuccessful.ToString()); };
            Print("WillStart");
            rm.FuncStart();
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
            //Print(e.Result.IsHandled.ToString());
        }
    }
}
