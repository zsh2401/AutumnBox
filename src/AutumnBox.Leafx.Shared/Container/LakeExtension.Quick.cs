#nullable enable
/*

* ==============================================================================
*
* Filename: LakeExtension
* Description: 
*
* Version: 1.0
* Created: 2020/5/4 3:30:22
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Container
{
    partial class LakeExtension
    {
        /// <summary>
        /// 内部实现的空湖
        /// </summary>
        private class EmptyLake : ILake
        {
            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            public int Count => 0;

            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public object GetComponent(string id)
            {
                throw new IdNotFoundException(id);
            }
        }

        /// <summary>
        /// 表示一个空湖
        /// </summary>
        public static ILake Empty
        {
            get
            {
                emptyLake ??= new EmptyLake();
                return emptyLake;
            }
        }
        private static ILake? emptyLake;

        /// <summary>
        /// 联合多个Lake
        /// </summary>
        /// <param name="first"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static ILake UniteWith(this ILake first, params ILake[] others)
        {
            List<ILake> tmp = new List<ILake>
            {
                [0] = first
            };
            tmp.AddRange(others);
            return new MergedLake(tmp.ToArray());
        }
    }
}
