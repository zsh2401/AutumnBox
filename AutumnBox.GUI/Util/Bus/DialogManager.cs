using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Bus
{
    public static class DialogManager
    {
        private const int STATE_CHECK_INTERVAL = 200;
        public interface IDialog
        {
            object ViewContent { get; }
            event EventHandler<DialogClosedEventArgs> Closed;
        }
        public class DialogClosedEventArgs : EventArgs
        {
            public object Result { get; set; }
            public DialogClosedEventArgs() { }
            public DialogClosedEventArgs(object result)
            {
                Result = result;
            }
        }
        public static Task<object> Show(string token, IDialog dialog)
        {
            var hDialog = Dialog.Show(dialog.ViewContent, token);
            return Task.Run(() =>
            {
                object result = null;
                dialog.Closed += (s, e) =>
                {
                    App.Current.Dispatcher.Invoke(() => { hDialog.Close(); });
                    result = e.Result;
                };
                while (!App.Current.Dispatcher.Invoke(() => hDialog.IsClosed))
                {
                    Thread.Sleep(STATE_CHECK_INTERVAL);
                }
                return result;
            });
        }
    }
}
