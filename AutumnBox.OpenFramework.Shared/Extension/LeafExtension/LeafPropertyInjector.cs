using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    internal class LeafPropertyInjector
    {
        private readonly LeafExtensionBase ext;

        public LeafPropertyInjector(LeafExtensionBase ext)
        {
            this.ext = ext ?? throw new System.ArgumentNullException(nameof(ext));
        }
        public void Inject()
        {
            var properties = from property in ext.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                             where IsInjectableProperty(property)
                             select property;
            foreach (var prop in properties)
            {
                var setter = prop.GetSetMethod() ?? prop.GetSetMethod(true);
                setter.Invoke(ext, new object[] { ApiAllocator.GetProperty(ext.Context, prop.PropertyType) });
            }
        }
        private bool IsInjectableProperty(PropertyInfo propInfo)
        {
            return propInfo.GetCustomAttribute(typeof(LPropertyAttribute)) != null;
        }
    }
}
