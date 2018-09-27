using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 组件名
    /// </summary>
    public class ComponentName
    {
        /// <summary>
        /// 包名
        /// </summary>
        public string PackageName { get; private set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; private set; }
        /// <summary>
        /// 通过包名和类名构造此类
        /// </summary>
        /// <param name="pkgName"></param>
        /// <param name="className"></param>
        public ComponentName(string pkgName,string className) {
            this.PackageName = pkgName;
            this.ClassName = className;
        }
        /// <summary>
        /// 字符串化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{PackageName}/.{ClassName}";
        }
    }
}
