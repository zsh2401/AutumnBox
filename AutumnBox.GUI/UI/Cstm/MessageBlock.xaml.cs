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

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBlock : UserControl, IDisposable
    {
        public static readonly int AnimationLength = 300;
        private readonly ThicknessAnimation _riseAnimation = new ThicknessAnimation()
        {
            To = new Thickness(0, 0, 0, 0),
            Duration = TimeSpan.FromMilliseconds(AnimationLength),
        };
        private readonly ThicknessAnimation _hideAnimation = new ThicknessAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(AnimationLength),
        };
        private Grid _father;
        public MessageBlock(Grid father, string title, string text)
        {
            InitializeComponent();
            TBTitle.Text = title;
            TBContent.Text = text;
            _father = father;
            _father.Children.Add(this);
            Height = _father.ActualHeight;
            Width = _father.ActualWidth;
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
            this.BeginAnimation(Grid.MarginProperty, _riseAnimation);
        }

        public void Hide()
        {
            this.BeginAnimation(Grid.MarginProperty, _hideAnimation);
            Task.Run(() =>
            {
                Thread.Sleep(AnimationLength);
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

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
