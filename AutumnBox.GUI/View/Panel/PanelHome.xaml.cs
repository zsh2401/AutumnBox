using AutumnBox.GUI.Model;
using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.GUI.ViewModel;
using AutumnBox.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
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
            (DataContext as INotifyPropertyChanged).PropertyChanged += PanelHome_PropertyChanged;
#if RELEASE
            btnRefresh.Visibility = System.Windows.Visibility.Collapsed;
#endif
        }
        private async void PanelHome_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Tips")
            {
                StackColume1.Children.Clear();
                StackColume2.Children.Clear();
                await Task.Run(() =>
                {
                    do
                    {
                        Thread.Sleep(1000);
                    } while (mWrapPanel.IsVisible == false);
                });
                StackColume1.SizeChanged += Col_SizeChanged;
                StackColume2.SizeChanged += Col_SizeChanged;
                HandleTipsChanged();
            }
        }
        private IEnumerator<Tip> Enumerator { get; set; }
        private void HandleTipsChanged()
        {
            Enumerator = (DataContext as VMHome).Tips.GetEnumerator();
            AddNext();
        }
        private void Col_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            AddNext();
        }
        private void AddNext()
        {
            if (Enumerator != null && Enumerator.MoveNext())
            {
                AddTipCard(Enumerator.Current);
            }
            else
            {
                StackColume1.SizeChanged -= Col_SizeChanged;
                StackColume2.SizeChanged -= Col_SizeChanged;
                Enumerator = null;
            }
        }
        private void AddTipCard(Tip tip)
        {
            var card = new TipCard(tip);
            if (StackColume2.ActualHeight < StackColume1.ActualHeight)
            {
                StackColume2.Children.Add(card);
            }
            else
            {
                StackColume1.Children.Add(card);
            }
        }
    }
}
