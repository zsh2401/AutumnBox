/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 20:39:18
** filename: ImagesGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Resources.Images
{
    public static class ImageGetter
    {
        //private static readonly Assembly currentAssembly;
        private const string prefix = "/AutumnBox;component/Resources/Images/";
        static ImageGetter() {
            //currentAssembly = Assembly.GetExecutingAssembly();
        }
        public static BitmapImage Get(string relativePath) {
            BitmapImage bmp = new BitmapImage(new Uri($"{prefix}{relativePath}", UriKind.RelativeOrAbsolute));
            //using (var imageStream = currentAssembly.GetManifestResourceStream(prefix+ relativePath))
            //{
            //    bmp.BeginInit();
            //    bmp.StreamSource = imageStream;
            //    bmp.EndInit();
            //}
            return bmp;
        }
    }
}
