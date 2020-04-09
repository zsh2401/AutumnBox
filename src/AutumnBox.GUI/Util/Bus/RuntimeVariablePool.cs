using System;
using System.Collections.Generic;

namespace AutumnBox.GUI.Util.Bus
{
    public static class RuntimeVariablePool
    {
        private readonly static Dictionary<string, (bool, object)> records = new Dictionary<string, (bool, object)>();
        public const string APP_IS_LOADED = "__app_is_loaded";
        public static void Set(string key, object v, bool _lock)
        {
            lock (records)
            {
                if (records.TryGetValue(key, out (bool, object) record) && (!record.Item1))
                {
                    records[key] = (_lock, v);
                }
                else
                {
                    return;
                }
            }

        }
        public static object Get(string key)
        {
            try
            {
                lock (records)
                {
                    return records[key].Item2;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Not found", e);
            }
        }
    }
}
