using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.SERT.Impl
{
    internal class SERTManagerImpl : ISERTManager
    {
        public IScriptExtension[] Extensions { get; private set; } = Array.Empty<IScriptExtension>();
        private IAtmbApi api;
        private IRawScriptsProvider provider;
        public void Dispose()
        {
            foreach (var ext in Extensions)
            {
                ext.Dispose();
            }
        }
        public void Init(IAtmbApi api, IRawScriptsProvider provider)
        {
            if (api is null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            this.api = api;
            this.provider = provider;
        }
        public void Refresh()
        {
            List<IScriptExtension> exts = new List<IScriptExtension>();
            foreach (var kv in provider.Scripts)
            {
                exts.Add(new ScriptExtension(kv.Value));
            }
            this.Extensions = exts.ToArray();
        }
    }
}
