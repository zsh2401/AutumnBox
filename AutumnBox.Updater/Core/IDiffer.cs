using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core
{
    interface IDiffer
    {
        IEnumerable<IFile> Diff(IEnumerable<IFile> remoteFiles,IEnumerable<FileInfo> localFiles);
    }
}
