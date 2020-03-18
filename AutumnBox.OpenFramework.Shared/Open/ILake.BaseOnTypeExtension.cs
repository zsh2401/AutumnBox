/*

* ==============================================================================
*
* Filename: ILake.Extension
* Description: 
*
* Version: 1.0
* Created: 2020/3/10 13:22:36
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.OpenFramework.Implementation;
using AutumnBox.OpenFramework.Open.ProxyKit;
using System;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// ILake的拓展
    /// </summary>
    public static class ILakeBaseOnTypeExtension
    {
        private readonly static IProxyBuilder defaultProxyBuilder = new ProxyBuilder();

        /// <summary>
        /// 泛型地获取一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <returns></returns>
        public static T Get<T>(this ILake lake)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            return (T)lake.Get(typeof(T));
        }
        /// <summary>
        /// 根据类型获取
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object Get(this ILake lake, Type t)
        {
            return lake.Get(GenerateNameOf(t));
        }
        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGet(this ILake lake, Type t, out object value)
        {
            try
            {
                value = lake.Get(t);
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }
        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGet<T>(this ILake lake, out T value)
        {
            try
            {
                value = lake.Get<T>();
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static ILake Register(this ILake lake, Type t, Func<object> factory)
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
        public static ILake Register<T>(this ILake lake, Func<T> factory)
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
        public static ILake Register<T>(this ILake lake, Type impl)
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
        public static ILake Register(this ILake lake, Type target, Type impl)
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
        public static ILake Register<T, TImpl>(this ILake lake) where TImpl : T
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
        public static ILake RegisterSingleton(this ILake lake, Type type, Func<object> factory)
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
        public static ILake RegisterSingleton<T>(this ILake lake, Func<object> factory)
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
        public static ILake RegisterSingleton<T>(this ILake lake, Type impl)
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
        public static ILake RegisterSingleton<T, TImpl>(this ILake lake) where TImpl : T
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
        public static ILake RegisterSingleton<T>(this ILake lake, T value)
        {
            RegisterSingletonBase(lake, typeof(T), () => value);
            return lake;
        }


        private static void RegisterBase(this ILake lake, Type target, Func<object> factory)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            lake.Register(GenerateNameOf(target), factory);
        }
        private static void RegisterSingletonBase(this ILake lake, Type target, Func<object> factory)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var lazy = new Lazy<object>(factory);
            lake.Register(GenerateNameOf(target), () => lazy.Value);
        }

        private static Func<object> GetObjectBuilderOf(ILake lake, Type t)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            return () =>
            {
                if (t.IsInterface || t.IsAbstract)
                {
                    throw new InvalidOperationException("Could not create instance of interface or abstract class");
                }
                IProxyBuilder proxyBuilder;
                if (lake.TryGet(out IProxyBuilder value))
                {
                    proxyBuilder = value;
                }
                else
                {
                    proxyBuilder = defaultProxyBuilder;
                }
                var proxy = proxyBuilder.CreateProxyOf(t);
                proxy.CreateInstance();
                proxy.Lakes[0] = lake;
                return proxy.Instance;
            };
        }
        private static string GenerateNameOf(Type t)
        {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            return $"___based_on_type_{t}";
        }
    }
}
