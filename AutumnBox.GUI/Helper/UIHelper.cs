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
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Function;
using AutumnBox.GUI.Cfg;
using AutumnBox.GUI.UI.Grids;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.CstmDebug;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Helper
{
    /// <summary>
    /// UI帮助类,与UI相关的静态函数
    /// </summary>
    public static class UIHelper
    {
        public static string GetString(string key)
        {
            object tmp = App.Current.Resources[key];
            return tmp == null ? key : tmp.ToString();
        }
        public static void SetOwnerTransparency(byte A)
        {
            Color now = ((SolidColorBrush)App.Current.Resources["BackBrush"]).Color;
            App.Current.MainWindow.Background = new SolidColorBrush(Color.FromArgb(A, now.R, now.G, now.B));
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
        /// <summary>
        /// DragMove
        /// </summary>
        /// <param name="m"></param>
        /// <param name="e"></param>
        public static void DragMove(Window m, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                m.DragMove();
            }
        }
        /// <summary>
        /// DragMove
        /// </summary>
        /// <param name="m"></param>
        /// <param name="e"></param>
        public static void DragMove(Window m, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                m.DragMove();
            }
        }
        /// <summary>
        /// 一个进度窗
        /// </summary>
        private static RateBox rateBox;
        /// <summary>
        /// 为了保证不同时出现多个ratebox而设计的函数
        /// </summary>
        /// <param name="rm"></param>
        public static void ShowRateBox(FunctionModuleProxy fmp = null)
        {
            try
            {
                if (fmp == null)
                {
                    rateBox = new RateBox();
                    rateBox.ShowDialog();
                    return;
                }
                if (rateBox.IsActive) rateBox.Close();
                rateBox = new RateBox(fmp);
                rateBox.ShowDialog();
            }
            catch
            {
                rateBox = new RateBox(fmp);
                rateBox.ShowDialog();
            }
        }
        public static void ShowRateBox(IForceStoppable forceStoppable)
        {
            if (rateBox?.IsActive == true) return;
            if (forceStoppable != null)
                rateBox = new RateBox(forceStoppable);
#if FINISHED
            else
                rateBox = new RateBox();
#endif
            rateBox.ShowDialog();

        }
        public static void ShowChoiceGrid(ChoiceData data, Action<ChoiceResult> callback)
        {
            //try
            //{
            //    lock (choicer)
            //    {
            //        choicer = new ChoiceGrid(((MainWindow)App.Current.MainWindow).GridMainTab, data);
            //        choicer.Show(callback);
            //    }
            //}
            //catch (ArgumentNullException)
            //{
            //    choicer = new ChoiceGrid(((MainWindow)App.Current.MainWindow).GridMainTab, data);
            //    choicer.Show(callback);
            //}
        }
        /// <summary>
        /// 关闭进度窗
        /// </summary>
        public static void CloseRateBox()
        {
            try
            {
                rateBox.Close();
            }
            catch { }
        }

        private static bool _isForceStoped = false;
        private static ChoiceGrid _choicer = new ChoiceGrid(new Grid(), new ChoiceData());
        public static ChoiceResult RShowChoiceGrid(string titleKey, string textKey, string key_TextBtnLeft = null, string key_TextBtnRight = null)
        {
            bool _hasHide = false;
            ChoiceResult result = ChoiceResult.Cancel;
            var data = Make(titleKey, textKey, key_TextBtnLeft, key_TextBtnRight);
            App.Current.Dispatcher.Invoke(() =>
            {
                lock (_choicer)
                {
                    _isForceStoped = false;
                    _choicer = new ChoiceGrid(((MainWindow)App.Current.MainWindow).GridMainTab, data);
                    _choicer.Show((r) =>
                    {
                        _hasHide = true;
                        result = r;
                    });
                }
            });
            while (!_hasHide && !_isForceStoped) ;
            return _isForceStoped ? ChoiceResult.Cancel:result ;
        }
        public static bool ShowChoiceGrid(string titleKey, string textKey, string key_TextBtnLeft = null, string key_TextBtnRight = null)
        {
            return RShowChoiceGrid(titleKey, textKey, key_TextBtnLeft, key_TextBtnRight) == ChoiceResult.Right;
        }
        public static void HideChoiceGrid()
        {
            _isForceStoped = true;
        }
        private static ChoiceData Make(string titleKey, string textKey, string key_TextBtnLeft = null, string key_TextBtnRight = null)
        {
            return new ChoiceData()
            {
                Title = GetString(titleKey),
                Text = GetString(textKey),
                TextBtnLeft = (key_TextBtnLeft == null) ? UIHelper.GetString("btnCancel") : UIHelper.GetString(key_TextBtnLeft),
                TextBtnRight = (key_TextBtnRight == null) ? UIHelper.GetString("btnOK") : UIHelper.GetString(key_TextBtnRight),
            };
        }

    }
}
