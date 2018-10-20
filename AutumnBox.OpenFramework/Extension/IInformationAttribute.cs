using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    public interface IInformationAttribute
    {
        string Key { get; }
        object Value { get; }
    }
}
