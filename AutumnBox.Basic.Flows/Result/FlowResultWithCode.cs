/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 17:25:27
** filename: FlowResultWithCode.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows.Result
{
    public class FlowResultWithCode : FlowResult
    {
        public int ExitCode { get; set; }
    }
}
