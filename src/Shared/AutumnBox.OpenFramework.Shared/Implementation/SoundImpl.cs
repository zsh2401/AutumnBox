using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Implementation
{
    [Component(Type = typeof(ISoundService))]
    internal class SoundImpl : ISoundService
    {
        private readonly IBaseApi baseApi;

        public SoundImpl(IBaseApi baseApi) {
            this.baseApi = baseApi ?? throw new System.ArgumentNullException(nameof(baseApi));
        }
        public void OK()
        {
            baseApi.PlayOk();
        }
    }
}
