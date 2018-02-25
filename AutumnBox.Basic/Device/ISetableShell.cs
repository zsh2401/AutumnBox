/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 12:11:48
** filename: ShareableShell.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 可设置AndroidShell
    /// </summary>
    interface ISetableShell
    {
        AndroidShell ShellAsSu { set; }
    }
}
