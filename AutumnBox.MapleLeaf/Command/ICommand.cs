using System;

namespace AutumnBox.MapleLeaf.Basis
{
    public interface ICommand
    {
        ICommandProcess NewProcess();
        IAsyncCommandProcess NewAsyncProcess();
    }
}
