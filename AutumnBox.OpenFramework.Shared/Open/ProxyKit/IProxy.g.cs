using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProxy<T>
    {
        /// <summary>
        /// 设置额外的依赖工厂
        /// </summary>
        Func<Type, object> ExtraDependencyFactory { set; }
        /// <summary>
        /// 当代理正在请求某个对象时使用
        /// </summary>
        event RequestingObjectEventHandler RequestingObject;
        /// <summary>
        /// 湖列表
        /// </summary>
        List<ILake> Lakes { get; }
        /// <summary>
        /// 基于键的湖列表
        /// </summary>
        List<IKeyLake> KeyLakes { get; }
        /// <summary>
        /// 被代理对象的实例
        /// </summary>
        T Instance { get; }
        /// <summary>
        /// 创建实例
        /// </summary>
        void CreateInstance();
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        T InvokeMethod(string methodName);
    }
}
