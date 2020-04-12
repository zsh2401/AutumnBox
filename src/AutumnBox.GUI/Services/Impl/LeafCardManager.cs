using AutumnBox.Leafx.Container.Support;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILeafCardManager))]
    class LeafCardManager : ILeafCardManager
    {
        public ObservableCollection<object> Views { get; } = new ObservableCollection<object>();
        private List<(object, int)> viewsWithP = new List<(object, int)>();
        public void Add(object view, int level)
        {
            viewsWithP.Add((view, level));
            Reorder();
        }

        private void Reorder()
        {
            var result = from view in viewsWithP
                         orderby view.Item2 descending
                         select view.Item1;
            App.Current.Dispatcher.Invoke(() =>
            {
                Views.Clear();
                Views.Concat(result);
            });
        }
        public void Remove(object view)
        {
            int index = viewsWithP.FindIndex(v => v.Item1 == view);
            if (index != -1)
            {
                viewsWithP.RemoveAt(index);
            }
        }
    }
}
