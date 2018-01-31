/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/22 1:50:56 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Util;
using AutumnBox.Support.CstmDebug;
using System;
using System.Text;

namespace AutumnBox.Basic.ACP
{
    public struct AcpResponse:IPrinter
    {
        public bool IsSuccessful
        {
            get
            {
                return FirstCode == Acp.FCODE_SUCCESS;
            }
        }
        public byte FirstCode { get; set; }
        public byte[] Data { get; set; }

        public void PrintOnConsole()
        {
            Console.WriteLine($"PrintOnConsole(): {FirstCode} {ToString()}");
        }

        public void PrintOnLog()
        {
            Logger.T($"PrintOnLog(): {FirstCode} {ToString()}");
        }

        public override string ToString()
        {
            try
            {
                return Encoding.UTF8.GetString(Data);
            }
            catch
            {
                return base.ToString();
            }
        }
    }
}
