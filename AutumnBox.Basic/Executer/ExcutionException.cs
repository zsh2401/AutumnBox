/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/27 5:56:42 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public class ExcutionException:Exception
    {
        public CommandExecuterResult ExecuteResult { get; private set; }
        public ExcutionException(CommandExecuterResult result)
        {
            this.ExecuteResult = result;
        }
    }
}
