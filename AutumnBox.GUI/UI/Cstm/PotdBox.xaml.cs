using AutumnBox.GUI.Cfg;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.Resources.Images;
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
    public partial class PotdBox : UserControl
    {
        private string clickUrl;
        public PotdBox()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            new PotdGetter().RunAsync((result) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    SetByResult(result);
                });
            });
        }

        private void LBtnClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void SetByResult(PotdGetterResult result)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = result.ImageMemoryStream;
            bmp.EndInit();
            ImgMain.Source = bmp;
            clickUrl = result.ClickUrl;
            LBtnClose.Visibility = result.CanbeClosed ? Visibility.Visible : Visibility.Hidden;
        }

        private void OpenUrl()
        {
            try { Process.Start(clickUrl); } catch (Exception ex) { Logger.T($"click potd failed {ex.ToString()}"); }
        }
        private async void Close()
        {
            LBtnClose.Visibility = Visibility.Hidden;
            ImgMain.Source = ImageGetter.Get("ad_close.png");
            TBCountDown.Visibility = Visibility.Visible;
            while (int.Parse(TBCountDown.Text) > 0)
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                });
                TBCountDown.Text = (int.Parse(TBCountDown.Text) - 1).ToString();
            }
            this.Visibility = Visibility.Hidden;
        }
        private void ImgMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenUrl();
        }
    }
}
