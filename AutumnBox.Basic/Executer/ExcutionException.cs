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
    /// <summary>
    /// 命令执行错误
    /// </summary>
    public class ExcutionException:Exception
    {
        public Output ExecuteResult { get; private set; }
        public ExcutionException(Output result)
        {
            this.ExecuteResult = result;
        }
    }
}
