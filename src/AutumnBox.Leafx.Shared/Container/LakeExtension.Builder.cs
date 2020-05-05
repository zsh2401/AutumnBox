#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AutumnBox.Leafx.ObjectManagement;
namespace AutumnBox.Leafx.Container
{
    public static partial class LakeExtension
    {
        /// <summary>
        /// 使用湖中的依赖创建实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(this ILake source)
        {
            return (T)new ObjectBuilder(typeof(T), source).Build();
        }

        /// <summary>
        /// 使用湖中的依赖创建实例
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object CreateInstance(this ILake lake, Type t)
        {
            return new ObjectBuilder(t, lake).Build();
        }

        /// <summary>
        /// 将依赖注入到对象
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="instance"></param>
        public static void InjectPropertyTo(this ILake lake, object instance)
        {
            DependenciesInjector.Inject(instance, lake);
        }
    }
}
