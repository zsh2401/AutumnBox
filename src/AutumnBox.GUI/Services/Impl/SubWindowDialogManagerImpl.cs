using HandyControl.Controls;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services.Impl
{
    class SubWindowDialogManagerImpl : ISubWindowDialogManager
    {
        private const int STATE_CHECK_INTERVAL = 100;
        public Task<object> ShowDialog(string token, ISubWindowDialog dialog)
        {
            var hDialog = App.Current.Dispatcher.Invoke(() =>
            {
                return Dialog.Show(dialog.View, token);
            });
            return Task.Run(() =>
            {
                object result = null;
                dialog.Finished += (s, e) =>
                {
                    result = e.Value;
                    App.Current.Dispatcher.Invoke(() => { hDialog.Close(); });
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
