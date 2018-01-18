using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AutumnBox.UnableToStartHelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Debug.WriteLine(Environment.Version);
            Application.Run(new Form1());
        }

    }
}
