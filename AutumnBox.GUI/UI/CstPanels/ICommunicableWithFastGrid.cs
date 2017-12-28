/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/28 21:52:46
** filename: ICloseEventTrigger.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.UI.CstPanels
{
    public interface ICommunicableWithFastGrid
    {
        event EventHandler CallFatherToClose;
        void OnFatherClosed();
    }
}
