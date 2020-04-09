/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 5:49:54 (UTC +8:00)
** desc： ...
*************************************************/

using System;
using System.Collections.Generic;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// Intent,通常作为am命令的参数
    /// </summary>
    public sealed class Intent
    {
        ///// <summary>
        ///// 序列化为arg时的设置
        ///// </summary>
        //public enum IntentToArgOption
        //{
        //    /// <summary>
        //    /// key带上双引号
        //    /// </summary>
        //    KeyDoubleQuotation = 1 << 1,
        //    /// <summary>
        //    /// 值带上双引号
        //    /// </summary>
        //    ValueDoubleQuotation = 1 << 2,
        //    /// <summary>
        //    /// 自动判断，必要时将值带上引号
        //    /// </summary>
        //    AutoValueDoubleQuotation = 1 << 4
        //}
        //private static string ToIntentArg(string key, object value
        //    , IntentToArgOption option =
        //    IntentToArgOption.KeyDoubleQuotation |
        //    IntentToArgOption.AutoValueDoubleQuotation)
        //{
        //    string flag = "esn";
        //    string _value = value.ToString();
        //    if (value is string str)
        //    {
        //        flag = "e";
        //    }
        //    else if (value is int intNumber)
        //    {
        //        flag = "ei";
        //    }
        //    else if (value is Boolean boolValue)
        //    {
        //        flag = "ez";
        //        _value = _value.ToLower();
        //    }
        //    else if (value is long longNumber)
        //    {
        //        flag = "el";
        //    }
        //    else if (value is float floatNumber)
        //    {
        //        flag = "ef";
        //    }
        //    else if (value is Uri uri)
        //    {
        //        flag = "eu";
        //    }
        //    else if (value is ComponentName cn)
        //    {
        //        flag = "ecn";
        //    }
        //    else if (value is int[] intArray)
        //    {
        //        flag = "eia";
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append(intArray[0]);
        //        for (int i = 1; 1 < intArray.Length; i++)
        //        {
        //            sb.Append($",{intArray[i]}");
        //        }
        //        _value = sb.ToString();
        //    }
        //    else if (value is long[] longArray)
        //    {
        //        flag = "ela";
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append(longArray[0]);
        //        for (int i = 1; 1 < longArray.Length; i++)
        //        {
        //            sb.Append($",{longArray[i]}");
        //        }
        //        _value = sb.ToString();
        //    }
        //    if (option.HasFlag(IntentToArgOption.KeyDoubleQuotation))
        //    {
        //        key = $"\"{key}\"";
        //    }
        //    if (option.HasFlag(IntentToArgOption.AutoValueDoubleQuotation))
        //    {
        //        if (value is string || value is ComponentName)
        //        {
        //            _value = $"\"{_value}\"";
        //        }
        //    }
        //    else if (option.HasFlag(IntentToArgOption.ValueDoubleQuotation))
        //    {
        //        if (value is string || value is ComponentName)
        //        {
        //            _value = $"\"{_value}\"";
        //        }
        //    }
        //    return $"--{flag} {key} {_value}";
        //}
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
