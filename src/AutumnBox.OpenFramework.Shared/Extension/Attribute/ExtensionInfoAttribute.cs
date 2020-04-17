using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    public abstract class ExtensionInfoAttribute : ExtensionAttribute
    {
        public abstract string Key { get; }
        public abstract object Value { get; }
    }
}
