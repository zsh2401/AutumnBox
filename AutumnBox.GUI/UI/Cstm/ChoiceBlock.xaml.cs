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

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// ChoiceGrid.xaml 的交互逻辑
    /// </summary>
    public partial class ChoiceBlock : UserControl, IDisposable
    {
        public static readonly int AnimationLength = 300;
        public bool HasClosed { get; private set; } = false;
        /// <summary>
        /// 等待结束
        /// </summary>
        public void WaitToClosed()
        {
            while (!HasClosed) ;
        }
        public ChoiceResult? Result { get; private set; } = null;

        private readonly ThicknessAnimation _riseAnimation = new ThicknessAnimation()
        {
            To = new Thickness(0, 0, 0, 0),
            Duration = TimeSpan.FromMilliseconds(AnimationLength),
        };
        private readonly ThicknessAnimation _hideAnimation = new ThicknessAnimation()
        {
            Duration = TimeSpan.FromMilliseconds(AnimationLength),
        };
        private readonly Grid _father;

        private Action<ChoiceResult> _callback;

        public ChoiceBlock(Grid father, ChoiceData data)
        {
            InitializeComponent();
            BtnLeft.Content = data.TextBtnLeft;
            BtnRight.Content = data.TextBtnRight;
            TBTitle.Text = data.Title;
            TBContent.Text = data.Text;
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

        public void Show(Action<ChoiceResult> callback)
        {
            this.HasClosed = false;
            this._callback = callback;
            this.BeginAnimation(MarginProperty, _riseAnimation);
        }
        public void Show()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.Show((r) =>
                {
                    Result = r;
                });
            });
        }
        public void Hide()
        {
            Hide(ChoiceResult.Cancel);
        }

        private void Hide(ChoiceResult result)
        {
            Dispatcher.Invoke(() =>
            {
                BeginAnimation(MarginProperty, _hideAnimation);
                _hideAnimation.Completed += (s, e) =>
                {
                    HasClosed = true;
                    Dispatcher.Invoke(() => { _callback?.Invoke(result); });
                    Dispose();
                };
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
            Hide(ChoiceResult.Right);
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            Hide(ChoiceResult.Left);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide(ChoiceResult.Cancel);
        }
    }
}
