using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    public interface IExtensionScript:IExtension
    {
        string Name { get; }
        string Desc { get; }
        string Auth { get; }
        Version Version { get; }
        int UpdateId { get; }
        string ContactInfo { get; }
        void RunAsync(ExtensionStartArgs args, Action<bool> finishedCallback=null);
    }
}
