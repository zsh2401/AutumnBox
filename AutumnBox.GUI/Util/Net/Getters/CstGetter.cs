using AutumnBox.Logging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;

namespace AutumnBox.GUI.Util.Net.Getters
{
    public class CstGetter
    {
#if DEBUG
        public string Url { get; set; } = "http://localhost:24010/_api_/home_v1/cst.xaml";
#else
        public string Url { get; set; } = App.Current.Resources["WebApiCst"].ToString();
#endif

        private static ParserContext context;
        static CstGetter()
        {
            context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            context.XmlnsDictionary.Add("materialDesign", "http://materialdesigninxaml.net/winfx/xaml/themes");
        }
        public Task<object> DoAsync()
        {
            return Task.Run(() =>
            {
                return Do();
            });
        }
        private object Do()
        {
            var xamlStr = new WebClient().DownloadData(Url);
            var stream = new MemoryStream(xamlStr);
            return App.Current.Dispatcher.Invoke(() =>
            {
                return XamlReader.Load(stream, context);
            });
        }
    }
}
