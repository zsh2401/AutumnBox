using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Basic.Function.RunningManager;
using AutumnBox.Helper;
using AutumnBox.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace AutumnBox.UI.Grids
{
    /// <summary>
    /// PoweronFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class PoweronFunctions : UserControl
    {
        public PoweronFunctions()
        {
            InitializeComponent();
        }

        private void ButtonStartBrventService_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show( App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgStartBrventTip"].ToString())) return;
            BreventServiceActivator activator = new BreventServiceActivator();
            var rm = App.SelectedDevice.GetRunningManger(activator);
            rm.FuncEvents.Finished += ((StartWindow)App.OwnerWindow).FuncFinish;
            rm.FuncStart();
            UIHelper.ShowRateBox(rm);
        }

        private void ButtonPushFileToSdcard_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "刷机包/压缩包文件(*.zip)|*.zip|镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                FileSender fs = new FileSender(new FileArgs { files = new string[] { fileDialog.FileName } });
                RunningManager rm = App.SelectedDevice.GetRunningManger(fs);
                rm.FuncEvents.Finished +=(App.OwnerWindow as StartWindow).FuncFinish;
                rm.FuncStart();
                new FileSendingWindow(rm).ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void ButtonInstallApk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonScreentShot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonStartBrventService_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonUnlockMiSystem_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show( FindResource("Notice").ToString(), FindResource("msgUnlockXiaomiSystemTip").ToString())) return;
            MMessageBox.ShowDialog( FindResource("Notice").ToString(), FindResource("msgIfAllOK").ToString());
            XiaomiSystemUnlocker unlocker = new XiaomiSystemUnlocker();
            var rm = App.SelectedDevice.GetRunningManger(unlocker);
            rm.FuncEvents.Finished += App.OwnerWindow.FuncFinish;
            rm.FuncStart();
            UIHelper.ShowRateBox(rm);
        }
    }
}
