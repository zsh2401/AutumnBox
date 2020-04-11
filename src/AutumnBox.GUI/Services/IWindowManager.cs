using System.Windows;

namespace AutumnBox.GUI.Services
{
    interface IWindowManager
    {
        Window CreateWindow(string windowName, params object[] args);
        void Show(string windowName);
        void ShowDialog(string windowName);
    }
}
