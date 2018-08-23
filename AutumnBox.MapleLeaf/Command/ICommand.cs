using AutumnBox.MapleLeaf.DeviceManagement;
using System;

namespace AutumnBox.MapleLeaf.Command
{
    public interface ICommand
    {
        ICommandProcess NewProcess();
        IAsyncCommandProcess NewAsyncProcess();
    }
}
