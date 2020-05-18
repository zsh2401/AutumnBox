/*

* ==============================================================================
*
* Filename: ExtRequireRootAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/5/18 13:08:29
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 标记当前模块是否需要ROOT才能够运行
    /// </summary>
    public class ExtRequireRootAttribute : ExtensionInfoAttribute
    {
        /// <inheritdoc/>
        public override string Key => ExtensionMetadataKeys.ROOT;

        /// <inheritdoc/>
        public ExtRequireRootAttribute(bool needRoot=true) : base(needRoot) { }
    }
}
