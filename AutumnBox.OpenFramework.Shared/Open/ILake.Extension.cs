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
    public static class ILakeExtension
    {
        private readonly static IProxyBuilder proxyBuilder = new ProxyBuilder();
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
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static ILake Register<T>(this ILake lake, Func<T> factory)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return lake.Register(typeof(T), factory);
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
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (impl is null)
            {
                throw new ArgumentNullException(nameof(impl));
            }

            return lake.Register(typeof(T), () =>
            {
                var proxy = proxyBuilder.CreateProxyOf(impl);
                proxy.CreateInstance();
                return proxy.Instance;
            });
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns></returns>
        public static ILake Register<T, TImpl>(this ILake lake) where TImpl : T
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            return lake.Register(typeof(T), () =>
            {
                var proxy = proxyBuilder.CreateProxyOf(typeof(TImpl));
                proxy.CreateInstance();
                return proxy.Instance;
            });
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
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var lazy = new Lazy<object>(factory);
            return lake.Register(type, () => lazy.Value);
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
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return lake.RegisterSingleton(typeof(T), factory);
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
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (impl is null)
            {
                throw new ArgumentNullException(nameof(impl));
            }

            var proxy = proxyBuilder.CreateProxyOf(impl);
            return lake.RegisterSingleton<T>(() =>
           {
               proxy.CreateInstance();
               return proxy.Instance;
           });
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns></returns>
        public static ILake RegisterSingleton<T, TImpl>(this ILake lake) where TImpl : T
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            return lake.RegisterSingleton<T>(typeof(TImpl));
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
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            return lake.Register<T>(() => value);
        }
    }
}
