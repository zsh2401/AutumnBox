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
            foreach (var property in GetInjectableProperties(instance.GetType()))
            {
                try
                {
                    var attr = property.GetCustomAttribute<AutoInjectAttribute>();
                    var value = GetValue(attr.Id, property.PropertyType);
                    if (value != null)
                    {
                        var setter = property.GetSetMethod(true);
                        setter.Invoke(instance, new object[] { value });
                    }
                    else
                    {
                        SLogger<DependeciesInjector>.Info($"Injecting of {instance.GetType().FullName}.{property.Name} is skipped");
                    }
                }
                catch (Exception e)
                {
                    SLogger<DependeciesInjector>
                        .Warn($"Can't inject proerty:{instance.GetType().FullName}.{property.Name}", e);
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

        /// <summary>
        /// 获取所有可注入属性
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private IEnumerable<PropertyInfo> GetInjectableProperties(Type t)
        {
            var properties = from property in t.GetProperties(BINDING_FLAGS)
                             where property.GetCustomAttribute<AutoInjectAttribute>() != null
                             where property.GetSetMethod(true) != null
                             select property;
            if (t.BaseType == typeof(object))
            {
                return properties;
            }
            else
            {
                return properties.Concat(GetInjectableProperties(t.BaseType));
            }
        }
    }
}
