/*

* ==============================================================================
*
* Filename: LakeBase
* Description: 
*
* Version: 1.0
* Created: 2020/5/5 15:09:39
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable enable
namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 一个通用的湖基础实现
    /// </summary>
    public abstract class LakeBase : ILake
    {
        /// <summary>
        /// 内部维护的工厂记录表
        /// </summary>
        protected virtual IDictionary<string, ComponentFactory> Factories { get; } = new ConcurrentDictionary<string, ComponentFactory>();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual int Count => Factories.Count;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual object? GetComponent(string id)
        {
            if (Factories.TryGetValue(id, out ComponentFactory? factory))
            {
                return factory.Invoke();
            }
            else
            {
                throw new IdNotFoundException(id);
            }
        }

        /// <summary>
        /// 注册工厂
        /// </summary>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        protected virtual void InnerRegister(string id, ComponentFactory factory)
        {
            this.Factories[id] = factory;
        }

        /// <summary>
        /// 将两个湖合并
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static LakeBase operator +(LakeBase left, ILake right)
        {
            return new MergedLake(left, right);
        }

        /// <summary>
        /// 将两个湖合并
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static LakeBase operator +(ILake left, LakeBase right)
        {
            return new MergedLake(left, right);
        }

        /// <summary>
        /// 将两个湖合并
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static LakeBase operator +(LakeBase left, LakeBase right)
        {
            return new MergedLake(left, right);
        }
    }
}
