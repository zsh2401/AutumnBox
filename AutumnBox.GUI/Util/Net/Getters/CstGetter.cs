using System.Net;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace AutumnBox.GUI.Util.Net.Getters
{
    public class CstGetter
    {
#if DEBUG
        public string Url { get; set; } = "http://localhost:24010/_api_/cst/v1.xaml";
#else
        public string Url { get; set; } = App.Current.Resources["WebApiTips"].ToString();
#endif
        public Task<object> DoAsync(ParserContext context)
        {
            return Task.Run(() =>
            {
                return Do(context);
            });
        }
        private object Do(ParserContext context)
        {
            var xamlStr = new WebClient().DownloadString(Url);
            return App.Current.Dispatcher.Invoke(() =>
            {
                return XamlReader.Parse(xamlStr, context);
            });
        }
    }
}
