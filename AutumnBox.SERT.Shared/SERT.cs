using AutumnBox.SERT.Shared.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.SERT.Shared
{
    public static class SERT
    {
        public static ISERTManager NewSERTManager()
        {
            return new SERTManagerImpl();
        }
    }
}
