using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 代理
    /// </summary>
    public interface IProxy
    {
        /// <summary>
        /// 湖列表
        /// </summary>
        List<ILake> Lakes { get; }
        /// <summary>
        /// 被代理对象的实例
        /// </summary>
        object Instance { get; }
        /// <summary>
        /// 创建实例
        /// </summary>
        void CreateInstance(Dictionary<string, object> extraArgs = null);
        /// <summary>
        /// 注入属性
        /// </summary>
        void InjectProperty();
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="extraArgs"></param>
        /// <returns></returns>
        object InvokeMethod(string methodName, Dictionary<string, object> extraArgs = null);
    }
}
