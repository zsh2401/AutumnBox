#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AutumnBox.Leafx.ObjectManagement;
namespace AutumnBox.Leafx.Container
{
    public static partial class LakeExtension
    {
        private class EmptyLake : ILake
        {
            public int Count => 0;

            public object GetComponent(string id)
            {
                throw new IdNotFoundException(id);
            }
        }
        public static ILake Empty
        {
            get
            {
                if (emptyLake is null)
                {
                    emptyLake = new EmptyLake();
                }
                return emptyLake;
            }
        }
        private static ILake? emptyLake;

        public static T CreateInstance<T>(this ILake source)
        {
            return (T)new ObjectBuilder(typeof(T), source).Build();
        }

        public static object CreateInstance(this ILake lake, Type t)
        {
            return new ObjectBuilder(t, lake).Build();
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
