using System;
using AutumnBox.SERT.Shared.Runtime;

namespace AutumnBox.SERT.Shared.Impl
{
    public class ScriptExtension : IScriptExtension
    {

        public string Id => throw new NotImplementedException();

        public byte[] Icon => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public bool Executable => rt.HasMainFunction;

        public string Version => throw new NotImplementedException();

        private readonly ScriptRT rt;
        public ScriptExtension(string script)
        {
            if (script is null)
            {
                throw new ArgumentNullException(nameof(script));
            }
            rt = new ScriptRT(script);
        }

        public void Dispose()
        {
            rt?.Dispose();
        }

        public int Execute()
        {
            return rt.Main();
        }
    }
}
