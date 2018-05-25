using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core
{
    class FileDownloadedEventArgs : EventArgs
    {
        public readonly IFile File;
        public FileDownloadedEventArgs(IFile file)
        {
            this.File = file;
        }
    }
    interface IDownloader
    {
        event EventHandler AllFinished;
        event EventHandler<FileDownloadedEventArgs> DownloadedAFile;
        void Download(IEnumerable<IFile> files);
    }
}
