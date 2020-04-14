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
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;

namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// Lake的拓展函数
    /// </summary>
    public static class LakeExtension
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
            return lake.GetComponent(RLakeExtension.GenerateIdByType(t));
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
                SLogger.Warn(nameof(LakeExtension), $"Can not get component by type: {t.FullName}", e);
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
                value = lake.GetComponent(id);
                return true;
            }
            catch (Exception e)
            {
                SLogger.Warn(nameof(LakeExtension), $"Can not get component by id: {id}", e);
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
                SLogger.Warn(nameof(LakeExtension), $"Can not get component by type: {typeof(T).FullName}", e);
                value = default;
                return false;
            }
        }
    }
}
