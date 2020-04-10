using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.OpenFramework.Leafx.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutumnBox.Leafx.ObjectManagement
{
    public class DependeciesInjector
    {
        private readonly object instance;
        private readonly ILake[] sources;

        public DependeciesInjector(object instance, params ILake[] sources)
        {
            this.instance = instance ?? throw new ArgumentNullException(nameof(instance));
            this.sources = sources ?? throw new ArgumentNullException(nameof(sources));
        }
        private const BindingFlags PROPERTY_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty;
        public void Inject()
        {
            var injectableProperties = from property in instance.GetType().GetProperties(PROPERTY_FLAGS)
                                       where property.GetCustomAttribute<AutoInjectAttribute>() != null
                                       select (attr: property.GetCustomAttribute<AutoInjectAttribute>(), property);
            foreach (var prop in injectableProperties)
            {
                var args = new object[] { FindByIdOrType(prop.attr, prop.property) };
                prop.property.GetSetMethod().Invoke(instance, args);
            }
        }
        private object FindByIdOrType(AutoInjectAttribute attr, PropertyInfo prop)
        {
            return attr.Id != null ? sources.Get(attr.Id) : sources.Get(prop.PropertyType);
        }
    }
}
