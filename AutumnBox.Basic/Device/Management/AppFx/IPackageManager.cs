/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 4:28:31 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 包管理器接口
    /// </summary>
    public interface IPackageManager
    {
        /// <summary>
        /// 获取包名的PKG信息
        /// </summary>
        /// <param name="pkgName"></param>
        /// <returns></returns>
        PackageInfo PkgOf(string pkgName);
        /// <summary>
        /// 获取安装的所有包
        /// </summary>
        /// <returns></returns>
        IEnumerable<PackageInfo> GetPackages();
        /// <summary>
        /// 等同于pm path命令
        /// </summary>
        /// <param name="pkgName"></param>
        /// <returns></returns>
        string Path(string pkgName);
        /// <summary>
        /// 安装PC上的APK应用到手机上
        /// </summary>
        /// <param name="file"></param>
        void Install(FileInfo file);
        /// <summary>
        /// 判断是否安装了指定包名的应用
        /// </summary>
        /// <param name="pkgName"></param>
        /// <returns></returns>
        bool IsInstall(string pkgName);
        /// <summary>
        /// 卸载指定包名的应用
        /// </summary>
        /// <param name="pkgName"></param>
        void Uninstall(string pkgName);
    }
}
