using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class SoundImpl : ISoundService
    {
        public void OK()
        {
            OpenFxLoader.BaseApi.PlayOk();
        }
    }
}
