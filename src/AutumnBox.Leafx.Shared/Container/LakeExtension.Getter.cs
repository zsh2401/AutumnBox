#nullable enable
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
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// Lake的拓展函数
    /// </summary>
    public static partial class LakeExtension
    {
        /// <summary>
        /// 获取多个具有同id的组件
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static IEnumerable<object> GetComponents(this ILake lake, string id)
        {
            const string METHOD_NAME = "GetComponents";
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }
            var method = lake.GetType().GetMethod(METHOD_NAME, BindingFlags.Public | BindingFlags.Instance);
            if (method == null ||
                method.ReturnType != typeof(IEnumerable<object>) ||
                method.GetParameters().Length != 1 ||
                method.GetParameters()[0].ParameterType != typeof(string))
            {
                return new object[] { lake.GetComponent(id) };
                //throw new NotSupportedException($"This lake ({lake?.GetType()?.FullName}) does not support: IEnumerable<object> GetComponents(string)");
            }
            return (IEnumerable<object>)method.Invoke(lake, new object[] { id });
        }

        /// <summary>
        /// 泛型地获取一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lake"></param>
        /// <exception cref="ArgumentNullException">当Lake为NULL时抛出</exception>
        /// <exception cref="TypeNotFoundException">目标类型未被找到时发生</exception>
        /// <returns></returns>
        public static T Get<T>(this ILake lake)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            try
            {
#pragma warning disable CS8603 // 可能的 null 引用返回。
                return (T)lake.Get(typeof(T));
#pragma warning restore CS8603 // 可能的 null 引用返回。
            }
            catch (TypeNotFoundException)
            {
                throw new TypeNotFoundException(typeof(T));
            }
        }

        /// <summary>
        /// 根据类型获取
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <exception cref="ArgumentNullException">当参数为空</exception>
        /// <exception cref="TypeNotFoundException">目标类型未被找到</exception>
        /// <returns></returns>
        public static object? Get(this ILake lake, Type t)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            try
            {
                return lake.GetComponent(GenerateIdByType(t));
            }
            catch (IdNotFoundException)
            {
                throw new TypeNotFoundException(t);
            }
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="lake"></param>
        /// <param name="t"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGet(this ILake lake, Type t, out object? value)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            try
            {
                value = lake.Get(t);
                return true;
            }
            catch (TypeNotFoundException)
            {
                //SLogger.Warn(nameof(LakeExtension), $"Can not get component by type: {t.FullName}");
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
        /// <returns>是否成功获取</returns>
        public static bool TryGet(this ILake lake, string id, out object? value)
        {
            if (lake is null || string.IsNullOrEmpty(id))
            {
#pragma warning disable CS8601 // 可能的 null 引用赋值。
                value = default;
#pragma warning restore CS8601 // 可能的 null 引用赋值。
                return false;
            }

            try
            {
                value = lake.GetComponent(id);
                return true;
            }
            catch (IdNotFoundException)
            {
                //SLogger.Warn(nameof(LakeExtension), $"Can not get component by id: {id}");
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
        /// <returns>是否成功获取到</returns>
        public static bool TryGet<T>(this ILake lake, out T value)
        {
            if (lake is null)
            {
#pragma warning disable CS8601 // 可能的 null 引用赋值。
                value = default;
#pragma warning restore CS8601 // 可能的 null 引用赋值。
                return false;
            }

            try
            {
                value = lake.Get<T>();
                return true;
            }
            catch (TypeNotFoundException)
            {
                SLogger.Warn(nameof(LakeExtension), $"Can not get component by type: {typeof(T).FullName}");
                value = default!;
                return false;
            }
        }
    }
}
