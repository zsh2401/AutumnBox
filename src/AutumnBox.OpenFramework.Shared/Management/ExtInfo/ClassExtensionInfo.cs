#nullable enable
using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.ObjectModel;
using AutumnBox.Leafx.ObjectManagement;

namespace AutumnBox.OpenFramework.Management.ExtInfo
{
    /// <summary>
    /// 拓展模块信息
    /// </summary>
    public class ClassExtensionInfo : IExtensionInfo
    {
        /// <summary>
        /// 相关的IClassExtension拓展模块类型
        /// </summary>
        public Type ClassExtensionType { get; private set; }

        /// <summary>
        /// 不参与缓存机制，构造一个拓展模块信息
        /// </summary>
        /// <param name="classExtensionType"></param>
        public ClassExtensionInfo(Type classExtensionType)
        {
            if (!typeof(IClassExtension).IsAssignableFrom(classExtensionType))
            {
                throw new InvalidOperationException("Could not create extension info for an type which is not implementated IClassExtension");
            }
            this.ClassExtensionType = classExtensionType ?? throw new ArgumentNullException(nameof(classExtensionType));
            this.Metadata = ReadMetadata();
        }

        /// <summary>
        /// 参与缓存机制，根据类型获取
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IExtensionInfo GetByType(Type t)
        {
            var cache = ExtensionInfoCacheManager.Cached;
            if (cache.ContainsKey(t))
            {
                return cache[t];
            }
            else
            {
                var inf = new ClassExtensionInfo(t);
                cache.Add(t, inf);
                return inf;
            }
        }

        /// <summary>
        /// 读取元数据
        /// </summary>
        /// <returns></returns>
        private IReadOnlyDictionary<string, ValueReader> ReadMetadata()
        {
            Dictionary<string, ValueReader> kvs = new Dictionary<string, ValueReader>();
            foreach (var infAttr in ClassExtensionType.GetCustomAttributes<ExtensionInfoAttribute>())
            {
                kvs.Add(infAttr.Key, () => infAttr.Value);
            }
            return new ReadOnlyDictionary<string, ValueReader>(kvs);
        }

        /// <summary>
        /// 拓展模块的唯一标识符
        /// </summary>
        public string Id
        {
            get
            {
                if (Metadata.TryGetValue("ExtId", out ValueReader valueReader))
                {
                    return (valueReader() as string) ?? ClassExtensionType.Name;
                }
                else
                {
                    return ClassExtensionType.Name;
                }
            }
        }

        /// <summary>
        /// 拓展模块的元数据
        /// </summary>
        public IReadOnlyDictionary<string, ValueReader> Metadata { get; }

        /// <summary>
        /// 获取
        /// </summary>
        public IExtensionProcedure Procedure => new ClassExtensionProcedure(ClassExtensionType);

        /// <summary>
        /// 指示在该事务中,Lake的提供键
        /// </summary>
        public const string KEY_SOURCE_IN_EXTRA_ARGS = "classExtensionSource";

        private class ClassExtensionProcedure : IExtensionProcedure
        {
            private readonly Type classExtensionType;

            public ClassExtensionProcedure(Type classExtensionType)
            {
                this.classExtensionType = classExtensionType ?? throw new ArgumentNullException(nameof(classExtensionType));
            }

            public ILake[]? Source { get; set; }

            public Dictionary<string, object>? Args { get; set; }

            public object? Run()
            {
                var source = Source ?? new ILake[0];
                var objBuilder = new ObjectBuilder(classExtensionType, source);
                var instance = (IExtension)objBuilder.Build(Args);
                return instance.Main(Args ?? new Dictionary<string, object>());
            }
        }
    }
}
