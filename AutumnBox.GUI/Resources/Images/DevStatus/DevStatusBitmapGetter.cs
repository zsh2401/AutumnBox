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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
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
            BitmapSource bmp = null;
            switch (status)
            {
                case DeviceState.Poweron:
                    bmp = ((BitmapFrame)App.Current.Resources["ImgPoweron"]).Clone();
                    break;
                case DeviceState.Sideload:
                case DeviceState.Recovery:
                    bmp = ((BitmapFrame)App.Current.Resources["ImgRecovery"]).Clone();
                    break;
                case DeviceState.Fastboot:
                    bmp = ((BitmapFrame)App.Current.Resources["ImgFastboot"]).Clone();
                    break;
                default:
                    bmp = ((BitmapFrame)App.Current.Resources["ImgNoDevice"]).Clone();
                    break;
            }
            return Paint(bmp);
        }
        private static BitmapSource Paint(BitmapImage src)
        {


        }

        private static Bitmap ToBitmap(this BitmapSource src)
        {
            src.
        }
        private static BitmapSource ToBitmapSource(this Bitmap bmp)
        {
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(
                bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                return null;
            }
        }
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
    }
}
