/* =============================================================================*\
*
* Filename: IceBoxAndAirForzenActivatorErrType
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 23:23:39 (UTC+8:00)
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

namespace AutumnBox.Basic.Flows.States
{
    public enum IceActivatorErrType
    {
        None,
        UnknowAdmin,
        HaveOtherUser,
        DeviceOwnerIsAlreadySet,
        Unknow,
        UnknowJavaException,
        DeviceAlreadyProvisioned,
    }
}
