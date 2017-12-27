using AutumnBox.Basic.Connection;
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

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// DevicesPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DevicesPanel : UserControl
    {
        //public DeviceConnection CurrentSelect { get; private set; }
        //private 
        //////自定义路由事件
        ////public static readonly RoutedEvent DeviceSelectChangedEvent = EventManager.RegisterRoutedEvent("DeviceSelectChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DevicesPanel));
        ////public event RoutedEventHandler DeviceSelectChanged
        ////{
        ////    add { AddHandler(DeviceSelectChangedEvent, value); }
        ////    remove { RemoveHandler(DeviceSelectChangedEvent, value); }
        ////}
        public DevicesPanel()
        {
            InitializeComponent();
        }
    }
}
