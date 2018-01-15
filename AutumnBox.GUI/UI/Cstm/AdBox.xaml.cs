using AutumnBox.GUI.Cfg;
using AutumnBox.Support.CstmDebug;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// AdBox.xaml 的交互逻辑
    /// </summary>
    public partial class AdBox : UserControl
    {
        private class AdImageGetter
        {
            [JsonObject(MemberSerialization.OptOut)]
            private class RemoteAdInfo
            {
                [JsonProperty("name")]
                public string Name { get; set; }
                [JsonProperty("link")]
                public string Link { get; set; }
            }
            public class GettedImageEventArgs : EventArgs
            {
                public BitmapImage ResultImage { get; private set; }
                public GettedImageEventArgs(BitmapImage image)
                {
                    this.ResultImage = image;
                }
            }
            public event EventHandler<GettedImageEventArgs> GettedImage;
            private const string tempPath = "tmp";
            private const string apiUrl = "http://atmb.top/api/ad/";
            public async void Work()
            {
                var localImage = await Task.Run(() =>
                {
                    return GetLocalImage();
                });
                if (localImage != null)
                {
                    GettedImage?.Invoke(this, new GettedImageEventArgs(localImage));
                }
                var remoteImage = await Task.Run(() =>
                {
                    return GetRemoteImage();
                });
                if (remoteImage != null)
                {
                    GettedImage?.Invoke(this, new GettedImageEventArgs(remoteImage));
                }
            }
            private BitmapImage GetRemoteImage()
            {
                var remoteAdInfo = GetRemoteAdInfo();
                var needDownloadNewFile = File.Exists(tempPath + "\\" + remoteAdInfo.Name);
                if (needDownloadNewFile)
                {
                    string path = Download(remoteAdInfo);
                    return new BitmapImage(new Uri(path, UriKind.Relative));
                }
                return null;
            }
            private BitmapImage GetLocalImage()
            {
                try
                {
                    if (Config.LastAdPath != "unknow")
                    {
                        return new BitmapImage(new Uri(Config.LastAdPath, UriKind.Relative));
                    }
                    return null;
                }
                catch (Exception e)
                {
                    Logger.T("a exception happend on Geting local image", e);
                    return null;
                }
            }

            private static readonly WebClient webClient = new WebClient();
            private static string Download(RemoteAdInfo info)
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                webClient.DownloadFile(info.Link, $"{tempPath}\\{info.Name}");
                return $"{tempPath}\\{info.Name}";
            }
            private static RemoteAdInfo GetRemoteAdInfo()
            {
                string html = webClient.DownloadString(apiUrl);
                return (RemoteAdInfo)JsonConvert.DeserializeObject(html, typeof(RemoteAdInfo));
            }
        }


        public AdBox()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AdImageGetter imgGetter = new AdImageGetter();
            imgGetter.GettedImage += (s, ea) =>
            {
                ImgMain.Source = ea.ResultImage;
            };
            imgGetter.Work();
        }
    }
}
