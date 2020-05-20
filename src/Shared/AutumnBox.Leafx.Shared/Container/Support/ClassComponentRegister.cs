using System;
using System.Reflection;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;

namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 类组件注册器
    /// </summary>
    public static class ClassComponentRegister
    {
        /// <summary>
        /// 向一个湖注入一个已经被标记为组件的类
        /// </summary>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="InvalidOperationException">传入的类型不是组件</exception>
        /// <param name="lake"></param>
        /// <param name="componentType"></param>
        public static void Register(this IRegisterableLake lake, Type componentType)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (componentType is null)
            {
                throw new ArgumentNullException(nameof(componentType));
            }

            var componentAttribute = componentType.GetCustomAttribute<ComponentAttribute>();
            if (componentAttribute == null)
            {
                throw new InvalidOperationException($"{componentType.FullName} is not component");
            }
            if (componentAttribute.Id != null)
            {
                if (componentAttribute.SingletonMode)
                {
                    lake.RegisterSingleton(componentAttribute.Id, () => new ObjectBuilder(componentType, lake).Build());
                }
                else
                {
                    lake.Register(componentAttribute.Id, () => new ObjectBuilder(componentType, lake).Build());
                }
            }
            else
            {
                if (componentAttribute.SingletonMode)
                {
                    lake.RegisterSingleton(componentAttribute.Type, () => new ObjectBuilder(componentType, lake).Build());
                }
                else
                {
                    lake.Register(componentAttribute.Type, () => new ObjectBuilder(componentType, lake).Build());
                }
            }
        }
    }
}
