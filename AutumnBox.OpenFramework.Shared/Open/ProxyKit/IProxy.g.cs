using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProxy<T> : IProxy
    {
        /// <summary>
        /// 泛型化的实例
        /// </summary>
        T Instance { get; }
    }
}
