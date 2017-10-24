/* =============================================================================*\
*
* Filename: IGetatableById
* Description: 
*
* Version: 1.0
* Created: 2017/10/24 15:31:38 (UTC+8:00)
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

namespace AutumnBox.Basic.Devices
{
    public interface IGetatableById<T> where T : struct
    {
        T GetByID(string id);
    }
}
