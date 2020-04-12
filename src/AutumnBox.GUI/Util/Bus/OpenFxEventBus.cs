using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Bus
{
    [Obsolete("Use service to instead", true)]
    static class OpenFxEventBus
    {
        private static readonly Stack<Action> fxLoadedHandlers = new Stack<Action>();
        private static bool isLoaded = false;
        public static void OnLoaded()
        {
            isLoaded = true;
            foreach (var act in fxLoadedHandlers)
            {
                Task.Run(() => act?.Invoke());
            }
        }
        public static void AfterOpenFxLoaded(Action act)
        {
            if (isLoaded)
            {
                act?.Invoke();
            }
            else {
                fxLoadedHandlers.Push(act);
            }
        }
    }
}
