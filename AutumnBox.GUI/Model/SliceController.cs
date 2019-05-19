using AutumnBox.GUI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Model
{
    class SliceController : ModelBase, ISliceController
    {
        public virtual string Title { get; }

        public virtual string Id { get; }

        public virtual object Icon { get; }

        public virtual object View { get; }

        public SliceController(string id, string title, object icon, object view)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Title = App.Current.Resources[title]?.ToString() ?? title ?? throw new ArgumentNullException(nameof(title));
            Icon = icon;
            View = view ?? throw new ArgumentNullException(nameof(view));
        }
    }
}
