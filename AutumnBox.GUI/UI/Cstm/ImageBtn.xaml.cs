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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// ImageBtn.xaml 的交互逻辑
    /// </summary>
    public partial class ImageBtn : UserControl
    {
        public static readonly DependencyProperty ImageProperty
   = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageBtn));
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageSelectedProperty
   = DependencyProperty.Register("ImageSelected", typeof(ImageSource), typeof(ImageBtn));
        public ImageSource ImageSelected
        {
            get { return (ImageSource)GetValue(ImageSelectedProperty); }
            set { SetValue(ImageSelectedProperty, value); }
        }

        public ImageBtn()
        {
            InitializeComponent();
            last = ImageSelected;
            //Switch();
            //Switch();
        }

        private ImageSource last;
        private void Switch()
        {
            var tmp = Image;
            Image = last;
            last = tmp;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            MainImg.Source = ImageSelected;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            MainImg.Source = Image;
        }
    }
}
