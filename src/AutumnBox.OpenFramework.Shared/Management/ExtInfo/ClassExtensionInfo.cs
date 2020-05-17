using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.ObjectModel;

#nullable enable
namespace AutumnBox.OpenFramework.Management.ExtInfo
{
    /// <summary>
    /// 拓展模块信息
    /// </summary>
    public class ClassExtensionInfo : IExtensionInfo, IEquatable<ClassExtensionInfo?>
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
        /// 读取元数据
        /// </summary>
        /// <returns></returns>
        private IReadOnlyDictionary<string, ValueReader> ReadMetadata()
        {
            Dictionary<string, ValueReader> kvs = new Dictionary<string, ValueReader>();
            foreach (var infAttr in ClassExtensionType.GetCustomAttributes<ExtensionInfoAttribute>())
            {
                kvs[infAttr.Key] = () => infAttr.Value;
            }
            return new ReadOnlyDictionary<string, ValueReader>(kvs);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as ClassExtensionInfo);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ClassExtensionInfo? other)
        {
            return other != null &&
                   EqualityComparer<Type>.Default.Equals(ClassExtensionType, other.ClassExtensionType) &&
                   Id == other.Id &&
                   EqualityComparer<IReadOnlyDictionary<string, ValueReader>>.Default.Equals(Metadata, other.Metadata);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = 1439194382;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(ClassExtensionType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, ValueReader>>.Default.GetHashCode(Metadata);
            return hashCode;
        }

        /// <summary>
        /// 与其它IExtensionInfo对比
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IExtensionInfo other)
        {
            return other != null &&
                 Id == other.Id &&
                 EqualityComparer<IReadOnlyDictionary<string, ValueReader>>.Default.Equals(Metadata, other.Metadata);
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
        public IExtensionProcedure OpenProcedure() => new ClassExtensionProcedure(ClassExtensionType);

        /// <summary>
        /// 指示在该事务中,Lake的提供键
        /// </summary>
        public const string KEY_SOURCE_IN_EXTRA_ARGS = "classExtensionSource";

        /// <summary>
        /// 比较两个ClassExtensionInfo是否代表了同一个拓展
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ClassExtensionInfo? left, ClassExtensionInfo? right)
        {
            return EqualityComparer<ClassExtensionInfo>.Default.Equals(left, right);
        }

        /// <summary>
        /// 比较两个ClassExtensionInfo是否不是代表同一个拓展
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ClassExtensionInfo? left, ClassExtensionInfo? right)
        {
            return !(left == right);
        }
    }
}
