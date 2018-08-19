/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/20 3:29:09 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    class VMMore : ViewModelBase
    {
        public string ApiVersion { get; set; }
        public FlexiableCommand OpenExtFloder { get; set; }
        public FlexiableCommand OpenUrl { get; set; }
        public VMMore()
        {
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
