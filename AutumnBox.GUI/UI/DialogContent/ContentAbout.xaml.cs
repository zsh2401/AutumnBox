using AutumnBox.GUI.Helper;
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

namespace AutumnBox.GUI.UI.DialogContent
{
    /// <summary>
    /// ContentAbout.xaml 的交互逻辑
    /// </summary>
    public partial class ContentAbout : UserControl
    {
        public ContentAbout()
        {
            InitializeComponent();
            LabelVersion.Content = SystemHelper.CurrentVersion.ToString();
#if !DEBUG
            LabelVersion.Content += "-release";
#else
            LabelVersion.Content += "-debug";
#endif
            LabelCompiledDate.Content = SystemHelper.CompiledDate.ToString("MM-dd-yyyy");
        }
    }
}
