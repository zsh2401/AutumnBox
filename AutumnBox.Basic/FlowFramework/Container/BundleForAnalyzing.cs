/* =============================================================================*\
*
* Filename: BundleForAnalyzing
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 15:21:40 (UTC+8:00)
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

namespace AutumnBox.Basic.FlowFramework.Container
{
    public class BundleForAnalyzing<RESULT_T> where RESULT_T : Result
    {
        public RESULT_T Result { get; set; }
        public OutputData Output { get; set; }
    }
}
