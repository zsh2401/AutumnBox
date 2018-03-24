using AutumnBox.Basic.Device;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI
{
    internal static class PanelExtension
    {
        public static List<IRefreshable> GetAllRefreshableChild(this Panel panel)
        {
            var result = new List<IRefreshable>();
            Find(panel, result);
            return result;
        }
        private static void Find(Panel panel, List<IRefreshable> list)
        {
            foreach (UIElement ele in panel.Children)
            {
                if (ele is IRefreshable _refreshable) list.Add(_refreshable);
                if (ele is Panel _panel) Find(_panel, list);
            }
        }
    }
    internal interface IRefreshable
    {
        void Reset();
        void Refresh(DeviceBasicInfo deviceSimpleInfo);
    }
}
