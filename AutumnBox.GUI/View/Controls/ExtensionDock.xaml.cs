using AutumnBox.GUI.Model;
using AutumnBox.OpenFramework.Wrapper;
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

namespace AutumnBox.GUI.View.Controls
{
    /// <summary>
    /// ExtensionDock.xaml 的交互逻辑
    /// </summary>
    public partial class ExtensionDock : UserControl
    {
        private readonly ExtensionWrapperDock dock;

        internal ExtensionDock(ExtensionWrapperDock dock)
        {
            InitializeComponent();
            this.DataContext = dock;
            this.dock = dock;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dock.Wrapper.GetThread().Start();
        }
    }
}
