using System;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 请求对象事件的处理委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate object RequestingObjectEventHandler(object sender, RequestingObjectEventArgs e);
    /// <summary>
    /// 请求对象事件的处理委托的参数
    /// </summary>
    public class RequestingObjectEventArgs : EventArgs
    {
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="type"></param>
        public RequestingObjectEventArgs(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public Type Type { get; }
    }
}
