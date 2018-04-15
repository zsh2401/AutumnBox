using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    public interface IExtensionScript
    {
        string Name { get; }
        string Desc { get; }
        Version Version { get; }
        int UpdateId { get; }
        string ContactInfo { get; }
        bool Run(ScriptArgs args);
        void RunAsync(ScriptArgs args, Action<bool> finishedCallback=null);
    }
}
