/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 12:11:48
** filename: ShareableShell.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    interface IShareableShell
    {
        AndroidShell ShellAsSu { set; }
    }
}
