using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.UI.Fp
{
    public struct PanelArgs
    {
        public double FatherHeight { get; set; }
        public double FatherWidth { get; set; }
    }
    public interface IFastPanelChild
    {
        event EventHandler Finished;
        UIElement UIElement { get; }
        bool NeedShowBtnClose { get; }
        void OnPanelInited(PanelArgs args);
        void OnPanelDisplayed();
        void OnPanelBtnCloseClicked(ref bool prevent);
        void OnPanelClosed();
    }
}
