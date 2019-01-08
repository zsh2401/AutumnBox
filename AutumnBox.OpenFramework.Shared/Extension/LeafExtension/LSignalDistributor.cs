using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    internal sealed class LSignalDistributor
    {
        private readonly LeafExtensionBase ext;

        public LSignalDistributor(LeafExtensionBase ext)
        {
            this.ext = ext ?? throw new ArgumentNullException(nameof(ext));
        }
        public void ScanReceiver()
        {
            throw new NotImplementedException();
        }
        public void Receive(string message, object value)
        {
            throw new NotImplementedException();
        }
    }
}
