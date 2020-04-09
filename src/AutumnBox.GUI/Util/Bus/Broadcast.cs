using System;

namespace AutumnBox.GUI.Util.Bus
{
    public static class Broadcast
    {
        public enum BroadcastType
        {
            AppLoaded,
            REQUIRED_REFRESH_EXTENSIONS_LIST,
            
        }
        public static EventHandler BroadcastReceived;
        public static void Send(string id, EventArgs eventArgs) { }
        public static void SendAndHandleInUIThread()
        {

        }
    }
}
