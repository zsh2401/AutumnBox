/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/11 22:28:08
** filename: LineFlashPackageInfo.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System.Collections.Generic;

namespace AutumnBox.Basic.Flows.MiFlash
{
    /// <summary>
    /// 线刷包信息
    /// </summary>
    public struct LineFlashPackageInfo
    {
        /// <summary>
        /// 路径是正确的
        /// </summary>
        public bool PathIsRight { get; internal set; }
        /// <summary>
        /// 是正确的
        /// </summary>
        public bool IsRight { get; internal set; }
        /// <summary>
        /// 线刷包下的Bat
        /// </summary>
        public IEnumerable<BatInfo> Bats { get; internal set; }
    }
}
