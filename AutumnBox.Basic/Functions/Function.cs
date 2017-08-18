using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Arg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{

    internal class Function
    {
        protected Adb adb;
        protected Fastboot fastboot;
        public Function(FunctionInitType initType = FunctionInitType.All){
            switch (initType) {
                case FunctionInitType.Adb:
                    adb = new Adb();
                    break;
                case FunctionInitType.Fastboot:
                    fastboot = new Fastboot();
                    break;
                case FunctionInitType.All:
                    adb = new Adb();
                    fastboot = new Fastboot();
                    break;
            }
        }
    }
}
