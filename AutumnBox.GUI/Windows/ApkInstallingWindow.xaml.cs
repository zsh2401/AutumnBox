using AutumnBox.Basic.Flows;
using AutumnBox.Support.CstmDebug;
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
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// ApkInstallingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ApkInstallingWindow : Window
    {
        private readonly ApkInstaller installer;
        public ApkInstallingWindow(ApkInstaller installer, List<FileInfo> files)
        {
            InitializeComponent();
            this.installer = installer;
            TBCountOfAll.Text = (files.Count + 1).ToString();
            TBCountOfInstalled.Text = 0.ToString();
            this.installer.AApkIstanlltionCompleted += (s, e) =>
            {
                ProgressAdd();
                if (!e.IsSuccess)//如果这次安装是失败的
                {//询问用户是否在安装失败的情况下继续
                    e.NeedContinue = ShowChoiceLabelForContinueOnError();
                }
            };
        }
        private void ProgressAdd()
        {
            Logger.D("Progress add one");
            TBCountOfInstalled.Text = (Convert.ToInt32(TBCountOfInstalled.Text) + 1).ToString();
        }
        private bool ShowChoiceLabelForContinueOnError()
        {
            Logger.D("Show choice label continue on error");
            return true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            installer.ForceStop();
            Close();
        }
    }
}
