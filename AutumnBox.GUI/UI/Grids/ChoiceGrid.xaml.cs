using AutumnBox.GUI.Helper;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.Grids
{
    /// <summary>
    /// ChoiceGrid.xaml 的交互逻辑
    /// </summary>
    public partial class ChoiceGrid : UserControl, IDisposable
    {
        public event HidedEventHandler Hided;
        public static readonly double AnimationLength = 0.5;
        private readonly ThicknessAnimation _riseAnimation = new ThicknessAnimation()
        {
            To = new Thickness(0, 0, 0, 0),
            Duration = TimeSpan.FromSeconds(AnimationLength),
        };
        private readonly ThicknessAnimation _hideAnimation = new ThicknessAnimation()
        {
            To = new Thickness(0, 621, 0, 0),
            Duration = TimeSpan.FromSeconds(AnimationLength),
        };
        private ChoiceResult _result = ChoiceResult.Left;
        private readonly Grid _father;
        public ChoiceGrid(Grid father, ChoiceData data = null)
        {
            InitializeComponent();
            _father = father;
            _father.Children.Add(this);
            this.Height = _father.ActualHeight;
            this.Width = _father.ActualWidth;
            SetTop(_father.ActualHeight);
            _hideAnimation.To = new Thickness(0, _father.ActualHeight, 0, 0);
        }

        private void SetTop(double topValue)
        {
            var margin = this.Margin;
            margin.Top = topValue;
            this.Margin = margin;
        }

        public void Show()
        {
            this.BeginAnimation(MarginProperty, _riseAnimation);
        }

        public void Hide()
        {
            Hided?.Invoke(this, new HidedEventArgs() { Result = _result });
            this.BeginAnimation(MarginProperty, _hideAnimation);
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                Dispose();
            });
        }
        public void Dispose()
        {
            this.Dispatcher.Invoke(() =>
            {
                _father.Children.Remove(this);
                GC.SuppressFinalize(this);
            });
        }
        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }
    }
}
