using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Net.HomeContent
{
    interface IHomeContentGetter
    {
        bool TryGet(out object result);
        object Default();
    }
}
