using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 作为补充的湖
    /// </summary>
    public sealed class ParameterLake : ILake, IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// 内部维护的字典
        /// </summary>
        private readonly ConcurrentDictionary<string, object> innerDictionary;

        /// <summary>
        /// 获取组件数量
        /// </summary>
        public int Count => innerDictionary.Count;

        /// <summary>
        /// 构造
        /// </summary>
        public ParameterLake() : this(new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dictionary"></param>
        public ParameterLake(IDictionary<string, object> dictionary)
        {
            this.innerDictionary = new ConcurrentDictionary<string, object>();
            //手动循环添加是为了使用自身的add方法
            foreach (var kv in dictionary)
            {
                this.Add(kv.Key, kv.Value);
            }
            this.Add("lake_name", nameof(SunsetLake));
        }

        private string GetRecordName(string parameterName)
        {
            return $"";
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            if (!innerDictionary.TryAdd(key, value))
            {
                throw new Exception("Can not add record");
            }
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetComponent(string id)
        {
            return innerDictionary[id];
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return innerDictionary.GetEnumerator();
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerDictionary.GetEnumerator();
        }
    }
}
