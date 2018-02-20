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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.Fp
{
    /// <summary>
    /// FastPanels.xaml 的交互逻辑
    /// </summary>
    public partial class FastPanel : UserControl
    {
        private ThicknessAnimation riseAnimation;
        private ThicknessAnimation hideAnimation;
        private ThicknessAnimation closeAnimation;
        private readonly FastPanelChild child;
        private readonly Panel father;
        public event EventHandler Closed;
        public FastPanel(Panel father, FastPanelChild child)
        {
            InitializeComponent();
            this.father = father;
            this.child = child;
            InitSize();
            InitChild();
            InitAnimation();
            if (!child.NeedShowBtnClose)
            {
                BtnClose.Visibility = Visibility.Collapsed;
            }
            child.OnPanelInited(new PanelArgs() { Height = this.Height, Width = this.Width });
        }
        private void InitAnimation()
        {
            riseAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0, father.ActualHeight, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
            };
            riseAnimation.Completed += (s, e) => { _Display(); };
            hideAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, father.ActualHeight, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
            };
            closeAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, father.ActualHeight, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
            };
            closeAnimation.Completed += (s, e) => { _Close(); };
        }
        private void InitSize()
        {
            Height = father.ActualHeight;
            Width = father.ActualWidth;
        }
        private void InitChild()
        {
            child.Father = this;
            Container.Children.Add(child);
        }
        public void Display(bool animation = true)
        {
            if (animation)
            {
                BeginAnimation(MarginProperty, riseAnimation);
            }
            father.Children.Add(this);
        }
        public void Hide()
        {
            BeginAnimation(MarginProperty, hideAnimation);
        }
        public void Close()
        {
            BeginAnimation(MarginProperty, closeAnimation);
        }
        private void _Display()
        {
            child.OnPanelDisplayed();
        }
        private void _Close()
        {
            hideAnimation.Completed += (s, e) =>
            father.Children.Remove(this);
            child.OnPanelClosed();
            Closed?.Invoke(this, new EventArgs());
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            bool prevent = false;
            child.OnPanelBtnCloseClicked(ref prevent);
            if (!prevent)
            {
                Close();
            }
        }
    }
}
