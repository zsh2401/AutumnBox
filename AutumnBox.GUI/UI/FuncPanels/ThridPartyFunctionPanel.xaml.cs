using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.UI.CstPanels;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Warpper;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// ThridPartyFunctionPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ThridPartyFunctionPanel : UserControl, IRefreshable
    {
        private class WarpperWarpper
        {
            public IExtensionWarpper Warpper { get; private set; }
            public string Name => Warpper.Name;
            public ImageSource Icon
            {
                get
                {
                    if (icon == null) LoadIcon();
                    return icon;
                }
            }
            private ImageSource icon;
            public WarpperWarpper(IExtensionWarpper warpper)
            {
                this.Warpper = warpper;
            }
            private void LoadIcon()
            {
                if (Warpper.Icon == null)
                {
                    icon = App.Current.Resources["DefaultExtensionIcon"] as ImageSource;
                }
                else
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = new MemoryStream(Warpper.Icon);
                    bmp.EndInit();
                    bmp.Freeze();
                    icon = bmp;
                }
            }
            public static IEnumerable<WarpperWarpper> From(IEnumerable<IExtensionWarpper> warppers)
            {
                List<WarpperWarpper> result = new List<WarpperWarpper>();
                foreach (var warpper in warppers)
                {
                    result.Add(new WarpperWarpper(warpper));
                }
                return result;
            }
        }
        internal static ThridPartyFunctionPanel Single { get; private set; }
        private DeviceBasicInfo currentDevice;
        private readonly FastPanel runningPanel;
        private readonly ExtensionRuningPanel innerRunningPanel;
        public ThridPartyFunctionPanel()
        {
            InitializeComponent();
            innerRunningPanel = new ExtensionRuningPanel();
            runningPanel = new FastPanel(GridContainer, innerRunningPanel);
            Single = this;
            GridInfo.Visibility = Visibility.Collapsed;
            TxtNothing.Visibility = Visibility.Visible;
            TBSdk.Text = string.Format(TBSdk.Text, BuildInfo.SDK_VERSION);
            LanguageHelper.LanguageChanged += (s, e) =>
            {
                SetPanelByExtension(null);
                TBSdk.Text = string.Format(App.Current.Resources["lbApiLevel"].ToString(), BuildInfo.SDK_VERSION);
            };
            ListBoxModule.ItemsSource = Manager.InternalManager.Warppers;
        }

        private void SetPanelByExtension(IExtensionWarpper warpper)
        {
            if (warpper == null)//如果传入空,则视为未选中任何拓展模块
            {
                //隐藏拓展模块显示布局
                GridInfo.Visibility = Visibility.Collapsed;
                TxtNothing.Visibility = Visibility.Visible;
            }
            else
            {
                //显示拓展模块显示布局
                GridInfo.Visibility = Visibility.Visible;
                TxtNothing.Visibility = Visibility.Collapsed;
                //设置信息
                TBDesc.Text = warpper.Desc;
                TBName.Text = warpper.Name;
                //检查模块是否已经准备好了,并且设置按钮状态
                SetBtnByForerunCheckResult(warpper.ForerunCheck(currentDevice));
            }
        }
        //private void SetIconBy(IExtensionWarpper warpper)
        //{
        //    IMGIcon.Source = null;
        //    if (warpper == null
        //        || warpper.Icon == null
        //        || warpper.Icon.Length == 0
        //        ) return;
        //    byte[] iconBytes = warpper.Icon;
        //    if (iconBytes.Length == 0) return;
        //    try
        //    {
        //        BitmapImage bmp = new BitmapImage();
        //        bmp.BeginInit();
        //        bmp.StreamSource = new MemoryStream(warpper.Icon);
        //        bmp.EndInit();
        //        bmp.Freeze();
        //        IMGIcon.Source = bmp;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Info(this, ex);
        //    }
        //}
        private void SetBtnByForerunCheckResult(ForerunCheckResult result)
        {
            BtnRun.IsEnabled = (result == ForerunCheckResult.Ok);
            switch (result)
            {
                case ForerunCheckResult.Ok:
                    BtnRun.Content = App.Current.Resources["btnRunExtension"];
                    break;
                default:
                    BtnRun.Content = App.Current.Resources["btnCannotRunExtension"];
                    break;
            }
        }
        public void Refresh()
        {
            this.Refresh(currentDevice);
        }
        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            ListBoxModule.ItemsSource = WarpperWarpper.From(Manager.InternalManager.Warppers);
            currentDevice = deviceSimpleInfo;
            ListBoxModule.SelectedIndex = -1;
        }
        public void Reset()
        {
            Refresh(new DeviceBasicInfo()
            {
                Serial = null,
                State = DeviceState.None
            });
            ListBoxModule.SelectedIndex = -1;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetPanelByExtension((ListBoxModule.SelectedItem as WarpperWarpper)?.Warpper);
        }
        private void ShowRunningBox(IExtensionWarpper wapper)
        {
            innerRunningPanel.CurrentRunningName = wapper.Name;
            runningPanel.Display();
        }
        private void CloseRunningBox()
        {
            runningPanel.Hide();
        }
        private async void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            IExtensionWarpper warpper = (ListBoxModule.SelectedItem as WarpperWarpper).Warpper;
            warpper.RunAsync(currentDevice);
        }

        private void BtnOpenModuleFloder_Click(object sender, RoutedEventArgs e) =>
            Process.Start(Manager.InternalManager.ExtensionPath);
    }
}
