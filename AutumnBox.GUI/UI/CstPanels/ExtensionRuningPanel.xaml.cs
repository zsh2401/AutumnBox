using AutumnBox.GUI.UI.Fp;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Internal;
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

namespace AutumnBox.GUI.UI.CstPanels
{
    public class StopExtensionEventArgs : EventArgs
    {
        public bool Successful { get; set; } = true;
    }
    /// <summary>
    /// ExtensionRuningPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ExtensionRuningPanel : FastPanelChild
    {
        public override Brush PanelBackground => (Brush)App.Current.Resources["ExtRunningPanelBrushKey"];
        public override Brush BtnCloseForeground => (Brush)App.Current.Resources["ForegroundBrushKey"];
        public override bool NeedShowBtnClose => false;
        /// <summary>
        /// 不加Event修饰符是有原因的
        /// </summary>
        public EventHandler<StopExtensionEventArgs> OnClickStop;
        public string CurrentRunningName { get; set; }
        public ExtensionRuningPanel()
        {
            InitializeComponent();
            TBMsg.Text = $"{CurrentRunningName} {App.Current.Resources["msgIsRunning"]}";
            BtnStop.Click += (s, e) =>
            {
                var args = new StopExtensionEventArgs();
                OnClickStop?.Invoke(this, args);
                if (!args.Successful)
                {
                    TBMsg.Text = $"{CurrentRunningName} {App.Current.Resources["msgCannotStop"]}";
                }
            };
        }
    }
}
