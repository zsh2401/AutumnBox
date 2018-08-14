/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/14 16:15:42 (UTC +8:00)
** desc： ...
*************************************************/
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Dialogs
{
    static class DialogManager
    {
        public static void ShowMessageDialog()
        {
            DialogHost.Show(new MessageDialog());
        }
    }
}
