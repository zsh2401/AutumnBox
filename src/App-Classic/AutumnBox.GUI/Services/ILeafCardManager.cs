using System.Collections.ObjectModel;

namespace AutumnBox.GUI.Services
{
    /// <summary>
    /// 如果不套一层包装器,WPF会将其直接显示,无用使用模板
    /// </summary>
    class ViewWrapper
    {
        public ViewWrapper(object view)
        {
            if (view is null)
            {
                throw new System.ArgumentNullException(nameof(view));
            }
            View = view;
        }

        public object View { get; }
    }
    interface ILeafCardManager
    {
        ObservableCollection<ViewWrapper> Views { get; }

        void Add(object view, int level);
        void Remove(object view);
    }
}
