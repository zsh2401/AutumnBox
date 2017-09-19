using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.UI
{
    public static class UIHelper
    {
        public static void DragMove(Window m,MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                m.DragMove();
            }
        }
    }
}
