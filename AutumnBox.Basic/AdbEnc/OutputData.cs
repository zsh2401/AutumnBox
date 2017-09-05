using System.Collections.Generic;

namespace AutumnBox.Basic.AdbEnc
{
    public struct OutputData
    {
        public string error;
        public string nOutPut { get {
                string x = "";
                foreach (string line in output) {
                    x += line + "\r\n";
                }
                return x;
            } }
        public List<string> output;
    }
}
