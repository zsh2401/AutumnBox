/* =============================================================================*\
*
* Filename: IForceStoppable
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 17:22:52 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// 可被强制停止的
    /// </summary>
    public interface IForceStoppable
    {
        void ForceStop();
    }
}
