using System;
using System.Collections.Generic;
using System.Linq;
using AutumnBox.Leafx.ObjectManagement;
namespace AutumnBox.Leafx.Container
{
    public static partial class LakeExtension
    {
        public static T CreateInstance<T>(this ILake lake)
        {
            return (T)new ObjectBuilder(typeof(T), lake).Build();
        }

        public static T CreateInstance<T>(this IEnumerable<ILake> sources)
        {
            return (T)new ObjectBuilder(typeof(T), sources.ToArray()).Build();
        }

        public static object CreateInstance(this ILake lake, Type t)
        {
            return new ObjectBuilder(t, lake).Build();
        }

        public static object CreateInstance(this IEnumerable<ILake> sources, Type t)
        {
            return new ObjectBuilder(t, sources.ToArray()).Build();
        }

        public static void InjectPropertyTo(this ILake lake, object instance)
        {
            DependenciesInjector.Inject(instance, lake);
        }

        public static void InjectPropertyTo(this IEnumerable<ILake> sources, object instance)
        {
            DependenciesInjector.Inject(instance, sources.ToArray());
        }
    }
}
