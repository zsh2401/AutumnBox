/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 17:39:20 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Model;
using AutumnBox.GUI.View.DialogContent;
using MaterialDesignThemes.Wpf;
using System;

namespace AutumnBox.GUI.Helper
{


    static class ChoiceDialog
    {

        public static void Show(ChoicerContentStartArgs args)
        {
            var content = new ContentChoice(args);
            DialogHost.Show(content, new DialogOpenedEventHandler((s, e) =>
            {
                args.CloseDialog = () => e.Session.Close();
            }));
        }
    }
}
