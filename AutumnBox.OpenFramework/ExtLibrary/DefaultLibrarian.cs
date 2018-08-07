/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 15:32:23 (UTC +8:00)
** desc： ...
*************************************************/
using System.Reflection;

namespace AutumnBox.OpenFramework.ExtLibrary
{
    sealed class DefaultLibrarian : TypeBasedLibrarian
    {
        public sealed override string Name => ManagedAssembly.GetName().Name;

        public sealed override int MinApiLevel => BuildInfo.API_LEVEL;

        public sealed override int TargetApiLevel => BuildInfo.API_LEVEL;

        public DefaultLibrarian(Assembly assembly)
        {
            Init(assembly);
        }
    }
}
