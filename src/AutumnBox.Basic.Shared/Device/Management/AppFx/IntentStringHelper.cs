/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/29 22:48:00 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 用于将Intent转换为ADB可以识别的字符串
    /// </summary>
    public static class IntentStringHelper
    {
        private const string FLAG_INT = "ei";
        private const string FLAG_STRING = "e";
        private const string FLAG_BOOL = "ez";
        private const string FLAG_LONG = "el";
        private const string FLAG_CN = "ecn";
        private const string FLAG_URI = "eu";
        private const string FLAG_FLOAT = "ef";
        private const string FLAG_INT_ARR = "eia";
        private const string FLAG_LONG_ARR = "ela";
        private const string FMT_INTENT_KEY_VALUE = "--{0} \"{1}\" {2}";

        private static string ToIntentValue(this int intValue)
        {
            return intValue.ToString();
        }
        private static string ToIntentValue(this string str)
        {
            return $"\"{str}\"";
        }
        private static string ToIntentValue(this float floatValue)
        {
            return floatValue.ToString();
        }
        private static string ToIntentValue(this Uri uri)
        {
            return uri.ToString();
        }
        private static string ToIntentValue(this ComponentName cn)
        {
            return cn.ToString();
        }
        private static string ToIntentValue(this long value)
        {
            return value.ToString();
        }
        private static string ToIntentValue(this bool value)
        {
            return value.ToString();
        }
        private static string Wow(string key, object value)
        {
            string strValue = null;
            string flag = null;
            if (value is string str)
            {
                flag = FLAG_STRING;
                strValue = str.ToIntentValue();
            }
            else if (value is int intNumber)
            {
                flag = FLAG_STRING;
                strValue = intNumber.ToIntentValue();
            }
            else if (value is Boolean boolValue)
            {
                flag = FLAG_STRING;
                strValue = boolValue.ToIntentValue();
            }
            else if (value is long longNumber)
            {
                flag = FLAG_LONG;
                strValue = longNumber.ToIntentValue();
            }
            else if (value is float floatNumber)
            {
                flag = FLAG_FLOAT;
                strValue = floatNumber.ToIntentValue();
            }
            else if (value is Uri uri)
            {
                flag = FLAG_URI;
                strValue = uri.ToIntentValue();
            }
            else if (value is ComponentName cn)
            {
                flag = FLAG_CN;
                strValue = cn.ToIntentValue();
            }
            else if (value is int[] intArray)
            {
                flag = FLAG_INT_ARR;
                strValue = intArray.ToIntentValue();
            }
            else if (value is long[] longArray)
            {
                flag = FLAG_LONG_ARR;
                strValue = longArray.ToIntentValue();
            }
            else
            {
                throw new NotSupportedException();
            }
            return string.Format(FMT_INTENT_KEY_VALUE, flag, key, strValue);
        }
        /// <summary>
        /// 转换为Intent字符串形式
        /// </summary>
        /// <param name="keyAndValue"></param>
        /// <returns></returns>
        public static string ToIntentString(this KeyValuePair<string, object> keyAndValue)
        {
            return Wow(keyAndValue.Key, keyAndValue.Value);
        }
        /// <summary>
        /// 转换为ADB参数形式
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public static string ToAdbArguments(this Intent intent) {
            StringBuilder sb = new StringBuilder();
            foreach (var kv in intent.collection)
            {
                sb.Append(Wow(kv.Key, kv.Value));
                sb.Append(" ");
            }
            return sb.ToString();
        }
        private static string ToIntentValue(this int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 1)
            {
                return array[0].ToString();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                sb.Append("," + array[i]);
            }
            return sb.ToString();
        }
        private static string ToIntentValue(this long[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (array.Length == 1)
            {
                return array[0].ToString();
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                sb.Append("," + array[i]);
            }
            return sb.ToString();
        }


        [Obsolete]
        private static string ToIntentArg(string key, object value
            , IntentToArgOption option =
            IntentToArgOption.KeyDoubleQuotation |
            IntentToArgOption.AutoValueDoubleQuotation)
        {
            string flag = "esn";
            string _value = value.ToString();
            if (value is string str)
            {
                flag = "e";
            }
            else if (value is int intNumber)
            {
                flag = "ei";
            }
            else if (value is Boolean boolValue)
            {
                flag = "ez";
                _value = _value.ToLower();
            }
            else if (value is long longNumber)
            {
                flag = "el";
            }
            else if (value is float floatNumber)
            {
                flag = "ef";
            }
            else if (value is Uri uri)
            {
                flag = "eu";
            }
            else if (value is ComponentName cn)
            {
                flag = "ecn";
            }
            else if (value is int[] intArray)
            {
                flag = "eia";
                _value = intArray.ToIntentValue();
            }
            else if (value is long[] longArray)
            {
                flag = "ela";
                _value = longArray.ToIntentValue();
            }
            if (option.HasFlag(IntentToArgOption.KeyDoubleQuotation))
            {
                key = $"\"{key}\"";
            }
            if (option.HasFlag(IntentToArgOption.AutoValueDoubleQuotation))
            {
                if (value is string || value is ComponentName)
                {
                    _value = $"\"{_value}\"";
                }
            }
            else if (option.HasFlag(IntentToArgOption.ValueDoubleQuotation))
            {
                if (value is string || value is ComponentName)
                {
                    _value = $"\"{_value}\"";
                }
            }
            return $"--{flag} {key} {_value}";
        }

        /// <summary>
        /// 序列化为arg时的设置
        /// </summary>
        [Obsolete]
        public enum IntentToArgOption
        {
            /// <summary>
            /// key带上双引号
            /// </summary>
            KeyDoubleQuotation = 1 << 1,
            /// <summary>
            /// 值带上双引号
            /// </summary>
            ValueDoubleQuotation = 1 << 2,
            /// <summary>
            /// 自动判断，必要时将值带上引号
            /// </summary>
            AutoValueDoubleQuotation = 1 << 4
        }
    }
}
