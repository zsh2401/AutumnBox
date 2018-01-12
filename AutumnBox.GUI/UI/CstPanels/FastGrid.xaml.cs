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

namespace AutumnBox.GUI.UI.CstPanels
{
    /// <summary>
    /// FastGrid.xaml 的交互逻辑
    /// </summary>
    public partial class FastGrid : UserControl
    {
        private Action hidedcallback;
        private readonly Panel _father;
        private readonly ThicknessAnimation riseAnimation;
        private readonly ThicknessAnimation hideAnimation;
        private ICommunicableWithFastGrid communicableChirden;
        public FastGrid(Panel father, UIElement chirden, Brush background = null)
        {
            InitializeComponent();
            _father = father;
            if (background != null) this.Background = background;
            GridContent.Children.Add(chirden);
            Height = _father.ActualHeight;
            Width = _father.ActualWidth;
            _father.Children.Add(this);
            if (chirden is ICommunicableWithFastGrid)
            {
                communicableChirden = (ICommunicableWithFastGrid)chirden;
                communicableChirden.CallFatherToClose += (s, e) => { Close(); };
            }
            riseAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0, _father.ActualHeight, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
            };
            hideAnimation = new ThicknessAnimation()
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(0, _father.ActualHeight, 0, 0),
                Duration = TimeSpan.FromMilliseconds(500),
            };
            hideAnimation.Completed += (s, e) =>
            {
                _father.Children.Remove(this);
                hidedcallback?.Invoke();
            };
        }
        public FastGrid(Panel father, UIElement chirden, Action onhidedcallback) : this(father, chirden)
        {
            hidedcallback = onhidedcallback;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BeginAnimation(MarginProperty, riseAnimation);
        }
        public void Close()
        {
            BeginAnimation(MarginProperty, hideAnimation);
            communicableChirden?.OnFatherClosed();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
