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
    public sealed class DependeciesInjector
    {
        private readonly object instance;
        private readonly ILake[] sources;

        /// <summary>
        /// 构造一个属性注入器
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sources"></param>
        public DependeciesInjector(object instance, params ILake[] sources)
        {
            this.instance = instance ?? throw new System.ArgumentNullException(nameof(instance));
            this.sources = sources ?? throw new System.ArgumentNullException(nameof(sources));
        }

        /// <summary>
        /// 注入
        /// </summary>
        public void Inject()
        {
            foreach (var injectable in GetInjectables(instance.GetType()))
            {
                try
                {
                    var value = GetValue(injectable.Attr.Id, injectable.ValueType);
                    if (value != null)
                    {
                        injectable.Set(instance, value);
                    }
                    else
                    {
                        SLogger<DependeciesInjector>.Info($"Can not found component: {(injectable.Attr.Id ?? injectable.ValueType?.Name)}. Injecting of {instance.GetType().FullName}.{injectable.Name} is skipped");
                    }
                }
                catch (Exception e)
                {
                    SLogger<DependeciesInjector>
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
        private object GetValue(string id, Type t)
        {
            if (id == null)
            {
                return sources.Get(t);
            }
            else
            {
                var byIdResult = sources.Get(id);
                return byIdResult != null ? byIdResult : sources.Get(t);
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
            new DependeciesInjector(instance, sources).Inject();
        }
        private class InjectableInfo
        {
            public Type ValueType { get; }
            public string Name { get; }
            public AutoInjectAttribute Attr { get; }
            private readonly Action<object, object> setter;
            public InjectableInfo(PropertyInfo property)
            {
                if (property is null)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                ValueType = property.PropertyType;
                setter = (instance, v) => property.GetSetMethod(true).Invoke(instance, new object[] { v });
                Name = property.Name;
                Attr = property.GetCustomAttribute<AutoInjectAttribute>();
            }
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
            public void Set(object instance, object value)
            {
                setter(instance, value);
            }
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
    }
}
