/* =============================================================================*\
*
* Filename: NetUtilFinishEventHandler
* Description: 
*
* Version: 1.0
* Created: 2017/10/31 17:30:50 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

namespace AutumnBox.GUI.NetUtil
{
    internal class NetUtilFinishEventArgs : EventArgs
    {
        public NetUtilResult Result { get; set; }
    }
}