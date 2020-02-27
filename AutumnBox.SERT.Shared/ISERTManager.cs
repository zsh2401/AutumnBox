using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.SERT
{
    public interface ISERTManager : IDisposable
    {
        IScriptExtension[] Extensions { get; }
        void Init(IAtmbApi API, IRawScriptsProvider provider = null);
    }
}
