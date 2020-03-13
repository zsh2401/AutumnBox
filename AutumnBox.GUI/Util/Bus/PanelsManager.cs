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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;

namespace AutumnBox.GUI.Util.Bus
{
    internal static class PanelsManager
    {
        public class ViewContainer
        {
            public int Priority { get; }
            public object View { get; }
            public ViewContainer(object view, int priority = 0)
            {
                View = view;
                this.Priority = priority;
            }
        }
        public static ObservableCollection<ViewContainer> Views { get; } = new ObservableCollection<ViewContainer>();
        static PanelsManager()
        {
            Views.Add(new ViewContainer(new DeviceSelector()));
            Views.Add(new ViewContainer(new DeviceDash()));
            Views.Add(new ViewContainer(new TextBlock() { Text = "You can really dance!", Height = 300 }, 20));
            //Sort();
        }
        private static bool isSorting = false;
        public static void Sort()
        {
            if (!isSorting) isSorting = true;
            else return;
            var ordered = from view in Views
                          orderby view.Priority descending
                          select view;
            Views.Clear();
            foreach (var view in ordered)
            {
                Views.Add(view);
            }
            isSorting = false;
        }
    }
}
