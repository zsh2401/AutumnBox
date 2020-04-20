using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EExtensionInstaller : LeafExtensionBase
    {
        [AutoInject]
        private readonly IAppManager appManager;

        [LMain]
        public void EntryPoint()
        {
            
        }
    }
}
