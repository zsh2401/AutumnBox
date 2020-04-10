/*

* ==============================================================================
*
* Filename: LakeHelper
* Description: 
*
* Version: 1.0
* Created: 2020/4/5 23:06:21
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;

namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// Lake的拓展函数
    /// </summary>
    public static class Lake
    {
        /// <summary>
        /// 安全获取函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T SafeGet<T>(this ILake lake, T defaultValue = default(T))
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }
            try
            {
                return (T)lake.Get(typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

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
            return lake.Get(GenerateIdByType(t));
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
            catch (Exception e)
            {
                SLogger.Warn(nameof(Lake), "Can not get component", e);
                value = default;
                return false;
            }
        }
        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGet(this ILake lake, string id, out object value)
        {
            try
            {
                value = lake.Get(id);
                return true;
            }
            catch (Exception e)
            {
                SLogger.Warn(nameof(Lake), "Can not get component", e);
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
            catch (Exception e)
            {
                SLogger.Warn(nameof(Lake), "Can not get component", e);
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
        public static IRegisterableLake Register(this IRegisterableLake lake, Type t, Func<object> factory)
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
        public static IRegisterableLake Register<T>(this IRegisterableLake lake, Func<T> factory)
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
        public static IRegisterableLake RegisterSingleton(this IRegisterableLake lake, Type type, Func<object> factory)
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
        public static IRegisterableLake RegisterSingleton<T>(this IRegisterableLake lake, Func<object> factory)
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
        public static IRegisterableLake RegisterSingleton(this IRegisterableLake lake, string id, Func<object> factory)
        {
            lake.RegisterSingletonBase(id, factory);
            return lake;
        }

        /// <summary>
        /// 使用id注册多例
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IRegisterableLake Register(this IRegisterableLake lake, string id, Func<object> factory)
        {
            lake.RegisterBase(id, factory);
            return lake;
        }

        /// <summary>
        /// 注册函数的最内部实现
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="target"></param>
        /// <param name="factory"></param>
        private static void RegisterBase(this IRegisterableLake lake, Type target, Func<object> factory)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            lake.RegisterBase(GenerateIdByType(target), factory);
        }

        private static void RegisterBase(this IRegisterableLake lake, string id, Func<object> factory)
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
            lake.Register(id, factory);
        }

        private static void RegisterSingletonBase(this IRegisterableLake lake, string id, Func<object> factory)
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

            var lazy = new Lazy<object>(factory);
            lake.Register(id, () => lazy.Value);
        }

        private static void RegisterSingletonBase(this IRegisterableLake lake, Type target, Func<object> factory)
        {
            lake.RegisterSingletonBase(GenerateIdByType(target), factory);
        }

        /// <summary>
        /// 单例注册函数的最内部实现
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <returns></returns>
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
            if (t.IsInterface || t.IsAbstract)
            {
                throw new InvalidOperationException("Could not create instance of interface or abstract class");
            }

            return () =>
            {
                ObjectBuilder objBuilder = new ObjectBuilder(t, lake);
                var obj = objBuilder.Build();
                return obj;
            };
        }

        /// <summary>
        /// 根据类型生成唯一ID
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GenerateIdByType(Type t)
        {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            return $"___based_on_type_{t}";
        }


        public static void LoadFrom(this IRegisterableLake rLake, Type factoryType)
        {
            if (factoryType.IsAbstract || factoryType.IsInterface)
            {
                throw new InvalidOperationException("Is not correct factory type");
            }
            var instance = Activator.CreateInstance(factoryType);

        }
    }
}
