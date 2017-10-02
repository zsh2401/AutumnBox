using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.FunctionArgs;

namespace AutumnBox.Basic.Functions.FunctionModules
{
    public class DpiChanger : FunctionModule
    {
        private int dpi;
        public DpiChanger(DpiChangerArgs args) {
            dpi = args.Dpi;
        }
        protected override OutputData MainMethod()
        {
            OutputData o = new OutputData();
            o.OutSender = Executer;
            Ae("shell wm density 400");
            Ae("adb reboot");
            return o;
        }
    }
}
