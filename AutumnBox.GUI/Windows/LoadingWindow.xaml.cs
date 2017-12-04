/* =============================================================================*\
*
* Filename: RateBox.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 8/4/2017 12:12:58(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Function;
using AutumnBox.GUI.Helper;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// RateBox.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow(Window owner)
        {
            InitializeComponent();
            BtnCancel.Visibility = Visibility.Hidden;
            this.Owner = owner;
        }
        public new void ShowDialog()
        {
            BtnCancel.Visibility = Visibility.Hidden;
            base.ShowDialog();
        }
        public void ShowDialog(ICompletable flowOrFm)
        {
            BtnCancel.Visibility = Visibility.Visible;
            flowOrFm.NoArgFinished += (s, e) =>
            {
                this.Dispatcher.Invoke(()=> {
                    Close();
                });
            };
            this.BtnCancel.Click += (s, e) =>
            {
                flowOrFm.ForceStop();
                Close();
            };
            base.ShowDialog();
        }
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            UIHelper.DragMove(this, e);
        }
    }
}
