/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 11:09:52 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    public interface IPrintable
    {
        void PrintOnLog(bool printOnRelease=false);

        void PrintOnConsole();
    }
}
