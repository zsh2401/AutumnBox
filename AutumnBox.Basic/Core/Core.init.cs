using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using System;

namespace AutumnBox.Basic
{
    public partial class Core
    {
        public delegate void FinishEventHandler(OutputData o);
        public delegate void StartEvnetHandler(object args);
        public event StartEvnetHandler PushStart;
        public event FinishEventHandler PushFinish;
        public event FinishEventHandler RebootFinish;
        public event StartEvnetHandler FlashRecoveryStart;
        public event FinishEventHandler FlashRecoveryFinish;
        public event FinishEventHandler UnlockMiSystemFinish;
        public event FinishEventHandler RelockMiFinish;
        public event FinishEventHandler SideloadFinish;


        public DevicesListener devicesListener;
        private FastbootTools ft;
        private AdbTools at;

        private Func<string,OutputData> ae;
        private Func<string, OutputData> fe;
        public Core() {
            at = new AdbTools();
            ft = new FastbootTools();
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
