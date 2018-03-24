using System.Windows;

namespace AutumnBox.GUI.UI.Cstm
{
    internal interface ITitleBarWindow
    {
        bool BtnMinEnable { get; }
        Window MainWindow { get; }
        void OnBtnCloseClicked();
        void OnBtnMinClicked();
        void OnDragMove();
    }
}
