/*

* ==============================================================================
*
* Filename: FallExtension
* Description: 
*
* Version: 1.0
* Created: 2020/3/8 20:13:54
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Leafx;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 秋拓展
    /// </summary>
    public abstract class FallExtension : EmptyExtension, IClassExtension
    {
        /// <summary>
        /// 检测冗余调用
        /// </summary>
        private bool disposedValue = false;
        /// <summary>
        /// 公开的Main方法
        /// </summary>
        /// <param name="argsDictionary"></param>
        /// <returns></returns>
        public object Main(Dictionary<string, object> argsDictionary)
        {
            object result = null;
            Main(argsDictionary, LakeProvider.Lake, ref result);
            return result;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }


        /// <summary>
        /// 内部的main方法实现
        /// </summary>
        /// <param name="args">由拓展模块调用者传入的参数</param>
        /// <param name="lake">API湖</param>
        /// <param name="result">可以不进行设置</param>
        /// <returns></returns>
        protected abstract void Main(Dictionary<string, object> args, ILake lake, ref object result);
        /// <summary>
        /// 当拓展模块被释放时调用
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
    }
}