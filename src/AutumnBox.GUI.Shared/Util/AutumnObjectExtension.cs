using AutumnBox.Leafx.Container;
using AutumnBox.Logging;

namespace AutumnBox.GUI.Util
{
    public static class AutumnObjectExtension
    {
        public static T GetComponent<T>(this object _)
        {
            return App.Current.Lake.Get<T>();
        }
        public static ILogger NCLogger(this object obj, string categoryName = null)
        {
            return LoggerFactory.Auto(categoryName ?? obj?.GetType()?.Name ?? "Unknown Object");
        }
    }
}
