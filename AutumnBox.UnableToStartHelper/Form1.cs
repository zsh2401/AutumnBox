using System.Diagnostics;
using System.Windows.Forms;

namespace AutumnBox.UnableToStartHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://atmb.top/download/dotnet/");
        }

    }
}
