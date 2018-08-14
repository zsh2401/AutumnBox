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
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Warpper;

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// ExtensionPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ExtensionPanel : UserControl, IExtPanel
    {
        private class WarpperWarpper
        {
            public IExtensionWarpper Warpper { get; private set; }
            public string Name => Warpper.Info.Name;
            public ImageSource Icon
            {
                get
                {
                    if (icon == null) LoadIcon();
                    return icon;
                }
            }
            private ImageSource icon;
            private WarpperWarpper(IExtensionWarpper warpper)
            {
                this.Warpper = warpper;
            }
            private void LoadIcon()
            {
                if (Warpper.Info.Icon == null)
                {
                    icon = App.Current.Resources["DefaultExtensionIcon"] as ImageSource;
                }
                else
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = new MemoryStream(Warpper.Info.Icon);
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
        public DeviceState TargetDeviceState { get; set; }
        private DeviceBasicInfo CurrentDevice { get; set; }
        public ExtensionPanel()
        {
            InitializeComponent();
            LBMain.SelectionChanged += LBMain_SelectionChanged;
            BtnRun.Click += BtnRun_Click;
            SetInfoByWarpper(null);
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            (LBMain.SelectedItem as WarpperWarpper)?.Warpper.RunAsync(CurrentDevice);
        }

        private void LBMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetInfoByWarpper((LBMain.SelectedItem as WarpperWarpper)?.Warpper);
        }
        private void SetInfoByWarpper(IExtensionWarpper warpper)
        {
            if (warpper == null)
            {
                GBExtInfo.Visibility = Visibility.Collapsed;
                return;
            }
            GBExtInfo.Visibility = Visibility.Visible;
            TBDesc.Text = warpper.Info.FormatedDesc;
            GBExtInfo.Header = warpper.Info.Name;
            if (TargetHas(CurrentDevice.State))
            {
                BtnRun.IsEnabled = true;
                BtnRun.Content = App.Current.Resources["btnRunExtension"];
            }
            else
            {
                BtnRun.Content = App.Current.Resources["btnCannotRunExtension"];
                BtnRun.IsEnabled = false;
            }
        }

        public void Set(IExtensionWarpper[] warppers, DeviceBasicInfo device)
        {
            var filted = from warpper in warppers
                         where TargetHas(warpper.Info.RequiredDeviceStates)
                         select warpper;
            LBMain.ItemsSource = WarpperWarpper.From(filted);
            LBMain.SelectedIndex = -1;
            CurrentDevice = device;
        }

        public void Set(IExtensionWarpper[] warppers)
        {
            var filted = from warpper in warppers
                         where TargetHas(warpper.Info.RequiredDeviceStates)
                         select warpper;
            LBMain.ItemsSource = WarpperWarpper.From(filted);
            LBMain.SelectedIndex = -1;
            CurrentDevice = new DeviceBasicInfo()
            {
                State = DeviceState.None
            };
        }
        private bool TargetHas(DeviceState state)
        {
            return (TargetDeviceState & state) != 0;
        }
    }
}
