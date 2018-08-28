/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 0:36:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Exceptions
{
    public class AdbCommandFailedException : Exception
    {
        public AdbCommandFailedException() : base() { }
        public AdbCommandFailedException(AdvanceOutput output) : base(output.ToString())
        {

        }
        public AdbCommandFailedException(Output output) : base(output.ToString())
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
