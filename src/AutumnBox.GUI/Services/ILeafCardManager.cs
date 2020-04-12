using System.Collections.ObjectModel;

namespace AutumnBox.GUI.Services
{
    interface ILeafCardManager
    {
        ObservableCollection<object> Views { get; }

        void Add(object view, int level);
        void Remove(object view);
    }
}
