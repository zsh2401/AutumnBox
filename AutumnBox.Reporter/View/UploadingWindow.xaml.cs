using AutumnBox.Reporter.Model;
using AutumnBox.Reporter.ViewModel;
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
using System.Windows.Shapes;

namespace AutumnBox.Reporter.View
{
    /// <summary>
    /// UploadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UploadingWindow : Window
    {
        private readonly ReportHeader header;
        private readonly IEnumerable<Log> logs;

        internal UploadingWindow(ReportHeader header,IEnumerable<Log> logs)
        {
            InitializeComponent();
            this.header = header ?? throw new ArgumentNullException(nameof(header));
            this.logs = logs ?? throw new ArgumentNullException(nameof(logs));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as VMUploader).Stop();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            (DataContext as VMUploader).StartUpload(header, logs);
        }
    }
}
