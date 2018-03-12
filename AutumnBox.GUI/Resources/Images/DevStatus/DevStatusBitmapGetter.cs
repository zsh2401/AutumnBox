/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 19:48:02
** filename: DevStatusBitmapGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Resources.Images;
using AutumnBox.Support.Log;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Resources
{
    internal static class DevStatusBitmapGetter
    {
        private static readonly Assembly currentAssembly;
        private const string rec = "DevStatus/rec.png";
        private const string fastboot = "DevStatus/fastboot.png";
        private const string poweron = "DevStatus/poweron.png";
        private const string nodev = "DevStatus/no_dev.png";
        static DevStatusBitmapGetter()
        {
            currentAssembly = Assembly.GetExecutingAssembly();
        }
        public static BitmapSource Get(DeviceState status)
        {
            switch (status)
            {
                case DeviceState.Poweron:
                    return ((BitmapFrame)App.Current.Resources["ImgPoweron"]).Clone();
                case DeviceState.Sideload:
                case DeviceState.Recovery:
                    return ((BitmapFrame)App.Current.Resources["ImgRecovery"]).Clone();
                case DeviceState.Fastboot:
                    return ((BitmapFrame)App.Current.Resources["ImgFastboot"]).Clone();
                default:
                    return ((BitmapFrame)App.Current.Resources["ImgNoDevice"]).Clone() ;
            }
        }
        private static BitmapImage Paint(BitmapImage src)
        {
            Bitmap bmp = new Bitmap(src.Clone().StreamSource);
            Color crtPixel;
            for (int crtX = 0; crtX < bmp.Width; crtX++)
            {
                for (int crtY = 0; crtY < bmp.Height; crtY++)
                {
                    crtPixel = bmp.GetPixel(crtX, crtY);
                    if (crtPixel == Color.White)
                    {
                        bmp.SetPixel(crtX, crtY, (Color)App.Current.Resources["ColorPrimary"]);
                    }
                }
            }
            var result = new BitmapImage();
            using (MemoryStream ms = new MemoryStream())
            {
                result.BeginInit();
                bmp.Save(result.StreamSource, ImageFormat.Png);
                result.EndInit();
            }
            return result;
        }
    }
}
