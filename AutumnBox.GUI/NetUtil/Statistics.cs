/* =============================================================================*\
*
* Filename: Reporter
* Description: 
*
* Version: 1.0
* Created: 2017/10/16 13:30:24(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

namespace AutumnBox.GUI.NetUtil
{
    [NetUtilProperty(MustAddFininshedEventHandler = false)]
    internal class Statistics : NetUtil, INetUtil
    {
        public override NetUtilResult LocalMethod()
        {
            throw new NotImplementedException();
        }

        public override NetUtilResult NetMethod()
        {
            throw new NotImplementedException();
        }

    }
}
