/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 3:16:25 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.PackageManage
{
    /// <summary>
    /// 包信息
    /// </summary>
    public sealed class PackageInfo
    {
        #region Property
        /// <summary>
        /// 这个包是否存在
        /// </summary>
        public bool IsExist
        {
            get
            {
                var result = PackageManagerShared.Executer.QuicklyShell(Owner, $"pm path {Name}");
                return result.IsSuccessful;
            }
        }
        /// <summary>
        /// 主界面类名
        /// </summary>
        public string MainActivity
        {
            get
            {
                if (!IsExist) { throw new PackageNotFoundException(Name); }
                var exeResult = PackageManagerShared.Executer.QuicklyShell(Owner, $"dumpsys package {Name}");
                var match = Regex.Match(exeResult.ToString(), mainActivityPattern);
                if (match.Success)
                {
                    return match.Result("${result}");
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 包名
        /// </summary>
        public string Name { get; private set; }
        #endregion
        /// <summary>
        /// 这个包所在的设备
        /// </summary>
        public DeviceSerialNumber Owner { get; private set; }

        private static readonly string mainActivityPattern = $"android.intent.action.MAIN:{Environment.NewLine}.+.+\u0020(?<result>.+)";
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        public PackageInfo(DeviceSerialNumber owner, string name)
        {
            this.Name = name;
            this.Owner = owner;
        }
        
    }
}