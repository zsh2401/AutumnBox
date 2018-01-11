/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/11 22:28:08
** filename: LineFlashPackageInfo.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows.MiFlash
{
    public struct LineFlashPackageInfo
    {
        public bool IsRight { get; internal set; }
        public IEnumerable<BatInfo> Bats { get; internal set; }
    }
}
