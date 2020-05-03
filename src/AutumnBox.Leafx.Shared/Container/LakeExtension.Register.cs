#nullable enable
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutumnBox.Leafx.Container
{
    public static partial class LakeExtension
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IRegisterableLake Register(this IRegisterableLake lake, Type t, ComponentFactory factory)
        {
            RegisterBase(lake, t, () => factory());
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IRegisterableLake Register<T>(this IRegisterableLake lake, ComponentFactory factory)
        {
            RegisterBase(lake, typeof(T), () => factory());
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <param name="impl"></param>
        /// <returns></returns>
        public static IRegisterableLake Register<T>(this IRegisterableLake lake, Type impl)
        {
            RegisterBase(lake, typeof(T), GetObjectBuilderOf(lake, impl));
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="target"></param>
        /// <param name="impl"></param>
        /// <returns></returns>
        public static IRegisterableLake Register(this IRegisterableLake lake, Type target, Type impl)
        {
            RegisterBase(lake, target, GetObjectBuilderOf(lake, impl));
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns></returns>
        public static IRegisterableLake Register<T, TImpl>(this IRegisterableLake lake) where TImpl : T
        {
            RegisterBase(lake, typeof(T), GetObjectBuilderOf(lake, typeof(TImpl)));
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="type"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IRegisterableLake RegisterSingleton(this IRegisterableLake lake, Type type, ComponentFactory factory)
        {
            RegisterSingletonBase(lake, type, factory);
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IRegisterableLake RegisterSingleton<T>(this IRegisterableLake lake, ComponentFactory factory)
        {
            RegisterSingletonBase(lake, typeof(T), factory);
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <param name="impl"></param>
        /// <returns></returns>
        public static IRegisterableLake RegisterSingleton<T>(this IRegisterableLake lake, Type impl)
        {
            RegisterSingletonBase(lake, typeof(T), GetObjectBuilderOf(lake, impl));
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns></returns>
        public static IRegisterableLake RegisterSingleton<T, TImpl>(this IRegisterableLake lake) where TImpl : T
        {
            RegisterSingletonBase(lake, typeof(T), GetObjectBuilderOf(lake, typeof(TImpl)));
            return lake;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IRegisterableLake RegisterSingleton<T>(this IRegisterableLake lake, T value)
        {
            RegisterSingletonBase(lake, typeof(T), () => value);
            return lake;
        }

        /// <summary>
        /// 根据ID注册单例
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IRegisterableLake RegisterSingleton(this IRegisterableLake lake, string id, ComponentFactory factory)
        {
            lake.RegisterSingletonBase(id, factory);
            return lake;
        }

        /// <summary>
        /// 根据ID注册单例
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IRegisterableLake RegisterSingleton(this IRegisterableLake lake, string id, object value)
        {
            lake.RegisterSingletonBase(id, () => value);
            return lake;
        }

        /// <summary>
        /// 使用id注册多例
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IRegisterableLake Register(this IRegisterableLake lake, string id, ComponentFactory factory)
        {
            lake.RegisterBase(id, factory);
            return lake;
        }

        /// <summary>
        /// 根据type注册的内部实现
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="target"></param>
        /// <param name="factory"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        private static void RegisterBase(this IRegisterableLake lake, Type target, ComponentFactory factory)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            lake.RegisterBase(GenerateIdByType(target), factory);
        }

        /// <summary>
        /// 根据id注册的内部实现
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        private static void RegisterBase(this IRegisterableLake lake, string id, ComponentFactory factory)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            lake.RegisterComponent(id, factory);
        }

        /// <summary>
        /// 根据id注册单例的内部实现
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        private static void RegisterSingletonBase(this IRegisterableLake lake, string id, ComponentFactory factory)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            lake.RegisterComponent(id, GetSingletonFactory(factory));
        }

        /// <summary>
        /// 根据type注册单例的内部实现
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="target"></param>
        /// <param name="factory"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        private static void RegisterSingletonBase(this IRegisterableLake lake, Type target, ComponentFactory factory)
        {
            lake.RegisterSingletonBase(GenerateIdByType(target), factory);
        }

        /// <summary>
        /// 获取类型构建器的基础方法
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <returns></returns>
        private static ComponentFactory GetObjectBuilderOf(ILake lake, Type t)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }
            if (t.IsInterface || t.IsAbstract)
            {
                throw new InvalidOperationException("Could not create instance of interface or abstract class");
            }

            return () => lake.CreateInstance(t);
        }

        /// <summary>
        /// 根据类型生成唯一ID
        /// </summary>
        /// <param name="t"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <returns></returns>
        public static string GenerateIdByType(Type t)
        {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            return $"___lake_extension_register_by_type_id_{t.FullName}";
        }

        /// <summary>
        /// 获取一个单例工厂
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static ComponentFactory GetSingletonFactory(ComponentFactory factory)
        {
            bool created = false;
            object? cache = null;
            return () =>
            {
                if (!created)
                {
                    created = true;
                    cache = factory();
                }
                return cache;
            };
        }
    }
}
