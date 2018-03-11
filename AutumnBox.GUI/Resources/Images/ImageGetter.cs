/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 20:39:18
** filename: ImagesGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Resources.Images
{
    internal static class ImageGetter
    {
        private const string prefix = "/Resources/Images/";
        public static BitmapImage Get(string relativePath)
        {
            BitmapImage bmp = new BitmapImage(new Uri($"{prefix}{relativePath}", UriKind.RelativeOrAbsolute));
            return bmp;
        }
    }
}
