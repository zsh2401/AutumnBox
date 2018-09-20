/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 11:52:25 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Model
{
    internal class ExtensionWrapperDock : ModelBase
    {
        public string ToolTip
        {
            get
            {
                return Wrapper.Info.Name + Environment.NewLine +
                     Wrapper.Info.FormatedDesc;
            }
        }
        public IExtensionWrapper Wrapper { get; private set; }
        public string Name => Wrapper.Info.Name;
        public IExtInfoGetter Info => Wrapper.Info;
        public ImageSource Icon
        {
            get
            {
                if (icon == null)
                {
                    icon = Wrapper.Info.Icon.ToExtensionIcon();
                }
                return icon;
            }
        }
        private ImageSource icon;
        public ExtensionWrapperDock(IExtensionWrapper wrapper)
        {
            this.Wrapper = wrapper;
        }
    }
    internal static class ExtensionWrapperDockExtensions
    {
        public static IEnumerable<ExtensionWrapperDock> ToDocks(this IEnumerable<IExtensionWrapper> wrappers)
        {
            List<ExtensionWrapperDock> result = new List<ExtensionWrapperDock>();
            foreach (var wrapper in wrappers)
            {
                result.Add(new ExtensionWrapperDock(wrapper));
            }
            return result;
        }
    }
}
