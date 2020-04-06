using AutumnBox.OpenFramework.Leafx;
using AutumnBox.OpenFramework.Leafx.Container.Internal;

namespace AutumnBox.OpenFramework.Management
{
    public static class OpenFxLake
    {
        public static IRegisterableLake Lake { get; private set; }
        static OpenFxLake()
        {
            Lake = new SunsetLake();
        }
    }
}
