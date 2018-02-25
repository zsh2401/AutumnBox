/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/27 10:44:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    [Obsolete("Project ACP is dead")]
    /// <summary>
    /// 包基本信息
    /// </summary>
    public struct PackageBasicInfo
    {
        public string Name { get; set; }
        public string PackageName { get; set; }
        public bool IsSystemApp { get; set; }
    }
}
