using AutumnBox.GUI.Util.PaidVersion;
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

namespace AutumnBox.GUI.Windows.PaidVersion
{
    /// <summary>
    /// AccountWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AccountWindow : Window
    {
        private readonly IAccountManager am;
        internal AccountWindow(IAccountManager am)
        {
            InitializeComponent();
            this.am = am;
            am.Login("zsh2401","6808412");
            Date.Text = am.Current.ExpiredDate.ToString() ;
        }
    }
}
