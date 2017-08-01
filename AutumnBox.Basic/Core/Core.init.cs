using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic
{
    public partial class Core
    {
        public delegate void FinishEventHandler(OutputData o);
        public delegate void StartEvnetHandler(object args);
        public StartEvnetHandler PushStart;
        public FinishEventHandler PushFinish;
        public FinishEventHandler RebootFinish;
        public StartEvnetHandler FlashRecoveryStart;
        public FinishEventHandler FlashRecoveryFinish;

        public DevicesListener dl;
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
            dl = new DevicesListener(at, ft);
            dl.Start();
        }
    }
}
