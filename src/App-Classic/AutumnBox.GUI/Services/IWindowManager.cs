using System.Windows;

namespace AutumnBox.GUI.Services
{
    interface IWindowManager
    {
        Window MainWindow { get; }
        Window CreateWindow(string windowName, params object[] args);
        void Show(string windowName);
        void ShowDialog(string windowName);
    }
}
