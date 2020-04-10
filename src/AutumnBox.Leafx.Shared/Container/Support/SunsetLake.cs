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
using System;
using System.Collections.Generic;

namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 最简单基础的湖实现
    /// </summary>
    public class SunsetLake : IRegisterableLake
    {
        private readonly Dictionary<string, Func<object>> factories = new Dictionary<string, Func<object>>();
        public SunsetLake()
        {
            this.RegisterSingleton<ILake>(this);
            this.RegisterSingleton("register", () => this);
            this.RegisterSingleton("lake_name", () => nameof(SunsetLake));
        }
        /// <summary>
        /// 根据id获取值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(string id)
        {
            lock (factories)
            {
            }
            return factories[id]();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        public void Register(string id, Func<object> factory)
        {
            lock (factories)
            {
                factories[id] = factory;
            }
        }
    }
}
