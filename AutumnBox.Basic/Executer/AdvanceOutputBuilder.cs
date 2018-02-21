/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 21:38:41 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public class AdvanceOutputBuilder : OutputBuilder
    {
        public int? ExitCode { get; set; } = null;
        public new AdvanceOutput Result
        {
            get
            {
                return new AdvanceOutput(this.ToOutputData(), ExitCode??24010);
            }
        }
        public new void Clear() {
            base.Clear();
            ExitCode = null;
        }
    }
}
