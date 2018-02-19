using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.Fp
{
    /// <summary>
    /// FastPanelChild.xaml 的交互逻辑
    /// </summary>
    public partial class FastPanelChild : UserControl, IFastPanelChild
    {
        public event EventHandler Finished;
        public virtual bool NeedShowBtnClose { get;} = true;
        public UIElement UIElement => this;
        public virtual void OnPanelInited(PanelArgs args) {}
        public virtual void OnPanelDisplayed() { }
        public virtual void OnPanelBtnCloseClicked(ref bool prevent) {}
        public virtual void OnPanelClosed() { }
        protected void Finish()
        {
            Finished?.Invoke(this, new EventArgs());
        }
    }
}
