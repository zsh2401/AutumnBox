/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 15:32:23 (UTC +8:00)
** desc： ...
*************************************************/
using System.Reflection;

namespace AutumnBox.OpenFramework.Entrance
{
    sealed class DefaultEntrance : AtmbEntrance
    {
        public sealed override string Name => ManagedAssembly.GetName().Name;

        public sealed override int MinSdk => BuildInfo.SDK_VERSION;

        public sealed override int TargetSdk => BuildInfo.SDK_VERSION;

        public DefaultEntrance(Assembly assembly) {
            Init(assembly);
        }
    }
}
