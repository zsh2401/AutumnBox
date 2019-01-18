/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 18:54:24 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Model;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.View.Windows;
using MaterialDesignThemes.Wpf;
using System;

namespace AutumnBox.GUI.View
{
    internal static class MaterialDialog
    {

        public static void ShowChoiceDialog(ChoicerContentStartArgs args)
        {
            var content = new ContentChoice(args);
            (App.Current.MainWindow as MainWindow).DialogHost.ShowDialog(content, new DialogOpenedEventHandler((s, e) =>
            {
                args.CloseDialog = () =>
                {
                    try
                    {
                        e.Session.Close();
                    }
                    catch (Exception ex)
                    {
                        SLogger.Warn(senderOrTag: "ChoiceDialog", content: "an error happend on closing choice dialog", ex: ex);
                    }
                };
            }));
        }
    }
}
