/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 19:05:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.MapleLeaf.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Operating
{
    public interface IAndroidShell : IDisposable
    {
        void Connect();
        void Input();
        void Disconnect();
    }
}
