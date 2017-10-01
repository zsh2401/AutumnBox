using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AutumnBox.Helper
{
    public static class UIHelper
    {
        /// <summary>
        /// 将Bitmap转为BitmapImage
        /// </summary>
        /// <param name="bitmap">一个bitmap对象</param>
        /// <returns>一个BitmapImage对象</returns>
        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }
        public static void DragMove(Window m, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                m.DragMove();
            }
        }
        public static void DragMove(Window m, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                m.DragMove();
            }
        }
        public static void ChangeButtonByStatus(Button[] buttons, DeviceStatus status) {
        }
        public static void ChangeImageByStatus(Image[] images, DeviceStatus status) {
        }
        public static void ShowRateBox(Window owner) { }
        public static void CloseRateBox(Window owner) { }
    }

}
