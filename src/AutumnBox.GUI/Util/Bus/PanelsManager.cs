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
using System.Collections.ObjectModel;
using System.Linq;
using AutumnBox.GUI.MVVM;
using System;

namespace AutumnBox.GUI.Util.Bus
{
    [Obsolete("Use service to instead", true)]
    internal class PanelsManager : NotificationObject
    {
        public ObservableCollection<ViewContainer> Views
        {
            get => _views; set
            {
                _views = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<ViewContainer> _views;

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
        public static PanelsManager Instance { get; }
        static PanelsManager()
        {
            Instance = new PanelsManager();
        }
        private PanelsManager()
        {
            Views = new ObservableCollection<ViewContainer>();
            Views.Add(new ViewContainer(new DeviceSelector()));
            Views.Add(new ViewContainer(new DeviceDash()));
            Sort();
            Views.CollectionChanged += (s, e) => Sort();
        }
        public void Sort()
        {
            var ordered = from view in Views
                          orderby view.Priority descending
                          select view;
            Views = new ObservableCollection<ViewContainer>(ordered);
        }
    }
}
