using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.OpenFramework.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace AutumnBox.OpenFramework.Leafx.Container.Support
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
            return from method in factoryType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                   where IsFactoryMethod(method)
                   select (attr: method.GetCustomAttribute<ComponentAttribute>(), method);
        }
        private bool IsFactoryMethod(MethodInfo methodInfo)
        {
            return methodInfo.GetCustomAttribute(COMPONENT_ATTR_TYPE) != null;
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
