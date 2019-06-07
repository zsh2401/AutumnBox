namespace AutumnBox.GUI.Util.Net.HomeContent
{
    static class HomeContentProvider
    {
        public static event HomeContentRefreshedEventHandler Refreshed;
        public static event HomeContentRefreshingEventHandler Refreshing;
        private static readonly object _lock = new object();
        public static void Do(IHomeContentGetter getter = null)
        {
            lock (_lock)
            {
                getter = getter ?? new RemoteXamlReader();
                //开始刷新
                Refreshing?.Invoke(null, new HomeContentRefreshingEventArgs());
                //下载远端数据
                if (!getter.TryGet(out object result))
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        result = getter.Default();
                    });
                }
                //刷新完成
                Refreshed?.Invoke(null, new HomeContentRefreshedEventArgs(result));
            }
        }
    }
}
