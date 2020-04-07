using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;

namespace AutumnBox.Essentials
{
    public class EssentialsLibrarin : ExtensionLibrarian
    {
        public override string Name => "autumnbox-essentials";

        public override int MinApiLevel => 11;

        public override int TargetApiLevel => 11;
    }
}
