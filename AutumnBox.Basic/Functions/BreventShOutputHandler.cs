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
        public string output;
        private OutputData sourceOutput;
        public BreventShOutputHandler(OutputData o) {
            sourceOutput = o;
        }
        private void Start() {
            foreach (string line in sourceOutput.output) {
                output += Environment.NewLine + line;
            }
            //if(output.ToLower().Contains("error")|| output.ToLower().Contains("cannot"))
        }
    }
}
