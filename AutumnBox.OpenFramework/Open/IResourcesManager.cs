using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 全局资源管理器
    /// </summary>
    public interface IResourcesManager
    {
        /// <summary>
        /// 添加全局资源
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(string key, object value);
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key] { get; set; }
        /// <summary>
        /// 获取资源并根据传入泛型进行转换
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TReturn Get<TReturn>(string key) where TReturn : class;
        /// <summary>
        /// 获取值类型数据，并根据传入泛型强制转换
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TReturn GetValue<TReturn>(string key) where TReturn : struct;
    }
}
