using System;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelDevices.xaml 的交互逻辑
    /// </summary>
    public partial class PanelDevices : UserControl
    {
        public static bool hasOne = false;
        public PanelDevices()
        {
            if (hasOne)
            {
                throw new Exception("1 app 1 devices panel!");
            }
            InitializeComponent();
            hasOne = true;
        }
    }
}
