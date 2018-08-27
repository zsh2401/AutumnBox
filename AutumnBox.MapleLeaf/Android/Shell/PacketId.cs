/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 23:48:23 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Android.Shell
{
    public enum PacketId : byte
    {
        Stdin = 0,
        Stdout = 1,
        Stderr = 2,
        Exit = 3,
        // Close subprocess stdin if possible.
        CloseStdin = 4,
        // Window size change (an ASCII version of struct winsize).
        WindowSizeChange = 5,
        // Indicates an invalid or unknown packet.
        Invalid = 255,
    }
}
