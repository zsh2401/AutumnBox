//#define TEST_LOCALHOST_API
using AutumnBox.GUI.Cfg;
using AutumnBox.Support.CstmDebug;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
    /// AdBox.xaml 的交互逻辑
    /// </summary>
    public partial class AdBox : UserControl
    {
        private string clickUrl;
        public AdBox()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AdImageGetter imgGetter = new AdImageGetter();
            imgGetter.GettedImage += (s, ea) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = ea.MemoryStream;
                    bmp.EndInit();
                    ImgMain.Source = bmp;
                    clickUrl = ea.ClickUrl;
                });
            };
            imgGetter.Work();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (clickUrl != null)
            {
                Process.Start(clickUrl);
            }
        }
    }
}
