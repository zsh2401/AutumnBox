/*

* ==============================================================================
*
* Filename: CombienedLake
* Description: 
*
* Version: 1.0
* Created: 2020/4/28 17:36:49
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 合并的Lake
    /// </summary>
    public class MergedLake : ILake
    {
        /// <summary>
        /// 湖群
        /// </summary>
        /// <param name="lakes"></param>
        public MergedLake(params ILake[] lakes)
        {
            Lakes = lakes?.ToList() ?? throw new ArgumentNullException(nameof(lakes));
        }

        /// <summary>
        /// 不通过new获取合并湖
        /// </summary>
        /// <param name="lakes"></param>
        /// <returns></returns>
        public static MergedLake From(params ILake[] lakes)
        {
            return new MergedLake(lakes);
        }

        /// <summary>
        /// 将两个湖合并
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static MergedLake operator +(MergedLake left, ILake right)
        {
            return new MergedLake(left, right);
        }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                Lakes.ForEach(lake => count += lake.Count);
                return count;
            }
        }

        /// <summary>
        /// 所有被合并的湖
        /// </summary>
        public List<ILake> Lakes { get; }

        /// <summary>
        /// <获取组件,依据Lakes倒序获取
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="IdNotFoundException">进行了检索,但指定的id未找到</exception>
        /// <returns></returns>
        public object GetComponent(string id)
        {
            var reversed = Lakes.ToArray().Reverse();
            foreach (var lake in reversed)
            {
                try
                {
                    return lake.GetComponent(id);
                }
                catch (IdNotFoundException)
                {
                    continue;
                }
            }
            throw new IdNotFoundException(id);
        }

        /// <summary>
        /// 将Lakes属性复制到新的数组中
        /// </summary>
        /// <returns></returns>
        public ILake[] ToArray() {
            return Lakes.ToArray();
        }
    }
}
