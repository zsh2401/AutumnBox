/*

* ==============================================================================
*
* Filename: IStringLake
* Description: 
*
* Version: 1.0
* Created: 2020/3/9 0:31:14
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 以键为基础的池塘
    /// </summary>
    public interface IKeyValuePool : IDictionary<string, object>
    {
    }
}
