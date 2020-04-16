/*

* ==============================================================================
*
* Filename: SunsetLake
* Description: 
*
* Version: 1.0
* Created: 2020/4/3 23:03:17
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 最简单基础的湖实现
    /// </summary>
    public class SunsetLake : IRegisterableLake
    {
        /// <summary>
        /// 内部维护一个字典用于存储所有工厂
        /// </summary>
        private readonly ConcurrentDictionary<string, Func<object>> factories = new ConcurrentDictionary<string, Func<object>>();

        /// <summary>
        /// 构造Sunset Lake
        /// </summary>
        public SunsetLake()
        {
            this.RegisterSingleton<ILake>(this);
            this.RegisterSingleton("register", () => this);
            this.RegisterSingleton("lake_name", () => nameof(SunsetLake));
        }

        /// <summary>
        /// 获取总记录量
        /// </summary>
        public int Size => factories.Count;

        /// <summary>
        /// 根据id获取值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object GetComponent(string id)
        {
            return factories[id]();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        public void RegisterComponent(string id, Func<object> factory)
        {
            factories[id] = factory;
        }

        public IEnumerable<string> GetAll_ID()
        {
            return factories.Keys;
        }
    }
}
