/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 20:37:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.View.Windows;

namespace AutumnBox.GUI.Util
{
    static class ToastMotd
    {
        public static void Do()
        {
            new ToastMotdGetter().Try(e =>
            {
                if (!e.Enable) return;
                App.Current.Dispatcher.Invoke(() =>
                {
                    new MessageWindow()
                    {
                        MsgTitle = e.Title,
                        Message = e.Message,
                        Owner = App.Current.MainWindow
                    }.Show();
                });
            });
        }
    }
}
