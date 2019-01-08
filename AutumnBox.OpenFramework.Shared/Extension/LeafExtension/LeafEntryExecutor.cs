using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    internal class LeafEntryExecutor
    {
        private readonly LeafExtensionBase ext;
        private Dictionary<string, object> data;
        private readonly MethodInfo entry;
        public LeafEntryExecutor(LeafExtensionBase ext)
        {
            this.ext = ext ?? throw new ArgumentNullException(nameof(ext));
            entry = FindEntry();
        }
        private object[] GetPara(ParameterInfo[] pInfos)
        {
            List<object> ps = new List<object>();
            foreach (var pInfo in pInfos)
            {
                if (pInfo.ParameterType == typeof(Dictionary<string, object>))
                {
                    ps.Add(data);
                }
                else
                {
                    ps.Add(ApiAllocator.GetProperty(ext.Context, pInfo.ParameterType));
                }
            }
            return ps.ToArray();
        }
        private MethodInfo FindEntry()
        {
            var type = ext.GetType();
            var entries = from method in type.GetMethods()
                          where IsEntry(method)
                          select method;
            if (entries.Count() == 0)
            {
                throw new Exception("Entry not found!");
            }
            return entries.First();
        }
        private bool IsEntry(MethodInfo info)
        {
            return info.Name == "Main" || info.GetCustomAttribute(typeof(LMainAttribute)) != null;
        }
        public int? Execute(Dictionary<string, object> data = null)
        {
            this.data = data;

            //获取其需要的参数列表
            var para = GetPara(entry.GetParameters());
            //执行
            var result = entry.Invoke(ext, para);
            //处理可能的返回值
            if (result is int exitCode)
            {
                return exitCode;
            }
            else
            {
                return null;
            }
        }
    }
}
