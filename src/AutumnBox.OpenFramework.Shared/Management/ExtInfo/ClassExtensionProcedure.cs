#nullable enable
/*

* ==============================================================================
*
* Filename: ClassExtensionProcedure
* Description: 
*
* Version: 1.0
* Created: 2020/4/28 14:59:19
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Management.ExtInfo
{
    /// <summary>
    /// 类模块进程
    /// </summary>
    public class ClassExtensionProcedure : IExtensionProcedure
    {
        private readonly Type classExtensionType;

        /// <summary>
        /// 构建类模块进程
        /// </summary>
        /// <param name="classExtensionType"></param>
        public ClassExtensionProcedure(Type classExtensionType)
        {
            this.classExtensionType = classExtensionType ?? throw new ArgumentNullException(nameof(classExtensionType));
        }

        /// <summary>
        /// 源
        /// </summary>
        public ILake? Source { get; set; }

        /// <summary>
        /// Main函数的额外参数
        /// </summary>
        public Dictionary<string, object>? Args { get; set; }

        /// <summary>
        /// 运行
        /// </summary>
        /// <returns></returns>
        public object? Run()
        {
            var source = Source ?? LakeExtension.Empty;
            var objBuilder = new ObjectBuilder(classExtensionType, source);
            var instance = (IExtension)objBuilder.Build(Args);
            return instance.Main(Args ?? new Dictionary<string, object>());
        }
    }
}
