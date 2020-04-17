#nullable enable
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.TextKit;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class ClassTextDictionary : IClassTextDictionary
    {
        private readonly Type classExtensionType;
        private readonly Dictionary<string, ClassTextAttribute> ResourceCollection;

        /// <summary>
        /// 构造
        /// </summary>
        public ClassTextDictionary(Type target)
        {
            this.classExtensionType = target ?? throw new ArgumentNullException(nameof(target));
            ResourceCollection = new Dictionary<string, ClassTextAttribute>();
            Load();
        }

        /// <summary>
        /// 进行加载
        /// </summary>
        private void Load()
        {
            IEnumerable<ClassTextAttribute> objAttrs =
                classExtensionType.GetCustomAttributes<ClassTextAttribute>();
            foreach (ClassTextAttribute attr in objAttrs)
            {
                ResourceCollection.Add(attr.Key, attr);
            }
        }

        /// <summary>
        /// 进行查询与获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns>根据键查询到的对应语言环境下的值,如果失败则返回null</returns>
        public string? this[string key]
        {
            get
            {
                if (ResourceCollection.TryGetValue(ClassTextAttribute.GetKeyById(key), out ClassTextAttribute attr))
                {
                    return attr.Value?.ToString() ?? null;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
