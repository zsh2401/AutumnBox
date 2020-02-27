using AutumnBox.SERT.Runtime;
using System;

namespace AutumnBox.SERT
{
    internal interface IScriptExtension : IDisposable
    {
        string Id { get; }
        byte[] Icon { get; }
        string Name { get; }
        string Version { get; }
        bool Executable { get; }
        int Execute();
    }
}
