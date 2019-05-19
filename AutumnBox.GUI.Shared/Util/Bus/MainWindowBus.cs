using AutumnBox.GUI.ViewModel;
using HandyControl.Controls;

namespace AutumnBox.GUI.Util.Bus
{
    public static class MainWindowBus
    {
        public const string TOKEN_PANEL_MAIN = "main";
        public static void Info(string message)
        {
            Growl.Success(App.Current.Resources[message]?.ToString() ?? message, TOKEN_PANEL_MAIN);
        }
        public static void Success(string message)
        {
            Growl.Success(App.Current.Resources[message]?.ToString() ?? message, TOKEN_PANEL_MAIN);
        }
        public static void ShowSlice(object view, string title = null)
        {
            var vm = App.Current.MainWindow.DataContext as VMMainWindowV2;
            vm.ShowSlice(view, title);
        }
    }
}
