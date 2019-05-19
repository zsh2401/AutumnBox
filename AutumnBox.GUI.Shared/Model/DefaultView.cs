using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.Model
{
    class DefaultView : IAtmbViewItem
    {
        private readonly string iconUrl;
        private readonly string name;
        private readonly UIElement ui;

        public int Priority => int.MaxValue;

        public string Name => App.Current.Resources[name]?.ToString() ?? name;

        public object Icon => null;

        public bool Close()
        {
            return true;
        }

        public DefaultView(string iconUrl, string name, UIElement ui)
        {
            this.iconUrl = iconUrl;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.ui = ui ?? throw new ArgumentNullException(nameof(ui));
        }

        public UIElement GetView()
        {
            return ui;
        }
    }
}
