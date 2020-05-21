#nullable enable
/*

* ==============================================================================
*
* Filename: ILake
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:17:27
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using System;
//using System.Collections;
//using System.Collections.Generic;

namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// 标准的湖接口
    /// </summary>
    public interface ILake /*: IEnumerable, IEnumerable<ComponentFactory>*/
    {
        /// <summary>
        /// 组件数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 根据ID获取组件
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="IdNotFoundException">当ID不存在时触发</exception>
        /// <returns>被注册的值</returns>
        object? GetComponent(string id);
    }
    //public interface LakeRecord
    //{
    //    string Id { get; }
    //    ComponentFactory Factory { get; }
    //}
}
