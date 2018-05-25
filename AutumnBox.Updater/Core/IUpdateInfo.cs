using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core
{
    interface IUpdateInfo
    {
        Version Verision { get;  }
        string UpdateContent { get;  }
        IEnumerable<IFile> Files { get;  }
    }
}
