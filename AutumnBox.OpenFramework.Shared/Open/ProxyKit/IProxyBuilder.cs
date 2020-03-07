using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 代理构建器
    /// </summary>
    public interface IProxyBuilder
    {
        /// <summary>
        /// 为类型创建一个代理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IProxy CreateProxyOf(Type type);
        /// <summary>
        /// 为类型创建一个代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IProxy CreateProxyOf<T>();
    }
}
