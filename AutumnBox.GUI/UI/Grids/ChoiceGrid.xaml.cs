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
    public partial class ChoiceGrid : Grid
    {
        public event HidedEventHandler Hided;
        private Double ParentHeight
        {
            get
            {
                return ((Grid)VisualTreeHelper.GetParent(this)).ActualHeight;
            }
        }
        private ThicknessAnimation _riseAnimation = new ThicknessAnimation()
        {
            To = new Thickness(0, 0, 0, 0),
            Duration = TimeSpan.FromSeconds(0.5),
        };
        private ThicknessAnimation _hideAnimation = new ThicknessAnimation()
        {
            To = new Thickness(0, 621, 0, 0),
            Duration = TimeSpan.FromSeconds(0.5),
        };
        public ChoiceGrid()
        {
            InitializeComponent();
        }
        public void Show(ChoiceData data = null)
        {
            this.BeginAnimation(MarginProperty, _riseAnimation);
        }
        private ChoiceResult _result = ChoiceResult.Left;
        public void Hide()
        {
            Hided?.Invoke(this, new HidedEventArgs() { Result = _result});
            this.BeginAnimation(MarginProperty, _hideAnimation);
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
