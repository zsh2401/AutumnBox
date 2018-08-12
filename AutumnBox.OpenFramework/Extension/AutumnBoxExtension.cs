/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:40:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;

namespace AutumnBox.OpenFramework.Extension
{

    /// <summary>
    /// 标准的秋之盒拓展基类
    /// </summary>
    [ExtName("标准秋之盒拓展")]
    [ExtName("Standard AutumnBox extension", Lang = "en-us")]
    [ExtAuth("佚名")]
    [ExtAuth("Anyone", Lang = "en-us")]
    [ExtDesc("这是一个测试模块")]
    [ExtDesc("This is a test extesion", Lang = "en-us")]
    [ExtVersion(1, 0, 0)]
    [ExtRequiredDeviceStates(DeviceState.None)]
    [ExtMinApi(8)]
    [ExtTargetApi]
    [ExtRunAsAdmin(false)]
    [ExtHighPermission(false)]
    [ExtRequireRoot(false)]
    //[ExtAppProperty("com.fuck.a","Fuck")]
    public abstract class AutumnBoxExtension : Context
    {
        /// <summary>
        /// 完全成功
        /// </summary>
        public const int OK = 0;
        /// <summary>
        /// 发生错误
        /// </summary>
        public const int ERR = 1;
        /// <summary>
        /// 日志标签
        /// </summary>
        public sealed override string LoggingTag => ExtName;
        /// <summary>
        /// 拓展名
        /// </summary>
        public string ExtName { get; set; }
        /// <summary>
        /// 目标设备
        /// </summary>
        public DeviceBasicInfo TargetDevice { get; set; }
        /// <summary>
        /// 主函数
        /// </summary>
        public abstract int Main();
        /// <summary>
        /// 当用户要求终止时调用
        /// </summary>
        /// <returns></returns>
        public virtual bool OnStopCommand()
        {
            return true;
        }
    }
}
