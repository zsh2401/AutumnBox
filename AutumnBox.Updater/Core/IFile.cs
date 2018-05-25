using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core
{
    interface IFile
    {
        string Md5 { get; }
        string DownloadUrl { get; }
        string LocalPath { get; }
    }
}
