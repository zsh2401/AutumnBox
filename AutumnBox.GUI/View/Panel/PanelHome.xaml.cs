using AutumnBox.GUI.Util.Net.Getters;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelHome.xaml 的交互逻辑
    /// </summary>
    public partial class PanelHome : UserControl
    {
        public PanelHome()
        {
            InitializeComponent();
            new TipsGetter().Advance().ContinueWith((task) =>
            {
                if (task.IsCompleted)
                {
                    foreach (var tip in task.Result.Tips)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            TipsPanel.Children.Insert(0, new TipCard(tip));
                        });
                    }
                }
            });
        }
    }
}
