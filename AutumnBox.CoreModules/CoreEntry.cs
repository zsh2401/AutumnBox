/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:27:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.ExtLibrary;

namespace AutumnBox.CoreModules
{
    [ContextPermission(CtxPer.High)]
    public class CoreEntry : ExtensionLibrarin
    {
        public static Context Context { get; private set; }

        public override string Name => "AutumnBox.Core Modules";

        public override int MinApiLevel => 8;

        public override int TargetApiLevel => 8;

        public CoreEntry()
        {
            Context = this;
        }
    }
}
