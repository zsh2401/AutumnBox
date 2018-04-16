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
        /// <summary>
        /// 获取脚本本身，类似this
        /// </summary>
        public ScriptBase Self { get; internal set; }
        /// <summary>
        /// 获取该脚本的对应上下文
        /// </summary>
        public Context Context { get; private set; }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="self"></param>
        /// <param name="device"></param>
        internal ScriptArgs(ScriptBase self,DeviceBasicInfo device)
        {
            this.Self = self;
            this.Context = Self;
            this.DeviceInfo = device;
        }
    }
}
