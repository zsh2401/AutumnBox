/*

* ==============================================================================
*
* Filename: InjectAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/3/9 0:36:40
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using System;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 使用在属性上时,将注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
        /// <summary>
        /// 根据属性类型
        /// </summary>
        public InjectAttribute() { }
        /// <summary>
        /// 配置ID
        /// </summary>
        /// <param name="id"></param>
        public InjectAttribute(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            Id = id;
        }
        /// <summary>
        /// 显示声明需要注入的类型
        /// </summary>
        /// <param name="type"></param>
        public InjectAttribute(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// id
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
    }
}
