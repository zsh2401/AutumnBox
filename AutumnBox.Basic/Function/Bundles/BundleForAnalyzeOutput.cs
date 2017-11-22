/* =============================================================================*\
*
* Filename: BundleForAnalyzeOutput
* Description: 
*
* Version: 1.0
* Created: 2017/11/22 19:30:18 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function.Bundles
{
    public class BundleForAnalyzeOutput
    {
        public Object Other { get; set; }
        public ExecuteResult Result { get; set; }
        public OutputData OutputData { get { return Result.OutputData; } }
    }
}
