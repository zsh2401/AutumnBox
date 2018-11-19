/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/20 3:29:09 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Management;
using System.Diagnostics;

namespace AutumnBox.GUI.ViewModel
{
    class VMMore : ViewModelBase
    {
        public string ApiVersion { get; set; }
        public FlexiableCommand OpenLibsView { get; set; }
        public FlexiableCommand OpenExtFloder { get; set; }
        public FlexiableCommand OpenUrl { get; set; }
        public VMMore()
        {
            OpenLibsView = new FlexiableCommand(() =>
            {
                new LibsWindow().ShowDialog();
            });
            OpenExtFloder = new FlexiableCommand(() =>
            {
                Process.Start(Manager.InternalManager.ExtensionPath);
            });
            OpenUrl = new FlexiableCommand((para) =>
            {
                try
                {
                    Process.Start(para.ToString());
                }
                catch { }
            });
            ApiVersion = BuildInfo.SDK_VERSION.ToString();
        }
    }
}
