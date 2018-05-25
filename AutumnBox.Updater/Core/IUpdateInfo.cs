using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core
{
    interface IUpdateInfo
    {
        Version Verision { get; set; }
        string UpdateContent { get; set; }
        IEnumerable<IFile> Files { get; set; }
    }
}
