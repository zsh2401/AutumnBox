using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    interface ILeafCardManager
    {
        void Add(object view, int level);
        void Remove(object view);
    }
}
