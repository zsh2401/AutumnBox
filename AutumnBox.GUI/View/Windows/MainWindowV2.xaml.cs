using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.OS.BlurKit;
using AutumnBox.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// MainWindowV2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowV2
    {
        public MainWindowV2()
        {
            InitializeComponent();
            AppLoader.Instance.Loaded += (s, e) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Menu.Visibility = Visibility.Visible;
                    if (Settings.Default.IsFirstLaunch)
                    {
                        WinM.X("Donate");
                    }
                });
            };
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            var intptrHelper = new WindowInteropHelper(this);
            AcrylicHelper.EnableBlur(intptrHelper.Handle);
        }
    }
}
