/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/20 17:52:48 (UTC +8:00)
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
    /// 高级输出,相比父类多了个返回码
    /// </summary>
    public class AdvanceOutput : Output
    {
        public int ExitCode { get; private set; }
        public bool IsSuccessful
        {
            get
            {
                return ExitCode == 0;
            }
        }
        public AdvanceOutput(Output source, int exitCode):base(source.All,source.Out,source.Error) {
            this.ExitCode = exitCode;
        }
    }
}
