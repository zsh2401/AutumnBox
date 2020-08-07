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
    public class SunsetLake : LakeBase, IRegisterableLake
    {
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
        /// 注册
        /// </summary>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        public void RegisterComponent(string id, ComponentFactory factory)
        {
            InnerRegister(id, factory);
        }

        /// <summary>
        /// 获取所有的ID
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Ids => Factories.Keys;
    }
}
