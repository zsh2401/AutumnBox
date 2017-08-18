using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using System;
using System.Collections;
/*此文件中的代码是有关core的实例化的*/
namespace AutumnBox.Basic
{
    public sealed partial class Core
    {
        public event EventsHandlers.SimpleFinishEventHandler SendFileFinish;
        public event EventsHandlers.FinishEventHandler ActivatedBrvent;
        public event EventsHandlers.SimpleFinishEventHandler FlashCustomRecoveryFinish;
        public event EventsHandlers.FinishEventHandler RebootFinish;
        public event EventsHandlers.FinishEventHandler XiaomiSystemUnlockFinish;
        public event EventsHandlers.FinishEventHandler XiaomiBootloaderRelockFinish;


        public DevicesListener devicesListener;

        private Adb adb;
        private Fastboot fastboot;
        private FastbootTools ft;
        private AdbTools at;

        private Func<string,OutputData> ae;
        private Func<string, OutputData> fe;

        private Hashtable files = new Hashtable { { "sideloadbat", @"util\sideload.bat" } };
        public Core() {
            at = new AdbTools();
            ft = new FastbootTools();
            adb = new Adb();
            fastboot = new Fastboot();
            ae = (command) => {
                return at.Execute(command);
            };
            fe = (command) =>
            {
                return ft.Execute(command);
            };
            devicesListener = new DevicesListener(at, ft);
        }
    }
}
