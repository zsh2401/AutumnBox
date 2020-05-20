/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 5:49:54 (UTC +8:00)
** desc： ...
*************************************************/

using System;
using System.Collections.Generic;

namespace AutumnBox.Basic.Device.ManagementV2.AppFx
{
    /// <summary>
    /// Intent,通常作为am命令的参数
    /// </summary>
    public sealed class Intent
    {
        internal readonly Dictionary<string, object> collection = new Dictionary<string, object>();
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            collection.Clear();
        }
        /// <summary>
        /// 序列化为字符
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToAdbArguments();
        }
        /// <summary>
        /// 添加空值
        /// </summary>
        /// <param name="key"></param>
        public void Add(string key)
        {
            collection.Add(key, null);
        }
        /// <summary>
        /// 添加字符值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("message", nameof(value));
            }

            collection.Add(key, value);
        }
        /// <summary>
        /// 添加bool值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, bool value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            collection.Add(key, value);
        }
        /// <summary>
        /// 添加int值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, int value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            collection.Add(key, value);
        }
        /// <summary>
        /// 添加long值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, long value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            collection.Add(key, value);
        }
        /// <summary>
        /// 添加float值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, float value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            collection.Add(key, value);
        }
        /// <summary>
        /// 添加uri值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, Uri value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            collection.Add(key, value);
        }
        /// <summary>
        /// 添加组件名
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, ComponentName value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            collection.Add(key, value);
        }
        /// <summary>
        /// 添加int数组
        /// </summary>
        /// <param name="key"></param>
        /// <param name="intArray"></param>
        public void Add(string key, int[] intArray)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            collection.Add(key, intArray);
        }
        /// <summary>
        /// 添加long数组
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longArray"></param>
        public void Add(string key, long[] longArray)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            collection.Add(key, longArray);
        }
    }
}
