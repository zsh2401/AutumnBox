using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Leafx.Container;

namespace AutumnBox.Leafx.Container.Support
{
    internal sealed class ComponentFactoryReader
    {
        private readonly IRegisterableLake registerableLake;
        private readonly Type factoryType;
        private static readonly Type COMPONENT_ATTR_TYPE = typeof(ComponentAttribute);
        private object instance;
        public ComponentFactoryReader(IRegisterableLake registerableLake, Type factoryType)
        {
            this.registerableLake = registerableLake ?? throw new ArgumentNullException(nameof(registerableLake));
            this.factoryType = factoryType ?? throw new ArgumentNullException(nameof(factoryType));
        }
        private void LoadInstance()
        {
            instance = new ObjectBuilder(factoryType, registerableLake).Build();
        }
        private IEnumerable<(ComponentAttribute, MethodInfo)> GetRawMethods()
        {
            var methods = from method in factoryType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                          where method.GetCustomAttribute<ComponentAttribute>() != null
                          select (attr: method.GetCustomAttribute<ComponentAttribute>(), method);

            var propertiesGetters = from property in factoryType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                    where property.GetCustomAttribute<ComponentAttribute>() != null
                                    select (property.GetCustomAttribute<ComponentAttribute>(), property.GetGetMethod());
            return methods.Concat(propertiesGetters);
        }
        public void Read()
        {
            if (instance == null)
            {
                LoadInstance();
            }
            foreach (var raw in GetRawMethods())
            {
                Register(raw.Item1, raw.Item2);
            }
        }
        private void Register(ComponentAttribute attr, MethodInfo method)
        {
            var methodProxy = new MethodProxy(instance, method, registerableLake);
            object factory() => methodProxy.Invoke();

            if (attr.SingletonMode)
            {
                if (attr.Id == null)
                {
                    registerableLake.RegisterSingleton(method.ReturnType, factory);
                }
                else
                {
                    registerableLake.RegisterSingleton(attr.Id, factory);
                }
            }
            else
            {
                if (attr.Id == null)
                {
                    registerableLake.Register(method.ReturnType, factory);
                }
                else
                {
                    registerableLake.Register(attr.Id, factory);
                }
            }
        }
    }
}
