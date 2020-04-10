using AutumnBox.Leafx.Attributes;
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
    public sealed class PropertyInjector
    {
        private readonly object instance;
        private readonly ILake[] sources;

        /// <summary>
        /// 构造一个属性注入器
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="sources"></param>
        public PropertyInjector(object instance, params ILake[] sources)
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
                        SLogger<PropertyInjector>.Info($"Injecting of {instance.GetType().FullName}.{property.Name} is skipped");
                    }
                }
                catch (Exception e)
                {
                    SLogger<PropertyInjector>
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

        private const BindingFlags BINDING_FLAGS =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy
            ;
        private IEnumerable<PropertyInfo> GetInjectableProperties(Type t)
        {
            var properties = from property in t.GetProperties(BINDING_FLAGS)
                             where property.GetCustomAttribute<AutoInjectAttribute>() != null
                             where property.GetSetMethod(true) != null
                             select property;
            SLogger<PropertyInjector>.Info($"Find {properties.Count()} Injectable properties in {instance.GetType().Name}");
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
