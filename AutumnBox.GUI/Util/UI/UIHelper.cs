/* =============================================================================*\
*
* Filename: UIHelper.cs
* Description: 
*
* Version: 1.0
* Created: 9/23/2017 21:21:38(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Util.UI
{
    /// <summary>
    /// UI帮助类,与UI相关的静态函数
    /// </summary>
    [Obsolete("遗留代码")]
    internal static class UIHelper
    {
        private static SolidColorBrush CurrentBackgound
        {
            get
            {
                return (SolidColorBrush)App.Current.MainWindow.Background;
            }
        }
        public static string GetString(string key)
        {
            object tmp = App.Current.Resources[key];
            return tmp == null ? key : tmp.ToString();
        }
        public static void SetOwnerTransparency(byte A)
        {
#if ENABLE_BLUR
            Color currentColor = ((SolidColorBrush)App.Current.MainWindow.Background).Color;
            currentColor.A = A;
            App.Current.MainWindow.Background = new SolidColorBrush(currentColor);
#endif
        }
        /// <summary>
        /// 设置一个grid下的所有button的开启与否
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="status"></param>
        public static void SetGridButtonStatus(Grid grid, bool status)
        {
            var o = grid.Children;
            foreach (object a in o)
            {
                if (a is Button)
                {
                    (a as Button).IsEnabled = status;
                }
                else if (a is Grid)
                {
                    SetGridButtonStatus((a as Grid), status);
                }
            }
        }
        /// <summary>
        /// 设置一个grid下的所有labels的内容
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="content"></param>
        public static void SetGridLabelsContent(Grid grid, object content)
        {
            var o = grid.Children;
            foreach (object a in o)
            {
                if (a is Label)
                {
                    (a as Label).Content = content;
                }
            }
        }
        /// <summary>
        /// 设置一个panel下的所有button的内容
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="status"></param>
        public static void SetPanelButtonStatus(Panel panel, bool status)
        {
            foreach (object o in panel.Children)
            {
                if (o is Panel)
                {
                    SetPanelButtonStatus((Grid)o, status);
                }
                else if (o is Button)
                {
                    (o as Button).IsEnabled = status;
                }
            }
        }
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
    }
}
