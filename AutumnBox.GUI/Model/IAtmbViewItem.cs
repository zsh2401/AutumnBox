using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Model
{
    interface IAtmbViewItem
    {
        int Priority { get; }
        string Name { get; }
        object Icon { get; }
        UIElement GetView();
    }
}
