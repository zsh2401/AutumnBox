using AutumnBox.Basic.AdbEnc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    public class BreventShOutputHandler
    {
        public bool isOk { get; private set; }
        public string output = "";
        private OutputData sourceOutput;
        public BreventShOutputHandler(OutputData o) {
            sourceOutput = o;
            Start();
        }
        private void Start() {
            isOk = true;
            foreach (string line in sourceOutput.output) {
                output += Environment.NewLine + line;
            }
            output += sourceOutput.error;
            if (sourceOutput.error != "") {
                this.isOk = false;
            }
            if (output.ToLower().Contains("error") || output.ToLower().Contains("cannot") || output.ToLower().Contains("warning")) {
                isOk = false;
            }
        }
    }
}
