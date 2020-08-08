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
using System.Reflection;
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
            try
            {
                var source = Source ?? LakeExtension.Empty;
                var objBuilder = new ObjectBuilder(classExtensionType, source);
                var instance = (IExtension)objBuilder.Build(Args);
                return instance.Main(Args ?? new Dictionary<string, object>());
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException ?? e;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 释放函数
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        /// <summary>
        /// 拓展模块事务
        /// </summary>
        ~ClassExtensionProcedure()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
