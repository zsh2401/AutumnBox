using AutumnBox.Leafx.Container;

namespace AutumnBox.GUI.Services
{
    /// <summary>
    /// 与服务相关的一些帮助
    /// </summary>
    public static class Component
    {
        /// <summary>
        /// 获取服务,不推荐使用此方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return App.Current.Lake.Get<T>();
        }
        public static T GetComponent<T>(this object _)
        {
            return Get<T>();
        }
    }
}
