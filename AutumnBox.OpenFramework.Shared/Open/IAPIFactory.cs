/*

* ==============================================================================
*
* Filename: IFactory
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 14:00:13
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// API工厂
    /// </summary>
    public interface IAPIFactory
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="type"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        IAPIFactory Register(Type type, Func<object> factory);
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        IAPIFactory Register<T>(Func<object> factory);
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="impl"></param>
        /// <returns></returns>
        IAPIFactory Register<T>(Type impl);
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns></returns>
        IAPIFactory Register<T, TImpl>();

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="type"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        IAPIFactory RegisterSingleton(Type type, Func<object> factory);
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        IAPIFactory RegisterSingleton<T>(Func<object> factory);
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="impl"></param>
        /// <returns></returns>
        IAPIFactory RegisterSingleton<T>(Type impl);
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <returns></returns>
        IAPIFactory RegisterSingleton<T, TImpl>();
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IAPIFactory RegisterSingleton<T>(T value);
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Get(Type type);
        /// <summary>
        /// 泛型化的获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>();
    }
}
