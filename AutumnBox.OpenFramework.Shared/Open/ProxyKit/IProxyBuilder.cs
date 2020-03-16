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
        IProxy<T> CreateProxyOf<T>();
        /// <summary>
        /// 为已存在的实例创建代理
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        IProxy CreateProxyOf(object instance);
    }
}
