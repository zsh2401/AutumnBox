/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 8:27:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.ExtLibrary;
using System.IO;

namespace AutumnBox.CoreModules
{
    [ContextPermission(CtxPer.High)]
    public sealed class CoreLib : ExtensionLibrarin
    {
        public static Context Context { get; private set; }

        public override string Name => "AutumnBox.Core Modules";

        public override int MinApiLevel => 8;

        public override int TargetApiLevel => 8;

        public CoreLib()
        {
            Context = this;
        }
        public override void Ready()
        {
            base.Ready();
        }
    }
}
