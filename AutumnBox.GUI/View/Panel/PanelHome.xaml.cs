using AutumnBox.GUI.Model;
using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.GUI.Util.UI;
using AutumnBox.GUI.ViewModel;
using AutumnBox.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelHome.xaml 的交互逻辑
    /// </summary>
    public partial class PanelHome : UserControl
    {
        private readonly TipsWaterfallFlow flow;
        public PanelHome()
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
                    flow.TipsChanged((DataContext as VMHome).Tips);
                    break;
                case "CstXamlObject":
                    FullCustomGrid.Content = (DataContext as VMHome).CstXamlObject;
                    break;
            }
        }
    }
}
