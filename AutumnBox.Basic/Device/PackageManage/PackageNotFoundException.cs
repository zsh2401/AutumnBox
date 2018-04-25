/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:14:35 (UTC +8:00)
** desc： ...
*************************************************/
using System;

namespace AutumnBox.Basic.Device.PackageManage
{
    /// <summary>
    /// 找不到包的异常
    /// </summary>
    [Serializable]
    public class PackageNotFoundException : Exception
    {
        /// <summary>
        /// 包名
        /// </summary>
        public string PackageName { get; private set; }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="packageName"></param>
        public PackageNotFoundException(string packageName = null)
        {
            this.PackageName = packageName;
        }
    }
}
