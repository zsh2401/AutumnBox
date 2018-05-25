/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 19:04:20 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.Impl
{
    class NpdaterDownloader : IDownloader
    {
        public event EventHandler AllFinished;
        public event EventHandler<FileDownloadedEventArgs> DownloadedAFile;

        private readonly WebClient client = new WebClient();
        public void Download(IEnumerable<IFile> files)
        {
            foreach (var f in files) {
                client.DownloadFile(f.DownloadUrl,f.LocalPath);
                DownloadedAFile?.Invoke(this,new FileDownloadedEventArgs(f));
            }
            AllFinished?.Invoke(this, new EventArgs());
        }
    }
}
