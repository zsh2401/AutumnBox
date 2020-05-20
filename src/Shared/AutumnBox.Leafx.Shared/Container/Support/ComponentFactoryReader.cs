using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using AutumnBox.Leafx.ObjectManagement;

namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 组件工厂读取器
    /// </summary>
    public sealed class ComponentFactoryReader
    {
        private readonly IRegisterableLake registerableLake;
        private readonly Type factoryType;
        private static readonly Type COMPONENT_ATTR_TYPE = typeof(ComponentAttribute);
        private object instance;

        /// <summary>
        /// 构建一个新的组件工厂读取器实例
        /// </summary>
        /// <param name="registerableLake"></param>
        /// <param name="factoryType"></param>
        public ComponentFactoryReader(IRegisterableLake registerableLake, Type factoryType)
        {
            this.registerableLake = registerableLake ?? throw new ArgumentNullException(nameof(registerableLake));
            this.factoryType = factoryType ?? throw new ArgumentNullException(nameof(factoryType));
        }
        
        /// <summary>
        /// 加载工厂实例
        /// </summary>
        private void LoadInstance()
        {
            instance = new ObjectBuilder(factoryType, registerableLake).Build();
        }

        /// <summary>
        /// 获取所有原始方法
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 进行读取
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="method"></param>
        private void Register(ComponentAttribute attr, MethodInfo method)
        {
            var methodProxy = new MethodProxy(instance, method, registerableLake);
            object factory()
            {
                try
                {
                    return methodProxy.Invoke();
                }
                catch (ShouldAutoCreateException e)
                {
                    return new ObjectBuilder(e.Type, registerableLake).Build();
                }
            }

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
