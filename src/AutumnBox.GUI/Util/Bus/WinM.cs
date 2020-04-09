using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Util.Bus
{
    static class WinM
    {
        public static Window CreateWindow(string windowName, params object[] args)
        {
            var type = Type.GetType("AutumnBox.GUI.View.Windows." + windowName + "Window");
            var window = (Window)Activator.CreateInstance(type, args);
            window.Owner = App.Current.MainWindow;
            return window;
        }
        public static void ShowDialog(string windowName, params object[] args) 
            => CreateWindow(windowName, args).ShowDialog();

        public static void X(string windowName, params object[] args)
            => CreateWindow(windowName, args).ShowDialog();

        public static void D(string windowName, params object[] args)
            => CreateWindow(windowName, args).Show();

    }
}
