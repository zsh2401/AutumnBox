using AutumnBox.Basic.Flows;
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
        public ApkInstallingWindow(ApkInstaller installer,int filesCount,List<FileInfo> files)
        {
            InitializeComponent();
            this.installer = installer;
            this.installer.AApkIstanlltionCompleted += (s,e) => {
                
            };
        }
        private void ProgressAdd() {

        }
        private void Fuck()
        {
            //TODO
        }
    }
}
