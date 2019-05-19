using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.ViewModel;
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

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// MainWindowV2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowV2
    {
        public MainWindowV2()
        {
            InitializeComponent();
        }

        private void PagesSourceChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "DisplayedPages") return;
            //foreach (var page in VM.DisplayedPages)
            //{
            //    PagesTabControl.Items.Add();
            //}
        }

    }
}
