#nullable enable
using AutumnBox.Leafx.Container;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutumnBox.Leafx.ObjectManagement
{
    /// <summary>
    /// 属性注入器
    /// </summary>
    public sealed class DependenciesInjector
    {
        /// <summary>
        /// 即将被注入的实例
        /// </summary>
        private readonly object instance;
        /// <summary>
        /// 湖
        /// </summary>
        private readonly ILake[] sources;

        /// <summary>
        /// 构造一个属性注入器
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sources"></param>
        public DependenciesInjector(object instance, params ILake[] sources)
        {
            this.instance = instance ?? throw new ArgumentNullException(nameof(instance));
            this.sources = sources ?? throw new ArgumentNullException(nameof(sources));
        }

        /// <summary>
        /// 注入
        /// </summary>
        public void Inject()
        {
            var injectables = GetInjectables(instance.GetType());
            foreach (var injectable in injectables)
            {
                try
                {
                    object? value = GetValue(injectable.Attr.Id, injectable.ValueType!);
#pragma warning disable CS8604 // 可能的 null 引用参数。
                    injectable.Set(instance, value);
#pragma warning restore CS8604 // 可能的 null 引用参数。
                }
                catch (IdNotFoundException)
                {
                    SLogger<DependenciesInjector>.Info($"Can not found component: {(injectable.Attr.Id ?? injectable.ValueType?.Name)}. Injecting of {instance.GetType().FullName}.{injectable.Name} is skipped");
                }
                catch (TypeNotFoundException)
                {
                    SLogger<DependenciesInjector>.Info($"Can not found component: {(injectable.Attr.Id ?? injectable.ValueType?.Name)}. Injecting of {instance.GetType().FullName}.{injectable.Name} is skipped");
                }
                catch (Exception e)
                {
                    SLogger<DependenciesInjector>
                        .Warn($"Can't inject proerty:{instance.GetType().FullName}.{injectable.Name}", e);
                }
            }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private object? GetValue(string? id, Type t)
        {
            if (id == null)
            {
                return sources.Get(t);
            }
            else
            {
                try
                {
                    return sources.Get(id);
                }
                catch (IdNotFoundException)
                {
                    return sources.Get(t);
                }
            }
        }

        /// <summary>
        /// 指定扫描使用的Flags
        /// </summary>
        private const BindingFlags BINDING_FLAGS =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

        /// <summary>
        /// 静态的注入方法
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sources"></param>
        public static void Inject(object instance, params ILake[] sources)
        {
            new DependenciesInjector(instance, sources).Inject();
        }

        /// <summary>
        /// 获取所有可注入属性
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private IEnumerable<InjectableInfo> GetInjectables(Type t)
        {
            var injectableProperties = from property in t.GetProperties(BINDING_FLAGS)
                                       where property.GetCustomAttribute<AutoInjectAttribute>() != null
                                       where property.GetSetMethod(true) != null
                                       select new InjectableInfo(property);

            var injectableFields = from field in t.GetFields(BINDING_FLAGS)
                                   where field.GetCustomAttribute<AutoInjectAttribute>() != null
                                   select new InjectableInfo(field);

            var result = injectableProperties.Concat(injectableFields);
            if (t.BaseType == typeof(object))
            {
                return result;
            }
            else
            {
                return result.Concat(GetInjectables(t.BaseType));
            }
        }

        /// <summary>
        /// 可被注入目标的信息
        /// </summary>
        private class InjectableInfo
        {
            /// <summary>
            /// 期望被注入的类型
            /// </summary>
            public Type ValueType { get; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// 特性
            /// </summary>
            public AutoInjectAttribute Attr { get; }

            /// <summary>
            /// 设置器
            /// </summary>
            private readonly Action<object, object?> setter;

            /// <summary>
            /// 为属性构建可注入目标信息
            /// </summary>
            /// <param name="property"></param>
            public InjectableInfo(PropertyInfo property)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                ValueType = property.PropertyType;
                setter = (instance, v) => property.GetSetMethod(true).Invoke(instance, new object?[] { v });
                Name = property.Name;
                Attr = property.GetCustomAttribute<AutoInjectAttribute>();
            }

            /// <summary>
            /// 为字段构建可注入目标信息
            /// </summary>
            /// <param name="field"></param>
            public InjectableInfo(FieldInfo field)
            {
                if (field is null)
                {
                    throw new ArgumentNullException(nameof(field));
                }
                ValueType = field.FieldType;
                setter = (instance, v) => field.SetValue(instance, v);
                Name = field.Name;
                Attr = field.GetCustomAttribute<AutoInjectAttribute>();
            }

            /// <summary>
            /// 进行设置(注入操作)
            /// </summary>
            /// <param name="instance"></param>
            /// <param name="value"></param>
            public void Set(object instance, object? value)
            {
                setter(instance, value);
            }
        }
    }
}
