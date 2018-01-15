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
    internal class AdImageGetter
    {
        [JsonObject(MemberSerialization.OptOut)]
        private class RemoteAdInfo
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("link")]
            public string Link { get; set; }
            [JsonProperty("click")]
            public string Click { get; set; }
        }
        public class GettedImageEventArgs : EventArgs
        {
            public string ClickUrl { get; private set; }
            public MemoryStream MemoryStream { get; private set; }
            public GettedImageEventArgs(MemoryStream ms, string clickUrl)
            {
                this.MemoryStream = ms;
                ClickUrl = clickUrl;
            }
        }
        public event EventHandler<GettedImageEventArgs> GettedImage;
        private const string tempPath = "tmp";
#if TEST_LOCALHOST_API
        private const string apiUrl = "http://localhost:24010/api/ad/";
#else
            private const string apiUrl = "http://atmb.top/api/ad/";
#endif
        public void Work()
        {
            try
            {
                Task.Run(() =>
                {
                    var remoteInfo = GetRemoteAdInfo();
                    var ms = Download(remoteInfo);
                    if (ms != null)
                    {
                        GettedImage?.Invoke(this, new GettedImageEventArgs(ms, remoteInfo.Click));
                    }
                });
            }
            catch (Exception e)
            {
                Logger.T("ad exception", e);
            }

        }
        private readonly WebClient webClient = new WebClient();
        private MemoryStream Download(RemoteAdInfo info)
        {
            var bytes = webClient.DownloadData(info.Link);
            return new MemoryStream(bytes);
        }
        private RemoteAdInfo GetRemoteAdInfo()
        {
            Logger.D("getting remote info");
            string html = webClient.DownloadString(apiUrl);
            return (RemoteAdInfo)JsonConvert.DeserializeObject(html, typeof(RemoteAdInfo));
        }
    }
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
