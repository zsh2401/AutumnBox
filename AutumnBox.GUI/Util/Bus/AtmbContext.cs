using AutumnBox.OpenFramework.Content;

namespace AutumnBox.GUI.Util.Bus
{
    [ContextPermission(CtxPer.High)]
    class AtmbContext : Context
    {
        public static readonly AtmbContext Instance = new AtmbContext();
        private AtmbContext() { }
    }
}
