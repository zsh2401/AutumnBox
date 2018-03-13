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
        public ThicknessAnimation RiseAnimation { get; set; }
        public ThicknessAnimation HideAnimation { get; set; }
        public ThicknessAnimation CloseAnimation { get; set; }
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
            if (child.PanelBackground != null)
                this.Background = child.PanelBackground;
            if (child.BtnCloseForeground != null)
                this.BtnClose.Foreground = child.BtnCloseForeground;
            child.OnPanelInited(new PanelArgs() { Height = this.Height, Width = this.Width });
        }
        private void InitAnimation()
        {
            RiseAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0, father.ActualHeight, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
            };
            RiseAnimation.Completed += (s, e) => { _Display(); };
            HideAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, father.ActualHeight, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
            };
            CloseAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, father.ActualHeight, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
            };
            CloseAnimation.Completed += (s, e) => { _Close(); };
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
        public bool Displayed { get; private set; } = false;
        public void Display(bool animation = true)
        {
            if (!father.Children.Contains(this))
            {
                father.Children.Add(this);
            }
            if (animation)
            {
                BeginAnimation(MarginProperty, RiseAnimation);
            }
            Displayed = true;
        }
        public void Hide()
        {
            if (Displayed)
            {
                Displayed = false;
                BeginAnimation(MarginProperty, HideAnimation);
            }
        }
        public void Close()
        {
            if (Displayed)
            {
                Displayed = false;
                BeginAnimation(MarginProperty, CloseAnimation);
            }
            else
            {
                _Close();
            }
        }
        private void _Display()
        {
            child.OnPanelDisplayed();
        }
        private void _Close()
        {
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
