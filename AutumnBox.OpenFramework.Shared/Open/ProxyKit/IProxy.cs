using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 代理
    /// </summary>
    public interface IProxy
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
        /// 被代理对象的实例
        /// </summary>
        object Instance { get; }
        /// <summary>
        /// 创建实例
        /// </summary>
        void CreateInstance();
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        object InvokeMethod(string methodName);
    }
}
