/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.OpenApiImpl;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;

namespace AutumnBox.GUI.Util
{
    internal static class OpenFrameworkManager
    {
        public static void Init()
        {
            AutumnBoxGuiApiProvider.Inject(new AppManagerImpl());
            var instace = ExtensionManager.Instance;
        }
    }
}
