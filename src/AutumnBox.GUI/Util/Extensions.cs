/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/18 21:06:37 (UTC +8:00)
** desc： ...
*************************************************/

using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Util
{
    public static class Extensions
    {
        public static ImageSource ToExtensionIcon(this byte[] bytes)
        {
            if (bytes == null)
            {
                return App.Current.Resources["DefaultExtensionIcon"] as ImageSource;
            }
            else
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(bytes);
                bmp.EndInit();
                bmp.Freeze();
                return bmp;
            }
        }
        public static void SuppressScriptErrors(this WebBrowser webBrowser, bool hide)
        {
            webBrowser.Navigating += (s, e) =>
            {
                var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (fiComWebBrowser == null)
                    return;

                object objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);
                if (objComWebBrowser == null)
                    return;

                objComWebBrowser.GetType().InvokeMember("Silent", System.Reflection.BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
            };
        }
    }
}
