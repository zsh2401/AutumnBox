using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    public sealed class Intent
    {
        public enum IntentToArgOption
        {
            KeyDoubleQuotation = 1 << 1,
            ValueDoubleQuotation = 1 << 2,
            AutoValueDoubleQuotation = 1 << 4
        }
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
                StringBuilder sb = new StringBuilder();
                sb.Append(intArray[0]);
                for (int i = 1; 1 < intArray.Length; i++)
                {
                    sb.Append($",{intArray[i]}");
                }
                _value = sb.ToString();
            }
            else if (value is long[] longArray)
            {
                flag = "ela";
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
        private readonly Dictionary<string, object> collection = new Dictionary<string, object>();
        public void Clear()
        {
            collection.Clear();
        }
        public string ToAdbArguments()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kv in collection)
            {
                sb.Append(ToIntentArg(kv.Key, kv.Value));
                sb.Append(" ");
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToAdbArguments();
        }
        public object this[string key]
        {
            get
            {
                return collection[key];
            }
            set
            {
                try
                {
                    collection[key] = value;
                }
                catch (KeyNotFoundException)
                {
                    //collection.Add(key, value);
                }
            }
        }
        public void Add(string key)
        {
            collection.Add(key, null);
        }
        public void Add(string key, string value)
        {
            collection.Add(key, value);
        }
        public void Add(string key, bool value)
        {
            collection.Add(key, value);
        }
        public void Add(string key, int value)
        {
            collection.Add(key, value);
        }
        public void Add(string key, long value)
        {
            collection.Add(key, value);
        }
        public void Add(string key, float value)
        {
            collection.Add(key, value);
        }
        public void Add(string key, Uri value)
        {
            collection.Add(key, value);
        }
        public void Add(string key, ComponentName value)
        {
            collection.Add(key, value);
        }
        public void Add(string key, int[] intArray)
        {
            collection.Add(key, intArray);
        }
        public void Add(string key, long[] longArray)
        {
            collection.Add(key, longArray);
        }
    }
}
