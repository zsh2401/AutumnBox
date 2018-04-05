/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/22 1:50:56 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Util;
using AutumnBox.Support.Log;
using System;
using System.Text;

namespace AutumnBox.Basic.ACP
{
    [Obsolete("Project-ACP is dead")]
    internal struct AcpResponse:IPrintable
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

        public void PrintOnConsole(object tagOrSender)
        {
            throw new NotImplementedException();
        }

        public void PrintOnLog(bool printOnRelease = false)
        {
            if (printOnRelease) {
                Logger.Info(this,$"PrintOnLog(): {FirstCode} {ToString()}");
            } else {
                Logger.Debug(this,$"PrintOnLog(): {FirstCode} {ToString()}");
            } 
        }

        public void PrintOnLog(object tagOrSender, bool printOnRelease = false)
        {
            throw new NotImplementedException();
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
