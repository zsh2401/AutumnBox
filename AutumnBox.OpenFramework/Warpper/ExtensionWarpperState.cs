/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 16:08:16 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Warpper
{
    /// <summary>
    /// 拓展模块包装器的状态
    /// </summary>
    public enum ExtensionWarpperState
    {
        /// <summary>
        /// 准备状态,只要是非运行状态,都是此状态
        /// </summary>
        Ready = 0,
        /// <summary>
        /// 运行状态
        /// </summary>
        Running = 1,
    }
}
