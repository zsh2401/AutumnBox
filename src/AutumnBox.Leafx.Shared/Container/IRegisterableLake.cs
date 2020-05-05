#nullable enable
/*

* ==============================================================================
*
* Filename: IRegisterableLake
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:24:43
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;

namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// 可动态注册湖
    /// </summary>
    public interface IRegisterableLake : ILake
    {
        /// <summary>
        /// 根据ID注册组件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        void RegisterComponent(string id, ComponentFactory factory);
    }

    /// <summary>
    /// 组件工厂
    /// </summary>
    /// <returns></returns>
    public delegate object? ComponentFactory();
}
