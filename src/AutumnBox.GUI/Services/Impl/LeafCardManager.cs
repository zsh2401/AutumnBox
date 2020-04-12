using AutumnBox.GUI.View.Controls;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILeafCardManager))]
    class LeafCardManager : ILeafCardManager
    {
        public ObservableCollection<ViewWrapper> Views { get; } = new ObservableCollection<ViewWrapper>();
        private readonly List<(object, int)> viewsWithP = new List<(object, int)>();

        public void Add(object view, int level)
        {
            viewsWithP.Add((view, level));
            Reorder();
        }

        private void Reorder()
        {
            var result = from view in viewsWithP
                         orderby view.Item2 descending
                         select new ViewWrapper(view.Item1);
            App.Current.Dispatcher.Invoke(() =>
            {
                Views.Clear();
                result.All((r) =>
                {
                    Views.Add(r);
                    return true;
                });
                SLogger<LeafCardManager>.Info($"There is {Views.Count()} leaf card");
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
