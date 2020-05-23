using AutumnBox.Leafx.Container.Support;
using System;
using System.Windows;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IWindowManager))]
    class WindowManager : LoggingObject, IWindowManager
    {
        public Window MainWindow => App.Current.MainWindow;

        public Window CreateWindow(string windowName, params object[] args)
        {
            var type = Type.GetType("AutumnBox.GUI.Views.Windows." + windowName + "Window");
            var window = (Window)Activator.CreateInstance(type, args);
            window.Owner = App.Current.MainWindow;
            return window;
        }

        public void Show(string windowName) => CreateWindow(windowName).Show();

        public void ShowDialog(string windowName) => CreateWindow(windowName).ShowDialog();
    }
}
