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
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Function.RunningManager;
using AutumnBox.UI;
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
        public static string GetString(string key) {
            try {
                return App.cResources[key].ToString();
            } catch {
                return key;
            }
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
                else if (a is Grid) {
                    SetGridButtonStatus((a as Grid), status);
                }
            }
        }
        public static void SetGridLabelsContent(Grid grid, object content) {
            var o = grid.Children;
            foreach (object a in o)
            {
                if (a is Label)
                {
                    (a as Label).Content = content;
                }
            }
        }
        public static void SetPanelButtonStatus(Panel panel, bool status) {
            foreach (object o in panel.Children) {
                if (o is Panel)
                {
                    SetPanelButtonStatus((Grid)o, status);
                }
                else if (o is Button) {
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
        private static RateBox rateBox;
        public static void ShowRateBox(RunningManager rm=null) {
            try
            {
                if (rm == null)
                {
                    rateBox = new RateBox();
                    rateBox.ShowDialog();
                    return;
                }
                if (rateBox.IsActive) rateBox.Close();
                rateBox = new RateBox(rm);
                rateBox.ShowDialog();
            }
            catch
            {
                rateBox = new RateBox(rm);
                rateBox.ShowDialog();
            }
        }
        public static void CloseRateBox() {
            try
            {
               rateBox.Close();
            }
            catch { }
        }
    }

}
