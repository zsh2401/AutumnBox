/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 3:04:21 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 标准的基于编译类的拓展
    /// </summary>
    public interface IClassExtension
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="methodCaller"></param>
        /// <param name="args"></param>
        void Init(Context methodCaller, ExtensionArgs args);
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="caller"></param>
        /// <returns></returns>
        int Run(Context methodCaller);
        /// <summary>
        /// 尝试停止
        /// </summary>
        /// <exception cref="Exception">当无法停止时,将可能抛出异常</exception>
        /// <param name="methodCaller"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool TryStop(Context methodCaller, ExtensionStopArgs args);
        /// <summary>
        /// 无论是否成功,都会呼唤此方法
        /// </summary>
        /// <param name="methodCaller"></param>
        /// <param name="args"></param>
        void Finish(Context methodCaller, ExtensionFinishedArgs args);
        /// <summary>
        /// 摧毁
        /// </summary>
        /// <param name="methodCaller"></param>
        /// <param name="args"></param>
        void Destory(Context methodCaller, ExtensionDestoryArgs args);
    }
}
