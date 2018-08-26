/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 1:52:19 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Adb
{
    public interface IAdbService : IDisposable
    {
        IAdbClient CreateClient(bool connectAfterCreated=true);
        ushort Port { get; }
        bool IsAlive { get; }
        void Start(ushort port);
        void Kill();
    }
}
