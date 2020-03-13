/*

* ==============================================================================
*
* Filename: PanelsManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/14 1:40:31
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.View.Panel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AutumnBox.GUI.Util.Bus
{
    internal static class PanelsManager
    {
        public class ViewContainer
        {
            public object View { get; }
            public ViewContainer(object view)
            {
                View = view;
            }
        }
        public static ObservableCollection<ViewContainer> Views { get; } = new ObservableCollection<ViewContainer>();
        static PanelsManager()
        {
            Views.Add(new ViewContainer(new TextBlock() { Text = "You can really dance!", Height = 300 }));
            Views.Add(new ViewContainer(new DeviceSelector()));
            Views.Add(new ViewContainer(new DeviceDash()));
     
        }
    }
}
