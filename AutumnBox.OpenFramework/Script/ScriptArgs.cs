/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 20:33:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本运行参数
    /// </summary>
    public class ScriptArgs:IScriptArgs
    {
        /// <summary>
        /// 目标设备
        /// </summary>
        public DeviceBasicInfo DeviceInfo { get;private set; }
        public Script Self { get; internal set; }
        public Context Context { get; private set; }
        public ScriptArgs(Script self,DeviceBasicInfo device)
        {
            this.Self = self;
            this.Context = Self;
            this.DeviceInfo = device;
        }
    }
}
