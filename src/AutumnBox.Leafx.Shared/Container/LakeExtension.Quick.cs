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
using System.Linq;
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
            /// <inheritdoc/>
            public int Count => 0;

            /// <inheritdoc/>
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
        /// 联合多个lake
        /// </summary>
        /// <param name="lakes">需要被联合的湖</param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <returns>被联合的湖</returns>
        public static ILake Unite(params ILake[] lakes)
        {
            if (lakes is null)
            {
                throw new ArgumentNullException(nameof(lakes));
            }
            return new MergedLake(lakes.ToArray());
        }

        /// <summary>
        /// 联合多个Lake
        /// </summary>
        /// <param name="first">第一个湖</param>
        /// <param name="others">其它湖</param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <returns>被联合的湖</returns>
        public static ILake UniteWith(this ILake first, params ILake[] others)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (others is null)
            {
                throw new ArgumentNullException(nameof(others));
            }

            List<ILake> tmp = new List<ILake>
            {
                [0] = first
            };
            tmp.AddRange(others);
            return new MergedLake(tmp.ToArray());
        }
    }
}
