using AutumnBox.GUI.Util.I18N;
using AutumnBox.GUI.View.Panel;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace AutumnBox.GUI.Util.Net.HomeContent
{
    class RemoteXamlReader : IHomeContentGetter
    {
        private static readonly ParserContext context;
        static RemoteXamlReader()
        {
            context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            context.XmlnsDictionary.Add("vm", "clr-namespace:AutumnBox.GUI.ViewModel");
            context.XmlnsDictionary.Add("system", "clr-namespace:System;assembly=mscorlib");
            //context.XmlnsDictionary.Add("handycontrol", "clr-namespace:HandyControl.Controls;assembly=HandyControl");
            //context.XmlnsDictionary.Add("panel", "clr-namespace:AutumnBox.GUI.View.Panel");
            //context.XmlnsDictionary.Add("atmbctrl", "clr-namespace:AutumnBox.GUI.View.Controls");
        }
        private string GetUrl()
        {
#if DEBUG
            return "http://localhost:24010/_api_/home_v2/home.xaml";
#else
            return App.Current.Resources["WebApiHome"].ToString();
#endif
        }
        public object Default()
        {
            return new PanelDefaultInformation();
        }
        public bool TryGet(out object result)
        {
            try
            {
                if (LanguageManager.Instance.Current.LanCode.ToLower() != "zh-cn")
                {
                    result = null;
                    return false;
                }
                byte[] data = new WebClient().DownloadData(GetUrl());
                object _tmp = null;
                using (var ms = new MemoryStream(data))
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        _tmp = XamlReader.Load(ms, context);
                    });
                }
                result = _tmp;
                return true;
            }
            catch (Exception e)
            {
                result = null;
                SLogger<RemoteXamlReader>.Warn("Can not parse remote xaml", e);
                return false;
            }
        }
    }
}
