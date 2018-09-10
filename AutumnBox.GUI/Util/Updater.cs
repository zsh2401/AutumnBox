/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 18:53:28 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    public static class Updater
    {
        public static void CheckAndNotice()
        {
            UpdateChecker checker = new UpdateChecker();
            checker.RunAsync((e) =>
            {
                if (e.Version > Self.Version)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        new UpdateNoticeWindow(e).Show();
                    });
                }
            });
        }
    }
}
