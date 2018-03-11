using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
