using AutumnBox.GUI.View.Panel;
using AutumnBox.GUI.View.Windows;
using AutumnBox.GUI.ViewModel;
using AutumnBox.Logging;
using HandyControl.Controls;
using System;

namespace AutumnBox.GUI.Util.Bus
{
    public static class MainWindowBus
    {
        public const string TOKEN_PANEL_MAIN = "main";
        public const string TOKEN_DIALOG = "main";
        public static MainWindowV2 MainWindow
        {
            get
            {
                return (MainWindowV2)App.Current.MainWindow;
            }
        }
        public static event EventHandler ExtensionListRefreshing;
        public static void ReloadExtensionList()
        {
            ExtensionListRefreshing?.Invoke(null, new EventArgs());
        }
        public static void SwitchToMainGrid()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    SLogger.Info(nameof(MainWindowBus), "Panel creating");
                    var panel = new PanelMainV2();
                    MainWindow.MainContentContainer.Content = panel;
                    SLogger.Info(nameof(MainWindowBus), "Created panel instance");
                }
                catch (Exception e)
                {
                    SLogger.Warn(nameof(MainWindowBus), "Could not create panel", e);
                }
            });
        }
        public static void Info(string message)
        {
            Growl.Info(App.Current.Resources[message]?.ToString() ?? message, TOKEN_PANEL_MAIN);
        }
        public static void Success(string message)
        {
            Growl.Success(App.Current.Resources[message]?.ToString() ?? message, TOKEN_PANEL_MAIN);
        }
        public static void Warning(string message)
        {
            Growl.Warning(App.Current.Resources[message]?.ToString() ?? message, TOKEN_PANEL_MAIN);
        }
        public static void Error(string message)
        {
            Growl.Error(App.Current.Resources[message]?.ToString() ?? message, TOKEN_PANEL_MAIN);
        }
        public static void Ask(string message, Func<bool, bool> callback)
        {
            Growl.Ask(message, callback);
        }
    }
}
