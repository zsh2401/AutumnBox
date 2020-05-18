/*

* ==============================================================================
*
* Filename: IStorageManager
* Description: 
*
* Version: 1.0
* Created: 2020/5/18 12:13:09
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
    /// 持久化存储API管理器
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// 获取持久化存储对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IStorage Open(string id);
    }
}
