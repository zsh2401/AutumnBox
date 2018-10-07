/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/29 23:26:37 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 包安装的参数
    /// </summary>
    public enum PackageManagerInstallOption
    {
        /// <summary>
        ///  Install the package with forward lock.
        /// </summary>
        Lock,
        /// <summary>
        /// Reinstall an existing app, keeping its data.
        /// </summary>
        Reinstall,
        /// <summary>
        /// Allow test APKs to be installed
        /// </summary>
        Test,
        /// <summary>
        /// Install package on the shared mass storage (such as sdcard).
        /// </summary>
        Shared,
        /// <summary>
        /// Allow version code downgrade.
        /// </summary>
        AllowVersionDownGrade,
        /// <summary>
        /// Grant all permissions listed in the app manifest.
        /// </summary>
        GrantAllPermissions,
    }

}
