using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// ApplicationPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ApplicationPanel : UserControl
    {
        private readonly DeviceSerial serial;
        private readonly PackageBasicInfo pkgInfo;
        //bool参数的作用是,是否删除当前项的App选项
        private readonly Action<bool> closeCallback;
        public ApplicationPanel(DeviceSerial device,PackageBasicInfo info, Action<bool> closeCallback)
        {
            InitializeComponent();
            this.closeCallback = closeCallback;
            this.pkgInfo = info;
            this.serial = device;
            TBAppName.Text = info.Name;
            TBPkgName.Text = info.PackageName;
            SetIcon();
            SetUsedSpaceInfo();
        }
        private async void SetIcon() {
            var datas = await Task.Run(() =>
            {
                return PackageManager.GetIcon(serial, pkgInfo.PackageName);
            });
            if (datas != null) {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(datas);
                bmp.EndInit();
                ImgAppIcon.Source = bmp;
            }
        }
        private async void SetUsedSpaceInfo() {
            var info = await Task.Run(()=> {
                return PackageManager.GetAppUsedSpace(serial,pkgInfo.PackageName);
            });
            TBCacheSize.Text = info.CacheSize.ToString();
            TBCodeSize.Text = info.CodeSize.ToString();
            TBDataSize.Text = info.DataSize.ToString();
            TBTotalSize.Text = info.TotalSize.ToString();
        }

        private void BtnUninstall_Click(object sender, RoutedEventArgs e)
        {
            
            var success  = PackageManager.UninstallApp(serial,pkgInfo.PackageName);
            if (success) {
                closeCallback(true);
            }
        }
    }
}
