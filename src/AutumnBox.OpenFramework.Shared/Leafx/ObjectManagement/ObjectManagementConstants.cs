using System.Reflection;

namespace AutumnBox.OpenFramework.Leafx.ObjectManagement
{
    internal static class ObjectManagementConstants
    {
        public const BindingFlags BINDING_FLAGS =
            BindingFlags.Instance | BindingFlags.Public |
            BindingFlags.NonPublic | BindingFlags.SetProperty |
            BindingFlags.Default;
    }
}
