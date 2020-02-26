using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.SERT.Shared
{
    public interface IScriptExtension : IDisposable
    {
        string Id { get; }
        byte[] Icon { get; }
        string Name { get; }
        string Version { get; }
        bool Executable { get; }
        int Execute();
    }
}
