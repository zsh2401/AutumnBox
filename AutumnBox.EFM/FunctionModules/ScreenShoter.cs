using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.FunctionArgs;

namespace AutumnBox.Basic.Functions.FunctionModules
{
    public sealed class ScreenShoter : FunctionModule
    {
        ScreenShoterArgs Args;
        public ScreenShoter(ScreenShoterArgs args) {
            Args = args;
        }
        protected override OutputData MainMethod()
        {
            OutputData o = new OutputData();
            o.Append(Ae("shell /system/bin/screencap -p /sdcard/screenshot.png"));
            o.Append(Ae("pull /sdcard/screenshot.png " + Args.localPath));
            o.Append(Ae("shell rm -rf /sdcard/screenshot.png"));
            return o;
        }
    }
}
