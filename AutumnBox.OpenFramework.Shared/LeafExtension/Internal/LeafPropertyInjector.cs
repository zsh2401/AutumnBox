using AutumnBox.OpenFramework.LeafExtension.Attributes;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.LeafExtension.Internal
{
    internal class LeafPropertyInjector
    {
        private readonly LeafExtensionBase ext;
        private readonly ApiAllocator apiAllocator;

        public LeafPropertyInjector(LeafExtensionBase ext,ApiAllocator apiAllocator)
        {
            this.ext = ext ?? throw new System.ArgumentNullException(nameof(ext));
            this.apiAllocator = apiAllocator ?? throw new System.ArgumentNullException(nameof(apiAllocator));
        }
        public void Inject()
        {
            var properties = from property in ext.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                             where IsInjectableProperty(property)
                             select property;
            foreach (var prop in properties)
            {
                var setter = prop.GetSetMethod() ?? prop.GetSetMethod(true);
                setter.Invoke(ext, new object[] { apiAllocator.GetByType(prop.PropertyType) });
            }
        }
        private bool IsInjectableProperty(PropertyInfo propInfo)
        {
            return propInfo.GetCustomAttribute(typeof(LPropertyAttribute)) != null;
        }
    }
}
