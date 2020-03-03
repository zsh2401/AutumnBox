/*

* ==============================================================================
*
* Filename: LakeProvider
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 16:16:43
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
    /// 接口湖提供器
    /// </summary>
    public static class LakeProvider
    {
        /// <summary>
        /// 接口湖
        /// </summary>
        public static ILake Lake { get; }
        static LakeProvider()
        {
            if (Lake == null)
            {
                Lake = null;
            }
        }
    }
}
