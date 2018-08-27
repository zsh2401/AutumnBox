/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 2:47:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.MapleLeaf.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.MapleLeaf.Android.Shell
{
    public interface IAndroidShell : INotifyOutput, IDisposable
    {
        void InputLine(string inputContent);
    }
}
