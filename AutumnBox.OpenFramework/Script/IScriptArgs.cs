using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    public interface IScriptArgs
    {
        Context Context { get; }
        Script Self { get; }
    }
}
