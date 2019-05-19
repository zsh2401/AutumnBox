using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Model
{
    interface ISliceController
    {
        string Title { get; }
        object View { get; }
        string Id { get; }
    }
}
