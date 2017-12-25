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
using AutumnBox.Basic.Devices;
using AutumnBox.GUI.UI.Grids;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// FunctionPanel.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionPanel : UserControl, IRefreshable
    {
        private readonly PoweronFuncPanel poweronPanel;
        private readonly FastbootFuncPanel fastbootPanel;
        private readonly RecFuncPanel recFuncPanel;
        public FunctionPanel()
        {
            InitializeComponent();
            poweronPanel = new PoweronFuncPanel();
            fastbootPanel = new FastbootFuncPanel();
            recFuncPanel = new RecFuncPanel();
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            poweronPanel.Refresh(deviceSimpleInfo);
            fastbootPanel.Refresh(deviceSimpleInfo);
            recFuncPanel.Refresh(deviceSimpleInfo);
        }

        public void Reset()
        {
            poweronPanel.Reset();
            fastbootPanel.Reset();
            recFuncPanel.Reset();
        }

        private void BtnIndex0_Click(object sender, RoutedEventArgs e)
        {
            this.GridFunc.Children.Clear();
            this.GridFunc.Children.Add(poweronPanel);
        }
    }
}
