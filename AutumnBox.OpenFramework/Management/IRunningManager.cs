using AutumnBox.OpenFramework.Warpper;

namespace AutumnBox.OpenFramework.Management
{
    public interface IRunningManager
    {
        void Add(IExtensionWarpper warpper);
        void Remove(IExtensionWarpper warpper);
    }
}
