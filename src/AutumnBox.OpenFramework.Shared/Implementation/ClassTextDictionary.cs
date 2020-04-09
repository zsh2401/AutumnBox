using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class ClassTextDictionary : IClassTextDictionary
    {
        private readonly Type classExtensionType;
        private readonly Dictionary<string, ExtTextAttribute> ResourceCollection;
        /// <summary>
        /// 构造
        /// </summary>
        public ClassTextDictionary(Type target)
        {
            this.classExtensionType = target ?? throw new ArgumentNullException(nameof(target));
            ResourceCollection = new Dictionary<string, ExtTextAttribute>();
            Load();
        }
        /// <summary>
        /// 进行加载
        /// </summary>
        private void Load()
        {
            var objAttrs = classExtensionType.GetCustomAttributes(typeof(ExtTextAttribute), true);
            SLogger<ClassTextDictionary>.Debug($"There are {objAttrs.Length} ExtTextAttribute on {classExtensionType.Name}");
            var attrs = (ExtTextAttribute[])objAttrs;
            foreach (var attr in attrs)
            {
                ResourceCollection.Add(attr.Key, attr);
            }
        }
        /// <summary>
        /// 进行查询与获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns>根据键查询到的对应语言环境下的值</returns>
        public string this[string key]
        {
            get
            {
                if (ResourceCollection.TryGetValue("ExtText_" + key, out ExtTextAttribute attr))
                {
                    return attr.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
