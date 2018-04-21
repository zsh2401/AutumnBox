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
            Process.Start("http://www.atmb.top/download/dotnet/");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetText("zsh2401@163.com");
            button1.Text = "已复制";
        }
    }
}
