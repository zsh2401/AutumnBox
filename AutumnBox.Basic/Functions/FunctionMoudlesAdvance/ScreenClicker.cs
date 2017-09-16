using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.FunctionArgs;

namespace AutumnBox.Basic.Functions
{
    public class ScreenClicker : FunctionModule
    {
        public int KEY_CODE;
        public ScreenClicker(KeyCode kc) {
            KEY_CODE = (char)kc;
        }
        protected override OutputData MainMethod()
        {
            var o = executer.Execute($"shell input keyevent {KEY_CODE}");
            return o;
        }
    }
}
