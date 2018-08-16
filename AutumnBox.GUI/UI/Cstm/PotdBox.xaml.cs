using AutumnBox.GUI.Resources.Images;
using AutumnBox.GUI.Util.Net;
using AutumnBox.Support.Log;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.UI.Cstm
{

    /// <summary>
    /// AdBox.xaml 的交互逻辑
    /// </summary>
    public partial class PotdBox : UserControl
    {
        private PotdRemoteInfo remoteInfo;
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
        private void ImgMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenUrl();
        }

        private void SetByResult(PotdGetterResult result)
        {
            try
            {
                if (result.RemoteInfo.Enable == false) return;
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = result.ImageMemoryStream;
                bmp.EndInit();
                ImgMain.Source = bmp;
                remoteInfo = result.RemoteInfo;
                LBtnClose.Visibility = remoteInfo.CanbeClosed == true ? Visibility.Visible : Visibility.Hidden;
            }
            catch (Exception e)
            {
                Logger.Warn(this, "exception on setting PotdBox", e);
            }

        }
        private void OpenUrl()
        {
            try
            {
                if (remoteInfo.ClickUrl != null && remoteInfo.ClickUrl != "")
                    Process.Start(remoteInfo.ClickUrl);
            }
            catch (Exception ex)
            {
                Logger.Warn(this,$"click potd failed {ex.ToString()}");
            }
        }
        private async void Close()
        {
            try
            {
                LBtnClose.Visibility = Visibility.Hidden;
                ImgMain.Source = remoteInfo.IsAd == true ? ImageGetter.Get("ad_close.png") : ImageGetter.Get("potd_close.png");
                TBCountDown.Visibility = Visibility.Visible;
                while (int.Parse(TBCountDown.Text) >= 0)
                {
                    await Task.Run(() =>
                    {
                        Thread.Sleep(1000);
                    });
                    TBCountDown.Text = (int.Parse(TBCountDown.Text) - 1).ToString();
                }
                this.Visibility = Visibility.Hidden;
            }
            catch (Exception e)
            {
                Logger.Warn(this, "exception on closing PotdBox", e);
            }
        }
    }
}
