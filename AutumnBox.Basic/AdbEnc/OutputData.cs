/*
 输出的内容的结构体
 @zsh2401
 2017/9/8
 */
using System.Collections.Generic;

namespace AutumnBox.Basic.AdbEnc
{
    public struct OutputData
    {
        public string error { get; internal set; }
        public string nOutPut
        {
            get
            {
                string x = "";
                foreach (string line in output)
                {
                    if (line != null)
                        x += line + "\r\n";
                }
                return x;
            }
        }
        public List<string> output { get; internal set; }
        public string AllOut { get; internal set; }
    }
}
