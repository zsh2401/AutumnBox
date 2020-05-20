#nullable enable
/*

* ==============================================================================
*
* Filename: ExtRunnableProlicyAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/4/24 15:01:50
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 模块是否可运行的策略检查器
    /// </summary>
    public abstract class ExtRunnablePolicyAttribute : ExtensionInfoAttribute
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override object? Value => this;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string Key => ExtensionMetadataKeys.RUNNABLE_POLICY;

        /// <summary>
        /// 检查是否可运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool IsRunnable(RunnableCheckArgs args);
    }
}
