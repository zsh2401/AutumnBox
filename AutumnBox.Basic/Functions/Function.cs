using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Arg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{

    /// <summary>
    /// 各种功能类的父类
    /// </summary>
#if DEBUG
    internal class Function
#else
     internal class Function
#endif
    {
        protected Adb adb;
        protected Fastboot fastboot;
        protected Thread mainThread;
        internal Function(FunctionInitType initType = FunctionInitType.All){
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
