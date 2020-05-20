/*

* ==============================================================================
*
* Filename: Size
* Description: 
*
* Version: 1.0
* Created: 2020/5/16 20:28:29
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Data
{
    /// <summary>
    /// 大小
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// 构造一个Size结构体
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Size(int width, int height)
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentException("Invalid number,which less than zero");
            }
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 指示宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 指示高度
        /// </summary>
        public int Height { get; set; }
    }
}
