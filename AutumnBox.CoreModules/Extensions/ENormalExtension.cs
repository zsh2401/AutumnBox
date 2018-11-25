using AutumnBox.OpenFramework.Extension;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Wow!")]
    class ENormalExtension : AutumnBoxExtension
    {
        public override int Main(Dictionary<string, object> args)
        {
            Ux.Message("Wow");
            Args.CurrentThread.Kill();
            Ux.Message("WTF");
            return 0;
        }
    }
}
