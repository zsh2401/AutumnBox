using AutumnBox.GUI.Util.UI;
using AutumnBox.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelTips.xaml 的交互逻辑
    /// </summary>
    public partial class PanelTips : UserControl
    {
        private readonly TipsWaterfallFlow flow;
        public PanelTips()
        {
            InitializeComponent();
            flow = new TipsWaterfallFlow(StackColume1, StackColume2);
            (DataContext as INotifyPropertyChanged).PropertyChanged += PanelHome_PropertyChanged;
        }
        private void PanelHome_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Tips":
                    flow.TipsChanged((DataContext as VMTips).Tips);
                    break;
                //case "CstXamlObject":
                //    FullCustomGrid.Content = (DataContext as VMHome).CstXamlObject;
                //    break;
            }
        }
    }
}
