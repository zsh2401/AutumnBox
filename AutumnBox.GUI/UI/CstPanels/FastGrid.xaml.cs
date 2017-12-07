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
        private readonly Panel _father;
        private readonly ThicknessAnimation riseAnimation;
        private readonly ThicknessAnimation hideAnimation;
        public FastGrid(Panel father, UIElement chirden, Brush background = null)
        {
            InitializeComponent();
            _father = father;
            if (background != null) this.Background = background;
            GridContent.Children.Add(chirden);
            Height = _father.ActualHeight;
            Width = _father.ActualWidth;
            _father.Children.Add(this);
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
                GC.SuppressFinalize(this);
            };
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BeginAnimation(MarginProperty, riseAnimation);
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BeginAnimation(MarginProperty, hideAnimation);
        }
    }
}
