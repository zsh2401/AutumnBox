using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.Impl
{
    class UpdaterImpl : IUpdater
    {
        public async void Start(IProgressWindow prgWin)
        {
            prgWin.SetTip("哈哈", 20);
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });
            prgWin.SetTip("哈哈X", 50);
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
            });
            prgWin.Finish();
        }
    }
}
